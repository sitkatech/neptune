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
    [Route("treatment-bmps/{treatmentBMPID}/benchmarks-and-thresholds")]
    public class TreatmentBMPBenchmarkAndThresholdController(
        NeptuneDbContext dbContext,
        ILogger<TreatmentBMPBenchmarkAndThresholdController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<TreatmentBMPBenchmarkAndThresholdController>(dbContext, logger, keystoneService,
            neptuneConfiguration)
    {
        [HttpGet]
        [AllowAnonymous]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<IEnumerable<TreatmentBMPBenchmarkAndThresholdDto>>> List([FromRoute] int treatmentBMPID)
        {
            var items = await TreatmentBMPBenchmarkAndThresholds.ListByTreatmentBMPIDAsDtoAsync(DbContext, treatmentBMPID);
            return Ok(items);
        }

        [HttpPost]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        public async Task<ActionResult<TreatmentBMPBenchmarkAndThresholdDto>> Create([FromRoute] int treatmentBMPID, [FromBody] TreatmentBMPBenchmarkAndThresholdUpsertDto dto)
        {
            var created = await TreatmentBMPBenchmarkAndThresholds.CreateAsync(DbContext, treatmentBMPID, dto);
            return CreatedAtAction(nameof(Get), new { treatmentBMPID, id = created.TreatmentBMPBenchmarkAndThresholdID }, created);
        }

        [HttpGet("{treatmentBMPBenchmarkAndThresholdID}")]
        [AllowAnonymous]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(TreatmentBMPBenchmarkAndThreshold), "treatmentBMPBenchmarkAndThresholdID")]
        public async Task<ActionResult<TreatmentBMPBenchmarkAndThresholdDto>> Get([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPBenchmarkAndThresholdID)
        {
            var dto = await TreatmentBMPBenchmarkAndThresholds.GetByIDAsync(DbContext, treatmentBMPBenchmarkAndThresholdID);
            return Ok(dto);
        }

        [HttpPut("{treatmentBMPBenchmarkAndThresholdID}")]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(TreatmentBMPBenchmarkAndThreshold), "treatmentBMPBenchmarkAndThresholdID")]
        public async Task<ActionResult<TreatmentBMPBenchmarkAndThresholdDto>> Update([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPBenchmarkAndThresholdID, [FromBody] TreatmentBMPBenchmarkAndThresholdUpsertDto dto)
        {
            var updated = await TreatmentBMPBenchmarkAndThresholds.UpdateAsync(DbContext, treatmentBMPBenchmarkAndThresholdID, dto);
            return Ok(updated);
        }

        [HttpDelete("{treatmentBMPBenchmarkAndThresholdID}")]
        [JurisdictionEditFeature]
        [EntityNotFoundAttribute(typeof(TreatmentBMP), "treatmentBMPID")]
        [EntityNotFoundAttribute(typeof(TreatmentBMPBenchmarkAndThreshold), "treatmentBMPBenchmarkAndThresholdID")]
        public async Task<IActionResult> Delete([FromRoute] int treatmentBMPID, [FromRoute] int treatmentBMPBenchmarkAndThresholdID)
        {
            var deleted = await TreatmentBMPBenchmarkAndThresholds.DeleteAsync(DbContext, treatmentBMPBenchmarkAndThresholdID);
            return NoContent();
        }
    }
}
