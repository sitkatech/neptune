using System.Collections.Generic;
using System.Linq;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class TreatmentBMPController : SitkaController<TreatmentBMPController>
    {
        public TreatmentBMPController(HippocampDbContext dbContext, ILogger<TreatmentBMPController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("treatmentBMPs/{projectID}/getByProjectID")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPUpsertDto> GetByProjectID([FromRoute] int projectID)
        {
            var treatmentBMPUpsertDtos = TreatmentBMPs.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(treatmentBMPUpsertDtos);
        }

        [HttpGet("treatmentBMPs")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPUpsertDto> List([FromRoute] int projectID)
        {
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListAsDisplayDto(_dbContext);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("treatmentBMPs/types")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPTypeSimpleDto> ListTypes()
        {
            var treatmentBMPTypeSimpleDtos = TreatmentBMPs.ListTypesAsSimpleDto(_dbContext);
            return Ok(treatmentBMPTypeSimpleDtos);
        }

        [HttpGet("treatmentBMPs/modelingAttributeDropdownItems")]
        [JurisdictionEditFeature]
        public ActionResult<TreatmentBMPModelingAttributeDropdownItemDto> GetModelingAttributeDropdownItems()
        {
            var treatmentBMPModelingAttributeDropdownItemDtos = TreatmentBMPs.GetModelingAttributeDropdownItemsAsDto(_dbContext);
            return Ok(treatmentBMPModelingAttributeDropdownItemDtos);
        }

        [HttpPut("treatmentBMPs/{projectID}")]
        [JurisdictionEditFeature]
        public ActionResult MergeTreatmentBMPs(List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos, [FromRoute] int projectID)
        {

            var updatedTreatmentBMPs = treatmentBMPUpsertDtos.Where(x => x.TreatmentBMPID > 0).ToList();
            var existingProjectTreatmentBMPs = _dbContext.TreatmentBMPs.Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP).Where(x => x.ProjectID == projectID).ToList();
            var allTreatmentBMPsInDatabase = _dbContext.TreatmentBMPs;
            var allTreatmentBMPModelingAttributesInDatabase = _dbContext.TreatmentBMPModelingAttributes;
            TreatmentBMPs.MergeUpdatedAndDeletedTreatmentBMPsByProjectID(updatedTreatmentBMPs, existingProjectTreatmentBMPs, allTreatmentBMPsInDatabase, allTreatmentBMPModelingAttributesInDatabase);

            var newTreatmentBMPs = treatmentBMPUpsertDtos.Where(x => x.TreatmentBMPID <= 0).ToList();
            TreatmentBMPs.MergeNewTreatmentBMPs(_dbContext, newTreatmentBMPs, existingProjectTreatmentBMPs, allTreatmentBMPsInDatabase, allTreatmentBMPModelingAttributesInDatabase);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}