using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class DelineationController : SitkaController<DelineationController>
    {
        public DelineationController(HippocampDbContext dbContext, ILogger<DelineationController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("Delineations/{projectID}/getByProjectID")]
        [JurisdictionEditFeature]
        public ActionResult<DelineationUpsertDto> GetByProjectID([FromRoute] int projectID)
        {
            var DelineationUpsertDtos = Delineations.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(DelineationUpsertDtos);
        }

        //[HttpGet("Delineations")]
        //[JurisdictionEditFeature]
        //public ActionResult<DelineationUpsertDto> List([FromRoute] int projectID)
        //{
        //    var DelineationDisplayDtos = Delineations.ListAsDisplayDto(_dbContext);
        //    return Ok(DelineationDisplayDtos);
        //}
    }
}