using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

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
    public async Task<ActionResult<List<LoadGeneratingUnitDto>>> List()
    {
        var dtos = await LoadGeneratingUnits.ListAsDtoAsync(DbContext);
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

    [HttpPost]
    [AdminFeature]
    public async Task<ActionResult<LoadGeneratingUnitDto>> Create([FromBody] LoadGeneratingUnitDto dto)
    {
        var created = await LoadGeneratingUnits.CreateAsync(DbContext, dto);
        return CreatedAtAction(nameof(Get), new { loadGeneratingUnitID = created.LoadGeneratingUnitID }, created);
    }

    [HttpPut("{loadGeneratingUnitID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(LoadGeneratingUnit), "loadGeneratingUnitID")]
    public async Task<ActionResult<LoadGeneratingUnitDto>> Update([FromRoute] int loadGeneratingUnitID, [FromBody] LoadGeneratingUnitDto dto)
    {
        var updated = await LoadGeneratingUnits.UpdateAsync(DbContext, loadGeneratingUnitID, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{loadGeneratingUnitID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(LoadGeneratingUnit), "loadGeneratingUnitID")]
    public async Task<IActionResult> Delete([FromRoute] int loadGeneratingUnitID)
    {
        var deleted = await LoadGeneratingUnits.DeleteAsync(DbContext, loadGeneratingUnitID);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
