using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers;

[ApiController]
[Route("organization-types")]
public class OrganizationTypeController(NeptuneDbContext dbContext, ILogger<OrganizationTypeController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<OrganizationTypeController>(dbContext, logger, keystoneService, neptuneConfiguration)
{
    [HttpGet]
    [AdminFeature]
    public async Task<ActionResult<List<OrganizationTypeSimpleDto>>> ListSimple()
    {
        var organizationTypeDtos = await OrganizationTypes.ListAsSimpleDtosAsync(DbContext);
        return Ok(organizationTypeDtos);
    }
}