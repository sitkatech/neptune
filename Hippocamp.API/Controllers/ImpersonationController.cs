using System;
using System.Threading.Tasks;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hippocamp.API.Controllers
{
    [ApiController]
    public class ImpersonationController : SitkaController<ImpersonationController>
    {
        private readonly ImpersonationService _impersonationService;

        public ImpersonationController(HippocampDbContext dbContext, ILogger<ImpersonationController> logger,
            KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration, ImpersonationService impersonationService) : base(dbContext,
            logger, keystoneService, hippocampConfiguration)
        {
            _impersonationService = impersonationService;
        }
        
        [HttpPost("/impersonate/{userID}")]
        [AdminFeature]
        public ActionResult<UserDto> ImpersonateUser([FromRoute] int userID)
        {
            var userToImpersonate = EFModels.Entities.User.GetByUserID(_dbContext, userID);

            return Ok(_impersonationService.ImpersonateUser(HttpContext, userToImpersonate));
        }

        [HttpPost("/impersonate/stop-impersonation")]
        [LoggedInUnclassifiedFeature]
        public ActionResult<UserDto> StopImpersonation()
        {
            return Ok(_impersonationService.StopImpersonation(HttpContext));
        }
    }
}