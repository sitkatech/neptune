using System.Collections.Generic;
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
    public class ProjectController : SitkaController<ProjectController>
    {
        public ProjectController(HippocampDbContext dbContext, ILogger<ProjectController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("projects")]
        public ActionResult<List<ProjectDto>> getAllProjects()
        {
            var projectDtos = Project.ListAsDtos(_dbContext);
            return Ok(projectDtos);
        }
    }
}