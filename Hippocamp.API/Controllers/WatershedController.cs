using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Hippocamp.Models.DataTransferObjects.Watershed;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class WatershedController : SitkaController<WatershedController>
    {
        public WatershedController(HippocampDbContext dbContext, ILogger<WatershedController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("/watersheds")]
        [AdminFeature]
        public ActionResult<List<WatershedDto>> ListAllWatersheds()
        {
            var watershedDtos = Watershed.List(_dbContext).OrderBy(x => x.WatershedName).ToList();
            return watershedDtos;
        }

        [HttpGet("/watersheds/{watershedID}")]
        [AdminFeature]
        public ActionResult<WatershedDto> GetWatershedByID([FromRoute] int watershedID)
        {
            var watershedDto = Watershed.GetByWatershedID(_dbContext, watershedID);
            return RequireNotNullThrowNotFound(watershedDto, "Watershed", watershedID);
        }

        [HttpPost("watersheds/getBoundingBox")]
        [AdminFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByWatershedIDs([FromBody] WatershedIDListDto watershedIDListDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var boundingBoxDto = Watershed.GetBoundingBoxByWatershedIDs(_dbContext, watershedIDListDto.WatershedIDs);
            return Ok(boundingBoxDto);
        }
    }
}