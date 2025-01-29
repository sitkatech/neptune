using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;

namespace Neptune.API.Controllers;

[ApiController]
[Route("land-use-blocks")]
public class LandUseBlockController(
    NeptuneDbContext dbContext,
    ILogger<LandUseBlockController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<LandUseBlockController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    public ActionResult<List<LandUseBlockGridDto>> List()
    {
        var landUseBlockGridDtos = LandUseBlocks.List(DbContext);
        return landUseBlockGridDtos;
    }
}