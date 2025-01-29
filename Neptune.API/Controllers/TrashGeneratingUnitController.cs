using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers;

[ApiController]
[Route("trash-generating-units")]
public class TrashGeneratingUnitController(
    NeptuneDbContext dbContext,
    ILogger<TrashGeneratingUnitController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<TrashGeneratingUnitController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [JurisdictionEditFeature]
    public ActionResult<List<TrashGeneratingUnitGridDto>> ListForCallingUser()
    {
        var trashGeneratingUnitGridDtos = TrashGeneratingUnits.List(DbContext, CallingUser);
        return Ok(trashGeneratingUnitGridDtos);
    }
}