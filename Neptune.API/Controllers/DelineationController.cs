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
    [Route("delineations")]
    public class DelineationController(
        NeptuneDbContext dbContext,
        ILogger<DelineationController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<DelineationController>(dbContext, logger, keystoneService, neptuneConfiguration)
    {
        [HttpGet]
        [JurisdictionEditFeature]
        public ActionResult<List<DelineationDto>> List()
        {
            var delineationsUpsertDtos = Delineations.ListByPersonIDAsDto(DbContext, CallingUser.PersonID);
            return Ok(delineationsUpsertDtos);
        }
    }
}