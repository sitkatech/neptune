using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class SystemInfoController : SitkaController<SystemInfoController>
    {
        private readonly IOptions<NeptuneConfiguration> _neptuneConfiguration;

        public SystemInfoController(NeptuneDbContext dbContext, ILogger<SystemInfoController> logger, KeystoneService keystoneService, IOptions<NeptuneConfiguration> neptuneConfiguration)
            : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
            _neptuneConfiguration = neptuneConfiguration;
        }

        [HttpGet("/", Name = "GetSystemInfo")]  // MCS: the pattern seems to be to allow anonymous access to this endpoint
        [AllowAnonymous]
        public ActionResult<SystemInfoDto> GetSystemInfo([FromServices] IWebHostEnvironment environment)
        {
            var systemInfo = new SystemInfoDto
            {
                Environment = environment.EnvironmentName,
                CurrentTimeUTC = DateTime.UtcNow.ToString("o"),
            };

            return Ok(systemInfo);
        }

    }
}