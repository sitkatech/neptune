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
    public class DelineationController : SitkaController<DelineationController>
    {
        public DelineationController(NeptuneDbContext dbContext, ILogger<DelineationController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("delineations")]
        [JurisdictionEditFeature]
        public ActionResult<List<DelineationDto>> List()
        {
            var person = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var delineationsUpsertDtos = Delineations.ListByPersonIDAsDto(_dbContext, person.PersonID);
            return Ok(delineationsUpsertDtos);
        }
    }
}