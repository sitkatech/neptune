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
        [AdminFeature]
        public ActionResult<List<ProjectSimpleDto>> GetAllProjects()
        {
            var projectSimpleDtos = Projects.ListAsSimpleDtos(_dbContext);
            return Ok(projectSimpleDtos);
        }

        [HttpGet("projects/{personID}")]
        [JurisdictionManagerOrEditorFeature]
        public ActionResult<List<ProjectSimpleDto>> GetProjectsByPersonID([FromRoute] int personID)
        {
            var projectSimpleDtos = Projects.ListByPersonIDAsSimpleDtos(_dbContext, personID);
            return Ok(projectSimpleDtos);
        }
    }
}