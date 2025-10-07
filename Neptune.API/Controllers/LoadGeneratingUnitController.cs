using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.API.Controllers;

[ApiController]
[Route("load-generating-units")]
public class LoadGeneratingUnitController : SitkaController<LoadGeneratingUnitController>
{
    public LoadGeneratingUnitController(
        NeptuneDbContext dbContext,
        ILogger<LoadGeneratingUnitController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : base(dbContext, logger, keystoneService, neptuneConfiguration)
    {
    }

    [HttpGet]
    [AdminFeature]
    public async Task<ActionResult<List<LoadGeneratingUnitGridDto>>> List()
    {
        var dtos = await LoadGeneratingUnits.ListAsGridDtoAsync(DbContext);
        return Ok(dtos);
    }

    [HttpGet("{loadGeneratingUnitID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(LoadGeneratingUnit), "loadGeneratingUnitID")]
    public async Task<ActionResult<LoadGeneratingUnitDto>> Get([FromRoute] int loadGeneratingUnitID)
    {
        var dto = await LoadGeneratingUnits.GetByIDAsDtoAsync(DbContext, loadGeneratingUnitID);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpGet("{loadGeneratingUnitID}/hru-characteristics")]
    [EntityNotFound(typeof(LoadGeneratingUnit), "loadGeneratingUnitID")]
    [AdminFeature]
    public async Task<ActionResult<List<HRUCharacteristicDto>>> ListHRUCharacteristics([FromRoute] int loadGeneratingUnitID)
    {
        var hruCharacteristics = await vHRUCharacteristics.ListByLoadGeneratingUnitAsGridDtoAsync(DbContext, loadGeneratingUnitID);
        return Ok(hruCharacteristics);
    }
}
