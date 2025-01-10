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
    public class StormwaterJurisdictionController(
        NeptuneDbContext dbContext,
        ILogger<StormwaterJurisdictionController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        Person callingUser)
        : SitkaController<StormwaterJurisdictionController>(dbContext, logger, keystoneService, neptuneConfiguration,
            callingUser)
    {
        [HttpGet("jurisdictions")]
        [JurisdictionEditFeature]
        public ActionResult<List<StormwaterJurisdictionDto>> ListByPersonID()
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(DbContext, CallingUser.PersonID);
            var stormwaterJurisdictionSimpleDtos = StormwaterJurisdictions.ListByIDsAsDto(DbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionSimpleDtos);
        }

        [HttpGet("jurisdictions/{projectID}/getBoundingBoxByProjectID")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByProjectID([FromRoute] int projectID)
        {
            var stormwaterJurisdictionID = Projects.GetByIDWithChangeTracking(DbContext, projectID).StormwaterJurisdictionID;
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(DbContext, stormwaterJurisdictionID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("jurisdictions/boundingBox")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByPersonID()
        {
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByPersonID(DbContext, CallingUser.PersonID);
            return Ok(boundingBoxDto);
        }
    }
}