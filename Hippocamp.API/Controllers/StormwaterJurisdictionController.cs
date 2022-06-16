using System.Collections.Generic;
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
    public class StormwaterJurisdictionController : SitkaController<StormwaterJurisdictionController>
    {
        public StormwaterJurisdictionController(HippocampDbContext dbContext, ILogger<StormwaterJurisdictionController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
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
        [JurisdictionEditFeature]
        public ActionResult<List<BoundingBoxDto>> GetBoundingBoxByProjectID([FromRoute] int projectID)
        {
            var stormwaterJurisdictionID = Projects.GetByID(_dbContext, projectID).StormwaterJurisdictionID;
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(_dbContext, stormwaterJurisdictionID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("jurisdictions/boundingBox")]
        [UserViewFeature]
        public ActionResult<List<BoundingBoxDto>> GetBoundingBoxByPersonID([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByPersonID(_dbContext, personDto.PersonID);
            return Ok(boundingBoxDto);
        }
    }
}