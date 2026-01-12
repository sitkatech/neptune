using System;
using System.Threading.Tasks;
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
    public async Task<ActionResult<PersonDto>> GetByGlobalID([FromRoute] string globalID)
    {
        var userDto = await People.GetByAuth0IDAsDtoAsync(DbContext, globalID);
        if (userDto == null)
        {
            var notFoundMessage = $"User with Auth0ID {globalID} does not exist!";
            Logger.LogError(notFoundMessage);
            return NotFound(notFoundMessage);
        }

        return Ok(userDto);
    }
}