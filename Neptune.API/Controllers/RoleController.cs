using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class RoleController : SitkaController<RoleController>
    {
        public RoleController(NeptuneDbContext dbContext, ILogger<RoleController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
        }

        [HttpGet("roles")]
        [AdminFeature]
        public ActionResult<List<RoleSimpleDto>> GetAllRoles()
        {
            var roleDtos = Role.AllAsSimpleDto;
            return Ok(roleDtos);
        }
    }
}