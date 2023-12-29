using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Neptune.Models.DataTransferObjects;
using Microsoft.Extensions.DependencyInjection;
using Neptune.Common;
using Neptune.Common.Email;
using Neptune.Models.DataTransferObjects.Person;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class UserController : SitkaController<UserController>
    {
        private readonly SitkaSmtpClientService _sitkaSmtpClientService;

        public UserController(NeptuneDbContext dbContext, ILogger<UserController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
            _sitkaSmtpClientService = sitkaSmtpClientService;
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

            var mailMessage = GenerateUserCreatedEmail(user);
            SitkaSmtpClientService.AddCcRecipientsToEmail(mailMessage,
                        People.GetEmailAddressesForAdminsThatReceiveSupportEmails(_dbContext));
            SendEmailMessage(mailMessage);

            return Ok(user);
        }

        [HttpGet("users")]
        [UserViewFeature]
        public ActionResult<List<PersonSimpleDto>> List()
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

            var userDto = People.GetByGuidAsDto(_dbContext, globalIDAsGuid);
            if (userDto == null)
            {
                var notFoundMessage = $"User with GUID {globalIDAsGuid} does not exist!";
                _logger.LogError(notFoundMessage);
                return NotFound(notFoundMessage);
            }

            return Ok(userDto);
        }

        private MailMessage GenerateUserCreatedEmail(PersonDto person)
        {
            var messageBody = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>OC Stormwater Tools User added:</strong> {person.FirstName} {person.LastName}<br />
    <strong>Added on:</strong> {DateTime.UtcNow}<br />
    <strong>Email:</strong> {person.Email}<br />
    <strong>Phone:</strong> {person.Phone}<br />
    <br />
    <p>
        You may want to <a href=""{_neptuneConfiguration.OcStormwaterToolsBaseUrl}/Detail/{person.PersonID}"">assign this user a role</a> and associate them with a jurisdiction to allow them to use the site. Or you can leave the user with Unassigned roles if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {person.LoginName}<br />
    <br />
    </div>
    {_sitkaSmtpClientService.GetSupportNotificationEmailSignature()}
</div>
";

            var mailMessage = new MailMessage
            {
                Subject = $"New User in OC Stormwater Tools",
                Body = $"Hello,<br /><br />{messageBody}",
            };

            mailMessage.To.Add(_sitkaSmtpClientService.GetDefaultEmailFrom());
            return mailMessage;
        }

        private async Task SendEmailMessage(MailMessage mailMessage)
        {
            mailMessage.IsBodyHtml = true;
            mailMessage.From = _sitkaSmtpClientService.GetDefaultEmailFrom();
            mailMessage.ReplyToList.Add(_neptuneConfiguration.DoNotReplyEmail);
            await _sitkaSmtpClientService.Send(mailMessage);
        }
    }
}
