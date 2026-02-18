using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.API.Services.Authorization;

namespace Neptune.API.Controllers;

[ApiController]
[Route("land-use-blocks")]
public class LandUseBlockController(
    NeptuneDbContext dbContext,
    ILogger<LandUseBlockController> logger,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<LandUseBlockController>(dbContext, logger, neptuneConfiguration)
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<LandUseBlockGridDto>> List()
    {
        var landUseBlockGridDtos = LandUseBlocks.List(dbContext);
        return landUseBlockGridDtos;
    }

    [HttpPut]
    [AdminFeature]
    public async Task<IActionResult> Update(int landUseBlockID, LandUseBlockUpsertDto landUseBlockUpsertDto)
    {
        var landUseBlock = LandUseBlocks.GetByIDWithChangeTracking(dbContext, landUseBlockID);
        await LandUseBlocks.Update(DbContext, landUseBlock, landUseBlockUpsertDto, CallingUser.PersonID);
        return Ok();
    }
}