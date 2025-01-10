using System;
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
    public class SystemInfoController(
        NeptuneDbContext dbContext,
        ILogger<SystemInfoController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        Person callingUser)
        : SitkaController<SystemInfoController>(dbContext, logger, keystoneService, neptuneConfiguration, callingUser)
    {
        private readonly IOptions<NeptuneConfiguration> _neptuneConfiguration = neptuneConfiguration;

        [Route("/")] // Default Route
        [HttpGet]
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