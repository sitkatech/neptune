using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.API.Services.Authorization;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("treatment-bmps/{treatmentBMPID}/funding-events")]
    public class FundingEventByTreatmentBMPIDController : SitkaController<FundingEventByTreatmentBMPIDController>
    {
        public FundingEventByTreatmentBMPIDController(
            NeptuneDbContext dbContext,
            ILogger<FundingEventByTreatmentBMPIDController> logger,
            KeystoneService keystoneService,
            IOptions<NeptuneConfiguration> neptuneConfiguration)
            : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<IEnumerable<FundingEventDto>>> List([FromRoute] int treatmentBMPID)
        {
            var events = await FundingEvents.ListByTreatmentBMPIDAsDtoAsync(DbContext, treatmentBMPID);
            return Ok(events);
        }

        [HttpPost]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<FundingEventDto>> Create([FromRoute] int treatmentBMPID,
            [FromBody] FundingEventUpsertDto dto)
        {
            var validationError = FundingEvents.ValidateFundingEventUpsertDto(dto);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }
            var created = await FundingEvents.CreateAsync(DbContext, treatmentBMPID, dto);
            return CreatedAtAction(nameof(Get), new { treatmentBMPID, fundingEventID = created.FundingEventID },
                created);
        }

        [HttpGet("{fundingEventID}")]
        [AllowAnonymous]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(FundingEvent), "fundingEventID")]
        public async Task<ActionResult<FundingEventDto>> Get([FromRoute] int treatmentBMPID, [FromRoute] int fundingEventID)
        {
            var entity = await FundingEvents.GetByIDAsDtoAsync(DbContext, fundingEventID);
            return Ok(entity);
        }

        [HttpPut("{fundingEventID}")]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(FundingEvent), "fundingEventID")]
        public async Task<ActionResult<FundingEventDto>> Update([FromRoute] int treatmentBMPID,
            [FromRoute] int fundingEventID, [FromBody] FundingEventUpsertDto dto)
        {
            var validationError = FundingEvents.ValidateFundingEventUpsertDto(dto);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }
            var updated = await FundingEvents.UpdateAsync(DbContext, fundingEventID, dto);
            return Ok(updated);
        }

        [HttpDelete("{fundingEventID}")]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(FundingEvent), "fundingEventID")]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID, [FromRoute] int fundingEventID)
        {
            var deleted = await FundingEvents.DeleteAsync(DbContext, fundingEventID);
            return NoContent();
        }
    }
}
