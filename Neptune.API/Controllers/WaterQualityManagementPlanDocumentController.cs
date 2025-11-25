using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("water-quality-management-plan-documents")]
    public class WaterQualityManagementPlanDocumentController : SitkaController<WaterQualityManagementPlanDocumentController>
    {
        public WaterQualityManagementPlanDocumentController(
            NeptuneDbContext dbContext,
            ILogger<WaterQualityManagementPlanDocumentController> logger,
            KeystoneService keystoneService,
            IOptions<NeptuneConfiguration> neptuneConfiguration)
            : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet]
        [AdminFeature]
        public async Task<ActionResult<IEnumerable<WaterQualityManagementPlanDocumentDto>>> List()
        {
            var docs = await WaterQualityManagementPlanDocuments.ListAsDtoAsync(DbContext);
            return Ok(docs);
        }

        [HttpGet("{waterQualityManagementPlanDocumentID}")]
        [AdminFeature]
        [EntityNotFoundAttribute(typeof(WaterQualityManagementPlanDocument), "waterQualityManagementPlanDocumentID")]
        public async Task<ActionResult<WaterQualityManagementPlanDocumentDto>> Get([FromRoute] int waterQualityManagementPlanDocumentID)
        {
            var entity = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        [AdminFeature]
        public async Task<ActionResult<WaterQualityManagementPlanDocumentDto>> Create([FromBody] WaterQualityManagementPlanDocumentUpsertDto dto)
        {
            var created = await WaterQualityManagementPlanDocuments.CreateAsync(DbContext, dto);
            return CreatedAtAction(nameof(Get), new { waterQualityManagementPlanDocumentID = created.WaterQualityManagementPlanDocumentID }, created);
        }

        [HttpPut("{waterQualityManagementPlanDocumentID}")]
        [AdminFeature]
        [EntityNotFoundAttribute(typeof(WaterQualityManagementPlanDocument), "waterQualityManagementPlanDocumentID")]
        public async Task<ActionResult<WaterQualityManagementPlanDocumentDto>> Update([FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] WaterQualityManagementPlanDocumentUpsertDto dto)
        {
            var updated = await WaterQualityManagementPlanDocuments.UpdateAsync(DbContext, waterQualityManagementPlanDocumentID, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{waterQualityManagementPlanDocumentID}")]
        [AdminFeature]
        [EntityNotFoundAttribute(typeof(WaterQualityManagementPlanDocument), "waterQualityManagementPlanDocumentID")]
        public async Task<IActionResult> Delete([FromRoute] int waterQualityManagementPlanDocumentID)
        {
            var deleted = await WaterQualityManagementPlanDocuments.DeleteAsync(DbContext, waterQualityManagementPlanDocumentID);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
