using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("jurisdictions")]
    public class StormwaterJurisdictionController(
        NeptuneDbContext dbContext,
        ILogger<StormwaterJurisdictionController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<StormwaterJurisdictionController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpGet]
        [JurisdictionEditFeature]
        public async Task<ActionResult<List<StormwaterJurisdictionGridDto>>> List()
        {
            var stormwaterJurisdictionDtos = await StormwaterJurisdictions.ListAsDtoAsync(DbContext);
            return Ok(stormwaterJurisdictionDtos);
        }

        [HttpGet("user-viewable")]
        public async Task<ActionResult<List<StormwaterJurisdictionDisplayDto>>> ListViewable()
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
            var stormwaterJurisdictionDisplayDtos = await StormwaterJurisdictions.ListByIDsAsDisplayDtoAsync(DbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionDisplayDtos);
        }

        [HttpGet("bounding-box")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBox()
        {
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByPersonID(DbContext, CallingUser.PersonID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("{jurisdictionID}/bounding-box")]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByJurisdictionID([FromRoute] int jurisdictionID)
        {
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(DbContext, jurisdictionID);
            return Ok(boundingBoxDto);
        }

    }
}