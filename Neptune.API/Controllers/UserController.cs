using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Models.DataTransferObjects.Person;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(
        NeptuneDbContext dbContext,
        ILogger<UserController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        SitkaSmtpClientService sitkaSmtpClientService)
        : SitkaController<UserController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpPost]
        [LoggedInUnclassifiedFeature]
        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonCreateDto personCreateDto)
        {
            // Validate request body; all fields required in Dto except Org Name and Phone
            if (personCreateDto == null)
            {
                return BadRequest();
            }

            var validationMessages = People.ValidateCreateUnassignedPerson(DbContext, personCreateDto);
            validationMessages.ForEach(vm => { ModelState.AddModelError(vm.Type, vm.Message); });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = People.CreateUnassignedPerson(DbContext, personCreateDto);

            var mailMessage = GenerateUserCreatedEmail(user);
            SitkaSmtpClientService.AddCcRecipientsToEmail(mailMessage,
                        People.GetEmailAddressesForAdminsThatReceiveSupportEmails(DbContext));
            await SendEmailMessage(mailMessage);

            return Ok(user);
        }

        [HttpGet]
        [UserViewFeature]
        public async Task<ActionResult<List<PersonSimpleDto>>> List()
        {
            var people = await People.ListAsSimpleDtoAsync(DbContext);
            return Ok(people);
        }

        [HttpGet("{personID}")]
        [UserViewDetailFeature]
        [EntityNotFoundAttribute(typeof(Person), "personID")]
        public async Task<ActionResult<PersonDto>> Get([FromRoute] int personID)
        {
            var person = await People.GetByIDAsDtoAsync(DbContext, personID);
            if (person == null) return NotFound();
            return Ok(person);
        }

        //[HttpPost]
        //[AdminFeature]
        //public async Task<ActionResult<PersonDto>> Create([FromBody] PersonUpsertDto dto)
        //{
        //    var created = await People.CreateAsync(DbContext, dto, dto.Email, Guid.NewGuid());
        //    return CreatedAtAction(nameof(Get), new { personID = created.PersonID }, created);
        //}

        [HttpPut("{personID}")]
        [AdminFeature]
        [EntityNotFoundAttribute(typeof(Person), "personID")]
        public async Task<ActionResult<PersonDto>> Update([FromRoute] int personID, [FromBody] PersonUpsertDto dto)
        {
            var updated = await People.UpdateAsync(DbContext, personID, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{personID}")]
        [AdminFeature]
        [EntityNotFoundAttribute(typeof(Person), "personID")]
        public async Task<IActionResult> Delete([FromRoute] int personID)
        {
            var deleted = await People.DeleteAsync(DbContext, personID);
            if (!deleted) return NotFound();
            return NoContent();
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
        You may want to <a href=""{NeptuneConfiguration.OcStormwaterToolsBaseUrl}/Detail/{person.PersonID}"">assign this user a role</a> and associate them with a jurisdiction to allow them to use the site. Or you can leave the user with Unassigned roles if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {person.LoginName}<br />
    <br />
    </div>
    {sitkaSmtpClientService.GetSupportNotificationEmailSignature()}
</div>
";

            var mailMessage = new MailMessage
            {
                Subject = $"New User in OC Stormwater Tools",
                Body = $"Hello,<br /><br />{messageBody}",
            };

            mailMessage.To.Add(sitkaSmtpClientService.GetDefaultEmailFrom());
            return mailMessage;
        }

        private async Task SendEmailMessage(MailMessage mailMessage)
        {
            mailMessage.IsBodyHtml = true;
            mailMessage.From = sitkaSmtpClientService.GetDefaultEmailFrom();
            mailMessage.ReplyToList.Add(NeptuneConfiguration.DoNotReplyEmail);
            await sitkaSmtpClientService.Send(mailMessage);
        }
    }
}
