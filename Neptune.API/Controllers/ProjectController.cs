﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Neptune.Models.DataTransferObjects;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using DelineationSimpleDto = Neptune.Models.DataTransferObjects.DelineationSimpleDto;
using ProjectDocumentSimpleDto = Neptune.Models.DataTransferObjects.ProjectDocumentSimpleDto;
using ProjectSimpleDto = Neptune.Models.DataTransferObjects.ProjectSimpleDto;

namespace Neptune.API.Controllers
{
    [ApiController]

    public class ProjectController : SitkaController<ProjectController>
    {
        private readonly HttpClient _neptuneClient;
        private readonly IWebHostEnvironment _environment;
        private readonly AzureBlobStorageService _blobStorageService;

        public ProjectController(NeptuneDbContext dbContext,
            ILogger<ProjectController> logger,
            KeystoneService keystoneService,
            IOptions<NeptuneConfiguration> neptuneConfiguration,
            IHttpClientFactory httpClientFactory, 
            IWebHostEnvironment environment,
            AzureBlobStorageService blobStorageService) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
            _neptuneClient = httpClientFactory.CreateClient("NeptuneClient");
            _environment = environment;
            _blobStorageService = blobStorageService;
        }

        [HttpGet("projects/{projectID}")]
        [UserViewFeature]
        public ActionResult<ProjectSimpleDto> GetByID([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectSimpleDto = Projects.GetByIDAsSimpleDto(_dbContext, projectID);

            if (personDto.IsOCTAGrantReviewer && projectSimpleDto.ShareOCTAM2Tier2Scores)
            {
                return Ok(projectSimpleDto);
            }
            if (UserCanEditJurisdiction(personDto, projectSimpleDto.StormwaterJurisdictionID))
            {
                return Ok(projectSimpleDto);
            }
            return Forbid();
        }

        [HttpGet("projects")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectSimpleDto>> ListByPersonID()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectSimpleDtos = Projects.ListByPersonIDAsSimpleDto(_dbContext, personDto.PersonID);
            return Ok(projectSimpleDtos);
        }

        [HttpPost("projects/new")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectSimpleDto> New([FromBody] ProjectUpsertDto projectCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID.Value))
            {
                return Forbid();
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
        public IActionResult Update([FromRoute] int projectID, [FromBody] ProjectUpsertDto projectCreateDto)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectCreateDto.StormwaterJurisdictionID.Value))
            {
                return Forbid();
            }
            Projects.Update(_dbContext, project, projectCreateDto, personDto.PersonID);
            return Ok();
        }

        [HttpGet("projects/{projectID}/attachments")]
        [UserViewFeature]
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

        [HttpGet("projects/attachments/{attachmentID}")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectDocumentSimpleDto> GetAttachmentByID([FromRoute] int attachmentID)
        {
            var projectDocument = ProjectDocuments.GetByID(_dbContext, attachmentID);
            return Ok(projectDocument.AsSimpleDto());
        }

        [HttpPost("projects/{projectID}/attachments")]
        [RequestSizeLimit(30 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 30 * 1024 * 1024), ProducesResponseType(StatusCodes.Status413RequestEntityTooLarge)]
        [JurisdictionEditFeature]
        public async Task<ActionResult<ProjectDocumentSimpleDto>> AddAttachment([FromRoute] int projectID, [FromForm] ProjectDocumentUpsertDto projectDocumentUpsertDto)
        {
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }

            var fileResource =
                await HttpUtilities.MakeFileResourceFromFormFile(projectDocumentUpsertDto.FileResource, _dbContext,
                    HttpContext, _blobStorageService);

            var projectDocument = new ProjectDocument()
            {
                ProjectID = projectDocumentUpsertDto.ProjectID,
                FileResource = fileResource,
                DisplayName = projectDocumentUpsertDto.DisplayName,
                DocumentDescription = projectDocumentUpsertDto.DocumentDescription,
                UploadDate = DateTime.Now
            };

            _dbContext.ProjectDocuments.Add(projectDocument);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(projectDocument).ReloadAsync();

            return Ok(projectDocument.AsSimpleDto());
        }

        [HttpPut("projects/attachments/{attachmentID}")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectDocumentSimpleDto> UpdateAttachment([FromRoute] int attachmentID, [FromBody] ProjectDocumentUpdateDto projectDocumentUpdateDto)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectDocument = ProjectDocuments.GetByIDWithTracking(_dbContext, attachmentID);
            if (ThrowNotFound(projectDocument, "ProjectDocument", attachmentID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var updatedProjectDocument = ProjectDocuments.Update(_dbContext, projectDocument, projectDocumentUpdateDto);

            return Ok(updatedProjectDocument.AsSimpleDto());
        }

        [HttpDelete("projects/attachments/{attachmentID}")]
        [JurisdictionEditFeature]
        public IActionResult DeleteAttachment([FromRoute] int attachmentID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectDocument = ProjectDocuments.GetByIDWithTracking(_dbContext, attachmentID);
            if (ThrowNotFound(projectDocument, "ProjectDocument", attachmentID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, projectDocument.Project.StormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return Forbid();
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
                return Forbid();
            }
            Projects.Delete(_dbContext, projectID);
            return Ok();
        }

        [HttpGet("projects/{projectID}/project-network-solve-histories")]
        [UserViewFeature]
        public ActionResult<List<ProjectNetworkSolveHistorySimpleDto>> GetProjectNetworkSolveHistoriesForProject(int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if ((!personDto.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var projectNetworkSolveHistoryDtos = ProjectNetworkSolveHistories.GetByProjectIDAsDto(_dbContext, projectID);
            return Ok(projectNetworkSolveHistoryDtos);
        }

        [HttpGet("projects/{projectID}/treatment-bmp-hru-characteristics")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPHRUCharacteristicsSummarySimpleDto>> GetTreatmentBMPHRUCharacteristicsForProject([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if ((!personDto.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var hruCharacteristics = Projects.GetTreatmentBMPHRUCharacteristicSimplesForProject(_dbContext, projectID);

            return Ok(hruCharacteristics);
        }

        [HttpGet("projects/{projectID}/load-reducing-results")]
        [UserViewFeature]
        public ActionResult<List<ProjectLoadReducingResultDto>> GetLoadRemovingResultsForProject([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if ((!personDto.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var modeledResults = ProjectLoadReducingResults.ListByProjectIDAsDto(_dbContext, projectID);

            return Ok(modeledResults);
        }

        [HttpGet("projects/{projectID}/load-generating-results")]
        [UserViewFeature]
        public ActionResult<List<ProjectLoadGeneratingResultDto>> GetLoadGeneratingResultsForProject([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if ((!personDto.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var modeledResults = ProjectLoadGeneratingResults.ListByProjectIDAsDto(_dbContext, projectID);

            return Ok(modeledResults);
        }

        [HttpPost("projects/{projectID}/modeled-performance")]
        [JurisdictionEditFeature]
        public IActionResult TriggerModeledPerformanceForProject([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (ThrowNotFound(project, "Project", projectID, out var actionResult))
            {
                return actionResult;
            }
            if (!UserCanEditJurisdiction(personDto, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var requestObject = new
            {
                webServiceAccessTokenGuidAsString = personDto.WebServiceAccessToken.ToString()
            };

            var requestBaseURL = _neptuneConfiguration.OcStormwaterToolsModelingBaseUrl;
            //Necessary for circumnavigating the container accessing localhost issue
            if (_environment.IsDevelopment())
            {
                requestBaseURL = "http://host.docker.internal";
            }

            var result = _neptuneClient.PostAsync($"{requestBaseURL}/Nereid/NetworkSolveForProject/{projectID}", new StringContent(GeoJsonSerializer.Serialize(requestObject), Encoding.UTF8, "application/json")).Result;
            var body = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode)
            {
                return StatusCode((int)result.StatusCode, body);
            }

            return Ok();
        }

        [HttpGet("projects/{projectID}/delineations")]
        [UserViewFeature]
        public ActionResult<List<DelineationUpsertDto>> GetDelineationsByProjectID([FromRoute] int projectID)
        {
            var DelineationUpsertDtos = Delineations.ListByProjectIDAsUpsertDto(_dbContext, projectID);
            return Ok(DelineationUpsertDtos);
        }

        [HttpPut("projects/{projectID}/delineations")]
        [JurisdictionEditFeature]
        public ActionResult MergeDelineationsForProject(List<DelineationUpsertDto> delineationUpsertDtos, [FromRoute] int projectID)
        {
            // project validation here
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            Projects.DeleteProjectNereidResultsAndGrantScores(_dbContext, projectID);
            Delineations.MergeDelineations(_dbContext, delineationUpsertDtos, project);

            var personID = UserContext.GetUserFromHttpContext(_dbContext, HttpContext).PersonID;
            Projects.SetUpdatePersonAndDate(_dbContext, projectID, personID);

            return Ok();
        }

        [HttpPost("projects/{projectID}/copy")]
        [JurisdictionEditFeature]
        public ActionResult<int> CreateProjectCopy([FromRoute] int projectID)
        {
            var person = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var project = Projects.GetByID(_dbContext, projectID);
            if (!UserCanEditJurisdiction(person, project.StormwaterJurisdictionID))
            {
                return Forbid();
            }

            var newProject = Projects.CreateCopy(_dbContext, project, person.PersonID);
            return Ok(newProject.ProjectID);
        }

        [HttpGet("projects/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadProjectsWithModels()
        {
            var projectIDs = Projects.ListProjectIDs(_dbContext);
            var records = Projects.ListByIDsAsModeledResultSummaryDtos(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectModeledResultsSummaryMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("projects/treatmentBMPs/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadTreatmentBMPsForProjects()
        {
            var projectIDs = Projects.ListProjectIDs(_dbContext);
            var records = ProjectLoadReducingResults.ListByProjectIDs(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectLoadRemovingResultMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        private bool UserCanEditJurisdiction(PersonDto personDto, int stormwaterJurisdictionID)
        {
            if (personDto.Role.RoleID == (int) RoleEnum.Admin || personDto.Role.RoleID == (int) RoleEnum.SitkaAdmin )
            {
                return true;
            }

            if (personDto.Role.RoleID == (int) RoleEnum.JurisdictionEditor || personDto.Role.RoleID == (int) RoleEnum.JurisdictionManager)
            {
                var stormwaterJurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonDto(_dbContext, personDto);
                return stormwaterJurisdictionIDs.Contains(stormwaterJurisdictionID);
            }

            return false;
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram")]
        [UserViewFeature]
        public ActionResult<List<ProjectSimpleDto>> GetProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var currentUser = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!currentUser.IsOCTAGrantReviewer)
            {
                return Forbid();
            }

            var projectHruCharacteristicsSummaryDtos = Projects.ListOCTAM2Tier2Projects(_dbContext)
                .Select(x => x.AsSimpleDto()).ToList();
            return Ok(projectHruCharacteristicsSummaryDtos);
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2ProjectIDs(_dbContext);
            var records = Projects.ListByIDsAsModeledResultSummaryDtos(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectModeledResultsSummaryMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/treatmentBMPs")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListTreatmentBMPsForProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var currentUser = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!currentUser.IsOCTAGrantReviewer)
            {
                return Forbid();
            }

            var projectIDs = Projects.ListOCTAM2Tier2ProjectIDs(_dbContext);
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByProjectIDsAsDisplayDto(_dbContext, projectIDs);

            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/treatmentBMPs/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadTreatmentBMPsForProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2ProjectIDs(_dbContext);
            var records = ProjectLoadReducingResults.ListByProjectIDs(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectLoadRemovingResultMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("projects/delineations")]
        [UserViewFeature]
        public ActionResult<List<DelineationSimpleDto>> List()
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            if (!personDto.IsOCTAGrantReviewer)
            {
                return Forbid();
            }
            var delineationsSimpleDtos = Delineations.ListProjectDelineationsAsSimpleDto(_dbContext);
            return Ok(delineationsSimpleDtos);
        }

    }
}