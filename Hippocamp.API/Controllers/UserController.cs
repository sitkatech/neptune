using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using System;
using System.Net.Mail;
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

        [HttpPost("users")]
        [LoggedInUnclassifiedFeature]
        public ActionResult<PersonDto> CreateUser([FromBody] PersonCreateDto personCreateDto)
        {
            // Validate request body; all fields required in Dto except Org Name and Phone
            if (personCreateDto == null)
            {
                return BadRequest();
            }

            var validationMessages = People.ValidateCreateUnassignedPerson(_dbContext, personCreateDto);
            validationMessages.ForEach(vm => { ModelState.AddModelError(vm.Type, vm.Message); });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = People.CreateUnassignedPerson(_dbContext, personCreateDto);

            var smtpClient = HttpContext.RequestServices.GetRequiredService<SitkaSmtpClientService>();
            var mailMessage = GenerateUserCreatedEmail(user, smtpClient);
            SitkaSmtpClientService.AddCcRecipientsToEmail(mailMessage,
                        People.GetEmailAddressesForAdminsThatReceiveSupportEmails(_dbContext));
            SendEmailMessage(smtpClient, mailMessage);

            return Ok(user);
        }

        [HttpGet("users")]
        [UserViewFeature]
        public ActionResult<PersonSimpleDto> List()
        {
            var userList = People.ListAsSimpleDto(_dbContext);
            return Ok(userList);
        }

        [HttpGet("users/{personID}")]
        [UserViewDetailFeature]
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

        private MailMessage GenerateUserCreatedEmail(PersonDto person, SitkaSmtpClientService smtpClient)
        {
            var messageBody = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>OC Stormwater Tools User added:</strong> {person.FirstName} {person.LastName}<br />
    <strong>Added on:</strong> {DateTime.UtcNow}<br />
    <strong>Email:</strong> {person.Email}<br />
    <strong>Phone:</strong> {person.Phone}<br />
    <br />
    <p>
        You may want to <a href=""{_hippocampConfiguration.OcStormwaterToolsBaseUrl}/Detail/{person.PersonID}"">assign this user a role</a> and associate them with a jurisdiction to allow them to use the site. Or you can leave the user with Unassigned roles if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {person.LoginName}<br />
    <br />
    </div>
    {smtpClient.GetSupportNotificationEmailSignature()}
</div>
";

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
