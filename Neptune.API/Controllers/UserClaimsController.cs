using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers;

[ApiController]
[Route("user-claims")]
public class UserClaimsController(
    NeptuneDbContext dbContext,
    ILogger<UserClaimsController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<UserClaimsController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet("{globalID}")]
    public ActionResult<PersonDto> GetByGlobalID([FromRoute] string globalID)
    {
        var isValidGuid = Guid.TryParse(globalID, out var globalIDAsGuid);
        if (!isValidGuid)
        {
            return BadRequest();
        }

        var userDto = People.GetByGuidAsDto(DbContext, globalIDAsGuid);
        if (userDto == null)
        {
            var notFoundMessage = $"User with GUID {globalIDAsGuid} does not exist!";
            Logger.LogError(notFoundMessage);
            return NotFound(notFoundMessage);
        }

        return Ok(userDto);
    }
}