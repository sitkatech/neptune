using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class DelineationController : SitkaController<DelineationController>
    {
        public DelineationController(HippocampDbContext dbContext, ILogger<DelineationController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("delineations/{projectID}/getByProjectID")]
        [JurisdictionEditFeature]
        public ActionResult<DelineationUpsertDto> GetByProjectID([FromRoute] int projectID)
        {
            var DelineationUpsertDtos = Delineations.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(DelineationUpsertDtos);
        }

        [HttpPut("delineations/{projectID}")]
        [JurisdictionEditFeature]
        public ActionResult MergeDelineations(List<DelineationUpsertDto> delineationUpsertDtos, [FromRoute] int projectID)
        {
            // project validation here
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            Delineations.MergeDelineations(_dbContext, delineationUpsertDtos, project);

            return Ok();
        }
    }
}