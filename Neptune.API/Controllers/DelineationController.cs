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
        public ActionResult<List<DelineationSimpleDto>> ListByPersonID()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var delineationsUpsertDtos = Delineations.ListByPersonIDAsSimpleDto(_dbContext, personDto.PersonID);
            return Ok(delineationsUpsertDtos);
        }
    }
}