using System.Collections.Generic;
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
        public ActionResult<List<StormwaterJurisdictionDto>> ListByPersonID()
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
            var stormwaterJurisdictionSimpleDtos = StormwaterJurisdictions.ListByIDsAsDto(DbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionSimpleDtos);
        }


        [HttpGet("user-viewable")]
        public ActionResult<List<StormwaterJurisdictionDto>> ListViewableStormwaterJurisdictionIDsByPersonID()
        {
            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonIDForBMPs(DbContext, CallingUser?.PersonID);
            var stormwaterJurisdictionSimpleDtos = StormwaterJurisdictions.ListByIDsAsDto(DbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionSimpleDtos);
        }

        [HttpGet("bounding-box")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByPersonID()
        {
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByPersonID(DbContext, CallingUser.PersonID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("{jurisdictionID}/bounding-box")]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByProjectID([FromRoute] int jurisdictionID)
        {
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(DbContext, jurisdictionID);
            return Ok(boundingBoxDto);
        }

    }
}