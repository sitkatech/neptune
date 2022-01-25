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

        [HttpGet("projects/{personID}")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectSimpleDto>> ListByPersonID([FromRoute] int personID)
        {
            var projectSimpleDtos = Projects.ListByPersonIDAsSimpleDto(_dbContext, personID);
            return Ok(projectSimpleDtos);
        }

        [HttpPost("projects/new")]
        [JurisdictionEditFeature]
        public IActionResult New([FromBody] ProjectCreateDto projectCreateDto)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            Projects.CreateNew(_dbContext, projectCreateDto, personDto);
            return Ok();
        }

        [HttpPost("projects/{projectID}/update")]
        [JurisdictionEditFeature]
        public IActionResult Update([FromRoute] int projectID, [FromBody] ProjectCreateDto projectCreateDto)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            Projects.Update(_dbContext, projectID, projectCreateDto);
            return Ok();
        }

        [HttpPost("projects/{projectID}/delete")]
        [JurisdictionEditFeature]
        public IActionResult Delete([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);

            if (!UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            Projects.Delete(_dbContext, project);
            return Ok();
        }

        private bool UserCanEditJurisdiction(PersonDto personDto, int stormwaterJurisdictionID)
        {
            if (personDto.Role.RoleID == (int) RoleEnum.JurisdictionEditor || personDto.Role.RoleID == (int) RoleEnum.JurisdictionManager)
            {
                var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonDto(_dbContext, personDto);
                return stormwaterJurisdictionIDs.Contains(stormwaterJurisdictionID);
            }
            return true;
        }
    }
}