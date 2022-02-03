using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ProjectController : SitkaController<ProjectController>
    {
        public ProjectController(HippocampDbContext dbContext, ILogger<ProjectController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
        }

        [HttpGet("projects/{projectID}")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectSimpleDto> GetByID([FromRoute] int projectID)
        {
            var projectSimpleDto = Projects.GetByIDAsSimpleDto(_dbContext, projectID);
            return Ok(projectSimpleDto);
        }

        [HttpGet("projects/{personID}/listByPersonID")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectSimpleDto>> ListByPersonID([FromRoute] int personID)
        {
            var projectSimpleDtos = Projects.ListByPersonIDAsSimpleDto(_dbContext, personID);
            return Ok(projectSimpleDtos);
        }

        [HttpPost("projects/new")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectSimpleDto> New([FromBody] ProjectCreateDto projectCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID.Value))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }

            var projectNameAlreadyExists = _dbContext.Projects.Any(x => x.ProjectName == projectCreateDto.ProjectName);
            if (projectNameAlreadyExists)
            {
                ModelState.AddModelError("ProjectName", $"A project with the name {projectCreateDto.ProjectName} already exists");
                return BadRequest(ModelState);
            }
            var project = Projects.CreateNew(_dbContext, projectCreateDto, personDto);
            return Ok(project);
        }

        [HttpPost("projects/{projectID}/update")]
        [JurisdictionEditFeature]
        public IActionResult Update([FromRoute] int projectID, [FromBody] ProjectCreateDto projectCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID.Value))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            Projects.Update(_dbContext, project, projectCreateDto);
            return Ok();
        }

        [HttpGet("projects/{projectID}/attachments")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectDocumentSimpleDto>> ListAttachmentsByProjectID([FromRoute] int projectID)
        {
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            var projectDocuments = ProjectDocuments.ListByProjectIDAsSimpleDto(_dbContext, projectID);
            return Ok(projectDocuments);
        }

        [HttpPost("projects/{projectID}/attachments")]
        [RequestSizeLimit(10L * 1024L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L)]
        [JurisdictionEditFeature]
        public async Task<ActionResult<ProjectDocumentSimpleDto>> AddAttachment([FromRoute] int projectID, [FromForm] ProjectDocumentUpsertDto projectDocumentUpsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projectDocumentUpsertDto.DisplayName == null || projectDocumentUpsertDto.DisplayName == "null" || projectDocumentUpsertDto.DisplayName == "undefined")
            {
                return BadRequest("Display Name is required");
            }

            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }

            var fileResource =
                await HttpUtilities.MakeFileResourceFromFormFile(projectDocumentUpsertDto.FileResource, _dbContext,
                    HttpContext);
            
            _dbContext.FileResources.Add(fileResource);

            var projectDocument = ProjectDocuments.Create(_dbContext, projectDocumentUpsertDto, fileResource);

            return Ok(projectDocument.AsSimpleDto());
        }

        [HttpPut("projects/attachments/{attachmentID}")]
        [RequestSizeLimit(10L * 1024L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L)]
        [JurisdictionEditFeature]
        public async Task<ActionResult<ProjectDocumentSimpleDto>> UpdateAttachment([FromRoute] int attachmentID, [FromForm] ProjectDocumentUpsertDto projectDocumentUpsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projectDocumentUpsertDto.DisplayName == null || projectDocumentUpsertDto.DisplayName == "null" || projectDocumentUpsertDto.DisplayName == "undefined")
            {
                return BadRequest("Display Name is required");
            }

            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectDocument = ProjectDocuments.GetByID(_dbContext, attachmentID);
            if (ThrowNotFound(projectDocument, "ProjectDocument", attachmentID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            FileResource fileResource = null;
            if (projectDocumentUpsertDto.FileResource != null)
            {
                fileResource = await HttpUtilities.MakeFileResourceFromFormFile(projectDocumentUpsertDto.FileResource, _dbContext, HttpContext);
                _dbContext.FileResources.Add(fileResource);
            }

            var updatedProjectDocument = ProjectDocuments.Update(_dbContext, projectDocument, projectDocumentUpsertDto, fileResource);

            return Ok(updatedProjectDocument.AsSimpleDto());
        }

        [HttpDelete("projects/attachments/{attachmentID}")]
        [JurisdictionEditFeature]
        public IActionResult DeleteAttachment([FromRoute] int attachmentID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectDocument = _dbContext.ProjectDocuments.SingleOrDefault(x => x.ProjectDocumentID == attachmentID);
            if (ThrowNotFound(projectDocument, "ProjectDocument", attachmentID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return Forbid("You are not authorized to edit projects within this jurisdiction.");
            }
            ProjectDocuments.Delete(_dbContext, projectDocument);
            return Ok();
        }

        [HttpDelete("projects/{projectID}/delete")]
        [JurisdictionEditFeature]
        public IActionResult Delete([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
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