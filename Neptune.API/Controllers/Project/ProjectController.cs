using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Hangfire;
using Neptune.Models.DataTransferObjects;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Workflows;
using Neptune.Jobs.Hangfire;
using Neptune.API.Services.Attributes;
using NetTopologySuite.Features;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController(
        NeptuneDbContext dbContext,
        ILogger<ProjectController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        AzureBlobStorageService azureBlobStorageService,
        Person callingUser)
        : SitkaController<ProjectController>(dbContext, logger, keystoneService, neptuneConfiguration, callingUser)
    {
        [HttpGet]
        [JurisdictionEditFeature]
        public ActionResult<List<ProjectDto>> ListByPersonID()
        {
            var projectDtos = Projects.ListByPersonIDAsDto(DbContext, CallingUser.PersonID);
            return Ok(projectDtos);
        }

        [HttpPost]
        [JurisdictionEditFeature]
        public async Task<ActionResult<ProjectDto>> New([FromBody] ProjectUpsertDto projectCreateDto)
        {
            if (!CallingUser.CanEditJurisdiction(projectCreateDto.StormwaterJurisdictionID.Value, DbContext))
            {
                return Forbid();
            }

            var projectNameAlreadyExists = DbContext.Projects.Any(x => x.ProjectName == projectCreateDto.ProjectName);
            if (projectNameAlreadyExists)
            {
                ModelState.AddModelError("ProjectName", $"A project with the name {projectCreateDto.ProjectName} already exists");
                return BadRequest(ModelState);
            }
            var project = await Projects.CreateNew(DbContext, projectCreateDto, CallingUser, CallingUser.PersonID);
            return Ok(project);
        }

        [HttpGet("{projectID}")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<ProjectDto> GetByID([FromRoute] int projectID)
        {
            var projectDto = Projects.GetByIDAsDto(DbContext, projectID);

            if (CallingUser.IsOCTAGrantReviewer && projectDto.ShareOCTAM2Tier2Scores)
            {
                return Ok(projectDto);
            }
            if (CallingUser.CanEditJurisdiction(projectDto.StormwaterJurisdictionID, DbContext))
            {
                return Ok(projectDto);
            }
            return Forbid();
        }

        [HttpGet("{projectID}/progress")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectWorkflowProgress.ProjectWorkflowProgressDto> GetProjectProgress([FromRoute] int projectID)
        {
            var project = Projects.GetByIDWithTrackingForWorkflow(DbContext, projectID);
            var projectWorkflowProgressDto = ProjectWorkflowProgress.GetProgress(project);
            return projectWorkflowProgressDto;
        }

        [HttpPost("{projectID}/update")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public async Task<IActionResult> Update([FromRoute] int projectID, [FromBody] ProjectUpsertDto projectCreateDto)
        {
            if (!CallingUser.CanEditJurisdiction(projectCreateDto.StormwaterJurisdictionID.Value, DbContext))
            {
                return Forbid();
            }
            var project = Projects.GetByIDWithChangeTracking(DbContext, projectID);
            await Projects.Update(DbContext, project, projectCreateDto, CallingUser.PersonID);
            return Ok();
        }

        [HttpGet("{projectID}/attachments")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<ProjectDocumentDto>> ListAttachmentsByProjectID([FromRoute] int projectID)
        {
            var projectDocuments = ProjectDocuments.ListByProjectIDAsDto(DbContext, projectID);
            return Ok(projectDocuments);
        }

        [HttpPost("{projectID}/attachments")]
        [EntityNotFound(typeof(Project), "projectID")]
        [RequestSizeLimit(30 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 30 * 1024 * 1024), ProducesResponseType(StatusCodes.Status413RequestEntityTooLarge)]
        [JurisdictionEditFeature]
        public async Task<ActionResult<ProjectDocumentDto>> AddAttachment([FromRoute] int projectID, [FromForm] ProjectDocumentUpsertDto projectDocumentUpsertDto)
        {
            var fileResource =
                await HttpUtilities.MakeFileResourceFromFormFile(projectDocumentUpsertDto.FileResource, DbContext,
                    HttpContext, azureBlobStorageService);

            var projectDocument = new ProjectDocument()
            {
                ProjectID = projectDocumentUpsertDto.ProjectID,
                FileResource = fileResource,
                DisplayName = projectDocumentUpsertDto.DisplayName,
                DocumentDescription = projectDocumentUpsertDto.DocumentDescription,
                UploadDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            DbContext.ProjectDocuments.Add(projectDocument);
            await DbContext.SaveChangesAsync();
            await DbContext.Entry(projectDocument).ReloadAsync();

            return Ok(projectDocument.AsDto());
        }

        [HttpDelete("{projectID}")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public async Task<IActionResult> Delete([FromRoute] int projectID)
        {
            var project = Projects.GetByID(DbContext, projectID);
            if (!CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }
            await Projects.Delete(DbContext, projectID);
            return Ok();
        }

        [HttpGet("{projectID}/treatment-bmps")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public ActionResult<List<TreatmentBMPDisplayDto>> ListTreatmentBMPsByProjectID([FromRoute] int projectID)
        {
            var featureCollection = TreatmentBMPs.ListByProjectIDsAsDisplayDto(DbContext, [projectID]);
            return Ok(featureCollection);
        }

        [HttpGet("{projectID}/project-network-solve-histories")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<ProjectNetworkSolveHistorySimpleDto>> GetProjectNetworkSolveHistoriesForProject([FromRoute] int projectID)
        {
            var project = Projects.GetByID(DbContext, projectID);
            if ((!CallingUser.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var projectNetworkSolveHistoryDtos = ProjectNetworkSolveHistories.GetByProjectIDAsDto(DbContext, projectID);
            return Ok(projectNetworkSolveHistoryDtos);
        }

        [HttpGet("{projectID}/treatment-bmp-hru-characteristics")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPHRUCharacteristicsSummarySimpleDto>> GetTreatmentBMPHRUCharacteristicsForProject([FromRoute] int projectID)
        {
            var project = Projects.GetByID(DbContext, projectID);
            if ((!CallingUser.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var hruCharacteristics = Projects.GetTreatmentBMPHRUCharacteristicSimplesForProject(DbContext, projectID);

            return Ok(hruCharacteristics);
        }

        [HttpGet("{projectID}/load-reducing-results")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<ProjectLoadReducingResultDto>> GetLoadRemovingResultsForProject([FromRoute] int projectID)
        {
            var project = Projects.GetByID(DbContext, projectID);
            if ((!CallingUser.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var modeledResults = ProjectLoadReducingResults.ListByProjectIDAsDto(DbContext, projectID);

            return Ok(modeledResults);
        }

        [HttpGet("{projectID}/load-generating-results")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<ProjectLoadGeneratingResultDto>> GetLoadGeneratingResultsForProject([FromRoute] int projectID)
        {
            var project = Projects.GetByID(DbContext, projectID);
            if ((!CallingUser.IsOCTAGrantReviewer || !project.ShareOCTAM2Tier2Scores) && !CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var modeledResults = ProjectLoadGeneratingResults.ListByProjectIDAsDto(DbContext, projectID);

            return Ok(modeledResults);
        }

        [HttpPost("{projectID}/modeled-performance")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public async Task<IActionResult> TriggerModeledPerformanceForProject([FromRoute] int projectID)
        {
            var project = Projects.GetByIDWithChangeTracking(DbContext, projectID);
            if (!CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var projectNetworkSolveHistoryEntity = new ProjectNetworkSolveHistory
            {
                ProjectID = projectID,
                RequestedByPersonID = CallingUser.PersonID,
                ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Queued,
                LastUpdated = DateTime.UtcNow
            };
            await DbContext.ProjectNetworkSolveHistories.AddAsync(projectNetworkSolveHistoryEntity);
            await DbContext.SaveChangesAsync();

            BackgroundJob.Enqueue<ProjectNetworkSolveJob>(x => x.RunNetworkSolveForProject(projectID, projectNetworkSolveHistoryEntity.ProjectNetworkSolveHistoryID));
            return Ok();
        }

        [HttpGet("{projectID}/delineations")]
        [EntityNotFound(typeof(Project), "projectID")]
        [UserViewFeature]
        public ActionResult<List<DelineationUpsertDto>> GetDelineationsByProjectID([FromRoute] int projectID)
        {
            var delineationUpsertDtos = Delineations.ListByProjectIDAsUpsertDto(DbContext, projectID);
            return Ok(delineationUpsertDtos);
        }

        [HttpPut("{projectID}/delineations")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public async Task<IActionResult> MergeDelineationsForProject([FromRoute] int projectID, List<DelineationUpsertDto> delineationUpsertDtos)
        {
            await Projects.DeleteProjectNereidResultsAndGrantScores(DbContext, projectID);
            await Delineations.MergeDelineations(DbContext, delineationUpsertDtos, projectID);
            Projects.SetUpdatePersonAndDate(DbContext, projectID, CallingUser.PersonID);
            return Ok();
        }

        [HttpPost("{projectID}/copy")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public async Task<ActionResult<int>> CreateProjectCopy([FromRoute] int projectID)
        {
            var project = Projects.GetByIDWithChangeTracking(DbContext, projectID);
            if (!CallingUser.CanEditJurisdiction(project.StormwaterJurisdictionID, DbContext))
            {
                return Forbid();
            }

            var newProject = await Projects.CreateCopy(DbContext, project, CallingUser.PersonID);
            return Ok(newProject.ProjectID);
        }

        [HttpGet("download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadProjectsWithModels()
        {
            var projectIDs = Projects.ListProjectIDs(DbContext);
            var records = Projects.ListByIDsAsModeledResultSummaryDtos(DbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectModeledResultsSummaryMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("treatmentBMPs/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadTreatmentBMPsForProjects()
        {
            var projectIDs = Projects.ListProjectIDs(DbContext);
            var records = ProjectLoadReducingResults.ListByProjectIDs(DbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectLoadRemovingResultMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("OCTAM2Tier2GrantProgram")]
        [UserViewFeature]
        public ActionResult<List<ProjectDto>> GetProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            if (!CallingUser.IsOCTAGrantReviewer)
            {
                return Forbid();
            }

            var projectHruCharacteristicsSummaryDtos = Projects.ListOCTAM2Tier2Projects(DbContext)
                .Select(x => x.AsDto()).ToList();
            return Ok(projectHruCharacteristicsSummaryDtos);
        }

        [HttpGet("OCTAM2Tier2GrantProgram/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2ProjectIDs(DbContext);
            var records = Projects.ListByIDsAsModeledResultSummaryDtos(DbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectModeledResultsSummaryMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("OCTAM2Tier2GrantProgram/treatmentBMPs/download")]
        [UserViewFeature]
        [Produces(@"text/csv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        public async Task<FileContentResult> DownloadTreatmentBMPsForProjectsSharedWithOCTAM2Tier2GrantProgram()
        {
            var projectIDs = Projects.ListOCTAM2Tier2ProjectIDs(DbContext);
            var records = ProjectLoadReducingResults.ListByProjectIDs(DbContext, projectIDs);

            await using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectLoadRemovingResultMap>();

            await csv.WriteRecordsAsync(records);
            await csv.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "text/csv");
        }

        [HttpGet("delineations")]
        [UserViewFeature]
        public ActionResult<List<DelineationDto>> List()
        {
            if (!CallingUser.IsOCTAGrantReviewer)
            {
                return Forbid();
            }
            var dtos = Delineations.ListProjectDelineationsAsDto(DbContext);
            return Ok(dtos);
        }

    }
}