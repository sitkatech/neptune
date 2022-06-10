using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Hippocamp.API.Services;
using Hippocamp.API.Services.Authorization;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hippocamp.API.Controllers
{
    [ApiController]

    public class ProjectController : SitkaController<ProjectController>
    {
        private readonly HttpClient _neptuneClient;
        private readonly IWebHostEnvironment _environment;

        public ProjectController(HippocampDbContext dbContext, ILogger<ProjectController> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration, IHttpClientFactory httpClientFactory, IWebHostEnvironment environment ) : base(dbContext, logger, keystoneService, hippocampConfiguration)
        {
            _neptuneClient = httpClientFactory.CreateClient("NeptuneClient");
            _environment = environment;
        }

        [HttpGet("projects/{projectID}")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectSimpleDto> GetByID([FromRoute] int projectID)
        {
            var personDto = UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
            var projectSimpleDto = Projects.GetByIDAsSimpleDto(_dbContext, projectID);
            if (!UserCanEditJurisdiction(personDto, projectSimpleDto.StormwaterJurisdictionID))
            {
                return Forbid();
            }
            return Ok(projectSimpleDto);
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
                return Forbid();
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
                    HttpContext);
            
            _dbContext.FileResources.Add(fileResource);

            var projectDocument = ProjectDocuments.Create(_dbContext, projectDocumentUpsertDto, fileResource);

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
            var projectDocument = ProjectDocuments.GetProjectDocumentsImpl(_dbContext).SingleOrDefault(x => x.ProjectDocumentID == attachmentID);
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
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectNetworkSolveHistorySimpleDto>> GetProjectNetworkSolveHistoriesForProject(int projectID)
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

            var projectNetworkSolveHistoryDtos = ProjectNetworkSolveHistories.GetByProjectIDAsDto(_dbContext, projectID);
            return Ok(projectNetworkSolveHistoryDtos);
        }

        [HttpGet("projects/{projectID}/treatment-bmp-hru-characteristics")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPHRUCharacteristicsSummarySimpleDto>> GetTreatmentBMPHRUCharacteristicsForProject([FromRoute] int projectID)
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

            var hruCharacteristics = Projects.GetTreatmentBMPHRUCharacteristicSimplesForProject(_dbContext, projectID);

            return Ok(hruCharacteristics);
        }

        [HttpGet("projects/{projectID}/load-reducing-results")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectLoadReducingResultDto>> GetLoadRemovingResultsForProject([FromRoute] int projectID)
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

            var modeledResults = ProjectLoadReducingResults.ListByProjectIDAsDto(_dbContext, projectID);

            return Ok(modeledResults);
        }

        [HttpGet("projects/{projectID}/load-generating-results")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectLoadGeneratingResultDto>> GetLoadGeneratingResultsForProject([FromRoute] int projectID)
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

            var requestBaseURL = _hippocampConfiguration.OcStormwaterToolsModelingBaseUrl;
            //Necessary for circumnavigating the container accessing localhost issue
            if (_environment.IsDevelopment())
            {
                requestBaseURL = "http://host.docker.internal";
            }

            var result = _neptuneClient.PostAsync($"{requestBaseURL}/Nereid/NetworkSolveForProject/{projectID}", new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json")).Result;
            var body = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode)
            {
                return StatusCode((int)result.StatusCode, body);
            }

            return Ok();
        }

        [HttpGet("projects/{projectID}/delineations")]
        [JurisdictionEditFeature]
        public ActionResult<DelineationUpsertDto> GetDelineationsByProjectID([FromRoute] int projectID)
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

            Delineations.MergeDelineations(_dbContext, delineationUpsertDtos, project);

            return Ok();
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

        [HttpGet("projects/{projectID}/OCTAM2Tier2GrantScores")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectGrantScoreDto> GetGrantScoresByProjectID([FromRoute] int projectID)
        {
            var projectGrantScoreDto = _dbContext.vProjectGrantScores.SingleOrDefault(x => x.ProjectID == projectID)?.AsDto();
            return Ok(projectGrantScoreDto);
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram")]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectHRUCharacteristicsSummaryDto>> GetProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectHruCharacteristicsSummaryDtos = Projects.ListOCTAM2Tier2Projects(_dbContext)
                .Select(x => x.AsProjectHRUCharacteristicsSummaryDto()).ToList();
            var projectIDs = projectHruCharacteristicsSummaryDtos.Select(x => x.ProjectID);
            var projectGrantScores = _dbContext.vProjectGrantScores.Where(x => projectIDs.Contains(x.ProjectID)).ToList();
            foreach (var projectHruCharacteristicsSummaryDto in projectHruCharacteristicsSummaryDtos)
            {
                var projectGrantScore =
                    projectGrantScores.SingleOrDefault(x => x.ProjectID == projectHruCharacteristicsSummaryDto.ProjectID);
                if (projectGrantScore != null)
                {
                    projectHruCharacteristicsSummaryDto.SEA = projectGrantScore.SEA;
                    projectHruCharacteristicsSummaryDto.TPI = projectGrantScore.TPI;
                    projectHruCharacteristicsSummaryDto.DryWeatherWQLRI = projectGrantScore.DryWeatherWQLRI;
                    projectHruCharacteristicsSummaryDto.WetWeatherWQLRI = projectGrantScore.WetWeatherWQLRI;
                }
            }
            return Ok(projectHruCharacteristicsSummaryDtos);
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/download")]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2Projects(_dbContext).Select(x => x.ProjectID).ToList();
            var records = Projects.ListByIDsAsModeledResultSummaryDtos(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);

            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectModeledResultsSummaryMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/treatmentBMPs")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListTreatmentBMPsForProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2Projects(_dbContext).Select(x => x.ProjectID).ToList();
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByProjectIDsAsDisplayDto(_dbContext, projectIDs);

            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("projects/OCTAM2Tier2GrantProgram/treatmentBMPs/download")]
        [JurisdictionEditFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadTreatmentBMPsForProjectsSharedWithOCTAM2Tier2GrantProgram()
        {

            var projectIDs = Projects.ListOCTAM2Tier2Projects(_dbContext).Select(x => x.ProjectID).ToList();
            var records = ProjectLoadReducingResults.ListByProjectIDsAsDto(_dbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectLoadRemovingResultMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }
    }
}