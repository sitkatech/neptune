using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hippocamp.Models.DataTransferObjects;
using Hippocamp.Models.DataTransferObjects.Person;
using Microsoft.Extensions.DependencyInjection;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class UserController : SitkaController<UserController>
    {
        public UserController(HippocampDbContext dbContext, ILogger<UserController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpPost("/users/invite")]
        [AdminFeature]
        public async Task<IActionResult> InviteUser([FromBody] PersonInviteDto inviteDto)
        {
            if (inviteDto.RoleID.HasValue)
            {
                var role = Role.GetByRoleID(_dbContext, inviteDto.RoleID.Value);
                if (role == null)
                {
                    return BadRequest($"Could not find a Role with the ID {inviteDto.RoleID}");
                }
            }
            else
            {
                return BadRequest("Role ID is required.");
            }

            var applicationName = $"{_hippocampConfiguration.PlatformLongName}";
            var leadOrganizationLongName = $"{_hippocampConfiguration.LeadOrganizationLongName}";
            var inviteModel = new KeystoneService.KeystoneInviteModel
            {
                FirstName = inviteDto.FirstName,
                LastName = inviteDto.LastName,
                Email = inviteDto.Email,
                Subject = $"Invitation to the {applicationName}",
                WelcomeText = $"You are receiving this notification because an administrator of the {applicationName}, an online service of the {leadOrganizationLongName}, has invited you to create an account.",
                SiteName = applicationName,
                SignatureBlock = $"{leadOrganizationLongName}<br /><a href='mailto:{_hippocampConfiguration.LeadOrganizationEmail}'>{_hippocampConfiguration.LeadOrganizationEmail}</a><a href='{_hippocampConfiguration.LeadOrganizationHomeUrl}'>{_hippocampConfiguration.LeadOrganizationHomeUrl}</a>",
                RedirectURL = _hippocampConfiguration.KEYSTONE_REDIRECT_URL
            };

            var response = await _keystoneService.Invite(inviteModel);
            if (response.StatusCode != HttpStatusCode.OK || response.Error != null)
            {
                ModelState.AddModelError("Email", $"There was a problem inviting the user to Keystone: {response.Error.Message}.");
                if (response.Error.ModelState != null)
                {
                    foreach (var modelStateKey in response.Error.ModelState.Keys)
                    {
                        foreach (var err in response.Error.ModelState[modelStateKey])
                        {
                            ModelState.AddModelError(modelStateKey, err);
                        }
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keystoneUser = response.Payload.Claims;
            var existingUser = People.GetByEmailAsDto(_dbContext, inviteDto.Email);
            if (existingUser != null)
            {
                existingUser = People.UpdatePersonGuid(_dbContext, existingUser.PersonID, keystoneUser.UserGuid);
                return Ok(existingUser);
            }

            var newUser = new PersonUpsertDto
            {
                FirstName = keystoneUser.FirstName,
                LastName = keystoneUser.LastName,
                OrganizationName = keystoneUser.OrganizationName,
                Email = keystoneUser.Email,
                PhoneNumber = keystoneUser.PrimaryPhone,
                RoleID = inviteDto.RoleID.Value
            };

            var user = People.CreateNewPerson(_dbContext, newUser, keystoneUser.LoginName,
                keystoneUser.UserGuid);
            return Ok(user);
        }

        [HttpPost("users")]
        [LoggedInUnclassifiedFeature]
        public ActionResult<PersonDto> CreateUser([FromBody] PersonCreateDto personUpsertDto)
        {
            var user = People.CreateNewPerson(_dbContext, personUpsertDto, personUpsertDto.LoginName,
                personUpsertDto.UserGuid);

            var smtpClient = HttpContext.RequestServices.GetRequiredService<SitkaSmtpClientService>();
            var mailMessage = GenerateUserCreatedEmail(_hippocampConfiguration.WEB_URL, user, _dbContext, smtpClient);
            SitkaSmtpClientService.AddCcRecipientsToEmail(mailMessage,
                        People.GetEmailAddressesForAdminsThatReceiveSupportEmails(_dbContext));
            SendEmailMessage(smtpClient, mailMessage);

            return Ok(user);
        }

        [HttpGet("users")]
        [AdminFeature]
        public ActionResult<IEnumerable<PersonDto>> List()
        {
            var userDtos = People.ListAsDto(_dbContext);
            return Ok(userDtos);
        }

        [HttpGet("users/unassigned-report")]
        [AdminFeature]
        public ActionResult<UnassignedUserReportDto> GetUnassignedUserReport()
        {
            var report = new UnassignedUserReportDto
                {Count = _dbContext.People.Count(x => x.RoleID == (int) RoleEnum.Unassigned)};
            return Ok(report);
        }

        [HttpGet("users/{personID}")]
        [UserViewFeature]
        public ActionResult<PersonDto> GetByPersonID([FromRoute] int personID)
        {
            var userDto = People.GetByIDAsDto(_dbContext, personID);
            return RequireNotNullThrowNotFound(userDto, "User", personID);
        }

        [HttpGet("user-claims/{globalID}")]
        public ActionResult<PersonDto> GetByGlobalID([FromRoute] string globalID)
        {
            var isValidGuid = Guid.TryParse(globalID, out var globalIDAsGuid);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var userDto = People.GetByPersonGuidAsDto(_dbContext, globalIDAsGuid);
            if (userDto == null)
            {
                var notFoundMessage = $"User with GUID {globalIDAsGuid} does not exist!";
                _logger.LogError(notFoundMessage);
                return NotFound(notFoundMessage);
            }

            return Ok(userDto);
        }

        [HttpPut("users/{personID}")]
        [AdminFeature]
        public ActionResult<PersonDto> UpdateUser([FromRoute] int personID, [FromBody] PersonUpsertDto personUpsertDto)
        {
            var userDto = People.GetByIDAsDto(_dbContext, personID);
            if (ThrowNotFound(userDto, "Person", personID, out var actionResult))
            {
                return actionResult;
            }

            var validationMessages =
                People.ValidateUpdate(_dbContext, personUpsertDto, userDto.PersonID);
            validationMessages.ForEach(vm => { ModelState.AddModelError(vm.Type, vm.Message); });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = Role.GetByRoleID(_dbContext, personUpsertDto.RoleID.GetValueOrDefault());
            if (role == null)
            {
                return BadRequest($"Could not find a System Role with the ID {personUpsertDto.RoleID}");
            }

            var updatedPersonDto = People.UpdatePersonEntity(_dbContext, personID, personUpsertDto);
            return Ok(updatedPersonDto);
        }

        private MailMessage GenerateUserCreatedEmail(string hippocampUrl, PersonDto user, HippocampDbContext dbContext,
            SitkaSmtpClientService smtpClient)
        {
            var messageBody = $@"A new user has signed up to the {_hippocampConfiguration.PlatformLongName}: <br/><br/>
 {user.FullName} ({user.Email}) <br/><br/>
As an administrator of the {_hippocampConfiguration.PlatformShortName}, you can assign them a role and associate them with a Billing Account by following <a href='{hippocampUrl}/users/{user.PersonID}'>this link</a>. <br/><br/>
{smtpClient.GetSupportNotificationEmailSignature()}";

            var mailMessage = new MailMessage
            {
                Subject = $"New User in {_hippocampConfiguration.PlatformLongName}",
                Body = $"Hello,<br /><br />{messageBody}",
            };

            mailMessage.To.Add(smtpClient.GetDefaultEmailFrom());
            return mailMessage;
        }

        private void SendEmailMessage(SitkaSmtpClientService smtpClient, MailMessage mailMessage)
        {
            mailMessage.IsBodyHtml = true;
            mailMessage.From = smtpClient.GetDefaultEmailFrom();
            mailMessage.ReplyToList.Add(!String.IsNullOrWhiteSpace(_hippocampConfiguration.LeadOrganizationEmail) ? _hippocampConfiguration.LeadOrganizationEmail : "donotreply@sitkatech.com");
            smtpClient.Send(mailMessage);
        }
    }
}
