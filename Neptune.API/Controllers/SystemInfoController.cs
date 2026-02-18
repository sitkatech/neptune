using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using System;

namespace Neptune.API.Controllers
{
    [ApiController]
    public class SystemInfoController(
        NeptuneDbContext dbContext,
        ILogger<SystemInfoController> logger,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : SitkaController<SystemInfoController>(dbContext, logger, neptuneConfiguration)
    {
        [Route("/")] // Default Route
        [AllowAnonymous]
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