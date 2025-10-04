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
using NetTopologySuite.Features;

namespace Neptune.API.Controllers;

[ApiController]
[Route("regional-subbasins")]
public class RegionalSubbasinController : SitkaController<RegionalSubbasinController>
{
    public RegionalSubbasinController(
        NeptuneDbContext dbContext,
        ILogger<RegionalSubbasinController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : base(dbContext, logger, keystoneService, neptuneConfiguration)
    {
    }

    [HttpGet]
    [AdminFeature]
    public async Task<ActionResult<List<RegionalSubbasinDto>>> List()
    {
        var regionalSubbasins = await RegionalSubbasins.ListAsDtoAsync(DbContext);
        return Ok(regionalSubbasins);
    }

    [HttpGet("{regionalSubbasinID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(RegionalSubbasin), "regionalSubbasinID")]
    public async Task<ActionResult<RegionalSubbasinDto>> Get([FromRoute] int regionalSubbasinID)
    {
        var regionalSubbasin = await RegionalSubbasins.GetByIDAsDtoAsync(DbContext, regionalSubbasinID);
        if (regionalSubbasin == null) return NotFound();
        return Ok(regionalSubbasin);
    }

    [HttpPost]
    [AdminFeature]
    public async Task<ActionResult<RegionalSubbasinDto>> Create([FromBody] RegionalSubbasinUpsertDto dto)
    {
        var created = await RegionalSubbasins.CreateAsync(DbContext, dto);
        return CreatedAtAction(nameof(Get), new { regionalSubbasinID = created.RegionalSubbasinID }, created);
    }

    [HttpPut("{regionalSubbasinID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(RegionalSubbasin), "regionalSubbasinID")]
    public async Task<ActionResult<RegionalSubbasinDto>> Update([FromRoute] int regionalSubbasinID, [FromBody] RegionalSubbasinUpsertDto dto)
    {
        var updated = await RegionalSubbasins.UpdateAsync(DbContext, regionalSubbasinID, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{regionalSubbasinID}")]
    [AdminFeature]
    [EntityNotFoundAttribute(typeof(RegionalSubbasin), "regionalSubbasinID")]
    public async Task<IActionResult> Delete([FromRoute] int regionalSubbasinID)
    {
        var deleted = await RegionalSubbasins.DeleteAsync(DbContext, regionalSubbasinID);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPost("/graph-trace-as-feature-collection-from-point")]
    public ActionResult<FeatureCollection> GetRegionalSubbasinGraphTraceAsFeatureCollectionFromPoint([FromBody] CoordinateDto coordinateDto)
    {
        var featureCollection = RegionalSubbasins.GetRegionalSubbasinGraphTraceAsFeatureCollection(DbContext, coordinateDto);
        return Ok(featureCollection);
    }
}