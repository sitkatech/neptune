using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
public class RegionalSubbasinController(
    NeptuneDbContext dbContext,
    ILogger<RegionalSubbasinController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<RegionalSubbasinController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpPost("/graph-trace-as-feature-collection-from-point")]
    public ActionResult<FeatureCollection> GetRegionalSubbasinGraphTraceAsFeatureCollectionFromPoint([FromBody] CoordinateDto coordinateDto)
    {
        var featureCollection = RegionalSubbasins.GetRegionalSubbasinGraphTraceAsFeatureCollection(dbContext, coordinateDto);
        return Ok(featureCollection);
    }
}