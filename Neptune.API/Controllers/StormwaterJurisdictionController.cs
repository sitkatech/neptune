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
    public class StormwaterJurisdictionController : SitkaController<StormwaterJurisdictionController>
    {
        public StormwaterJurisdictionController(NeptuneDbContext dbContext, ILogger<StormwaterJurisdictionController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("jurisdictions/{personID}")]
        [JurisdictionEditFeature]
        public ActionResult<List<StormwaterJurisdictionSimpleDto>> ListByPersonID([FromRoute] int personID)
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(_dbContext, personID);
            var stormwaterJurisdictionSimpleDtos = StormwaterJurisdictions.ListByIDsAsSimpleDto(_dbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionSimpleDtos);
        }

        [HttpGet("jurisdictions/{projectID}/getBoundingBoxByProjectID")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByProjectID([FromRoute] int projectID)
        {
            var stormwaterJurisdictionID = Projects.GetByID(_dbContext, projectID).StormwaterJurisdictionID;
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(_dbContext, stormwaterJurisdictionID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("jurisdictions/boundingBox")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByPersonID([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByPersonID(_dbContext, personDto.PersonID);
            return Ok(boundingBoxDto);
        }
    }
}