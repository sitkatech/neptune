using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class RoleController : SitkaController<RoleController>
    {
        public RoleController(HippocampDbContext dbContext, ILogger<RoleController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("roles")]
        [AdminFeature]
        public ActionResult<List<RoleDto>> GetAllRoles()
        {
            var roleDtos = Role.List(_dbContext);
            return Ok(roleDtos);
        }
    }
}