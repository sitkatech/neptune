using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System.Linq;
using System.Threading.Tasks;
using Neptune.Models.Helpers;

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
        var userDto = await People.GetByGlobalIDAsDtoAsync(DbContext, globalID);
        if (userDto == null)
        {
            var notFoundMessage = $"User with GlobalID {globalID} does not exist!";
            Logger.LogError(notFoundMessage);
            return NotFound(notFoundMessage);
        }

        return Ok(userDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PersonDto>> PostUserClaims([FromServices] HttpContext httpContext)
    {
        var claimsPrincipal = httpContext.User;
        if (!claimsPrincipal.Claims.Any())  // Updating user based on claims does not work when there are no claims
        {
            return BadRequest();
        }

        PersonDto claimsUserDto = null;
        var isClient = claimsPrincipal.Claims.Any(c => c.Type == ClaimsConstants.IsClient);
        if (isClient)  // Not appropriate to actually update client user based on claims
        {
            var clientID = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == ClaimsConstants.ClientID)?.Value;
            if (!string.IsNullOrEmpty(clientID))
            {
                claimsUserDto = await People.GetByGlobalIDAsDtoAsync(dbContext, clientID);
            }
            return Ok(claimsUserDto);
        }

        var subClaim = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == ClaimsConstants.Sub)?.Value;
        if (!string.IsNullOrEmpty(subClaim))
        {
            claimsUserDto = await People.GetByGlobalIDAsDtoAsync(dbContext, subClaim);
        }

        var updatedUserDto = await People.UpdateClaims(dbContext, claimsUserDto?.PersonID, claimsPrincipal);

        return Ok(updatedUserDto);
    }

}