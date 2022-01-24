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
        public ActionResult<List<StormwaterJurisdictionSimpleDto>> GetByPersonID([FromRoute] int personID)
        {
            var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(_dbContext, personID);
            var stormwaterJurisdictionSimpleDtos = StormwaterJurisdictions.ListByIDsAsSimpleDto(_dbContext, stormwaterJurisdictionIDs);
            return Ok(stormwaterJurisdictionSimpleDtos);
        }
    }
}