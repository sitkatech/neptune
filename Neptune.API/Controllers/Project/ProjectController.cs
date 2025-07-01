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
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Common.GeoSpatial;

namespace Neptune.API.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController(
        NeptuneDbContext dbContext,
        ILogger<ProjectController> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration,
        AzureBlobStorageService azureBlobStorageService)
        : SitkaController<ProjectController>(dbContext, logger, keystoneService, neptuneConfiguration)
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
            var project = await Projects.CreateNew(DbContext, projectCreateDto, CallingUser.PersonID);
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

        [HttpGet("{projectID}/bounding-box")]
        [UserViewFeature]
        public ActionResult<BoundingBoxDto> GetBoundingBoxByProjectID([FromRoute] int projectID)
        {
            var stormwaterJurisdictionID = Projects.GetByIDWithChangeTracking(DbContext, projectID).StormwaterJurisdictionID;
            var boundingBoxDto = StormwaterJurisdictions.GetBoundingBoxDtoByJurisdictionID(DbContext, stormwaterJurisdictionID);
            return Ok(boundingBoxDto);
        }

        [HttpGet("{projectID}/progress")]
        [EntityNotFound(typeof(Project), "projectID")]
        [JurisdictionEditFeature]
        public ActionResult<ProjectWorkflowProgress.ProjectWorkflowProgressDto> GetProjectProgress([FromRoute] int projectID)
        {
            var project = Projects.GetByIDWithTrackingForWorkflow(DbContext, projectID);
            var treatmentBMPModelingAttributes = vTreatmentBMPModelingAttributes.ListAsDictionary(dbContext);
            var projectWorkflowProgressDto = ProjectWorkflowProgress.GetProgress(project, treatmentBMPModelingAttributes);
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
            var treatmentBMPDisplayDtos = TreatmentBMPs.ListByProjectIDsAsDisplayDto(DbContext, [projectID]);
            return Ok(treatmentBMPDisplayDtos);
        }

        [HttpGet("{projectID}/treatment-bmps/as-upsert-dtos")]
        [UserViewFeature]
        public ActionResult<List<TreatmentBMPUpsertDto>> GetByProjectID([FromRoute] int projectID)
        {
            var treatmentBMPUpsertDtos = TreatmentBMPs.ListByProjectIDAsUpsertDto(DbContext, projectID);
            return Ok(treatmentBMPUpsertDtos);
        }


        [HttpPut("{projectID}/treatment-bmps/update-locations")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPLocations([FromRoute] int projectID, List<TreatmentBMPDisplayDto> treatmentBMPDisplayDtos)
        {
            var project = DbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var existingProjectTreatmentBMPs = DbContext.TreatmentBMPs.Where(x => x.ProjectID == project.ProjectID).ToList();
            foreach (var treatmentBMPDisplayDto in treatmentBMPDisplayDtos)
            {
                var treatmentBMP = existingProjectTreatmentBMPs.SingleOrDefault(x =>
                    x.TreatmentBMPID == treatmentBMPDisplayDto.TreatmentBMPID);
                if (treatmentBMP != null)
                {
                    var locationPointGeometry4326 = TreatmentBMPs.CreateLocationPoint4326FromLatLong(treatmentBMPDisplayDto.Latitude, treatmentBMPDisplayDto.Longitude);
                    var locationPoint = locationPointGeometry4326.ProjectTo2771();
                    treatmentBMP.LocationPoint = locationPoint;
                    treatmentBMP.LocationPoint4326 = locationPointGeometry4326;
                }
            }
            await DbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{projectID}/treatment-bmps")]
        [JurisdictionEditFeature]
        public async Task<ActionResult> MergeTreatmentBMPs([FromRoute] int projectID, List<TreatmentBMPUpsertDto> treatmentBMPUpsertDtos)
        {
            var project = DbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                return BadRequest();
            }

            var namingConflicts = treatmentBMPUpsertDtos.GroupBy(x => x.TreatmentBMPName).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            if (namingConflicts.Any())
            {
                ModelState.AddModelError("TreatmentBMPName",
                    $"Treatment BMP names need to be unique within a project.  The following names are used more than once: {string.Join(", ", namingConflicts)}");
            }
            else
            {
                var existingTreatmentBMPs = DbContext.TreatmentBMPs.Where(x => x.ProjectID == projectID).AsNoTracking().ToList();
                foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos.Where(treatmentBMPUpsertDto =>
                             existingTreatmentBMPs
                                 .Any(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName &&
                                           x.TreatmentBMPID != treatmentBMPUpsertDto.TreatmentBMPID)))
                {
                    ModelState.AddModelError("TreatmentBMPName",
                        $"A Treatment BMP with the name {treatmentBMPUpsertDto.TreatmentBMPName} already exists for this project. Treatment BMP names must be unique within a project.");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Projects.DeleteProjectNereidResultsAndGrantScores(DbContext, projectID);
            var existingProjectTreatmentBMPs = DbContext.TreatmentBMPs.Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP).Where(x => x.ProjectID == project.ProjectID).ToList();
            var existingProjectTreatmentBMPModelingAttributes = existingProjectTreatmentBMPs.Where(x => x.TreatmentBMPModelingAttributeTreatmentBMP != null).Select(x => x.TreatmentBMPModelingAttributeTreatmentBMP).ToList();

            var updatedTreatmentBMPs = treatmentBMPUpsertDtos.Select(x => TreatmentBMPs.TreatmentBMPFromUpsertDtoAndProject(DbContext, x, project)).ToList();

            await DbContext.TreatmentBMPs.AddRangeAsync(updatedTreatmentBMPs.Where(x => x.TreatmentBMPID == 0));
            await DbContext.SaveChangesAsync();

            // update upsert dtos with new TreatmentBMPIDs
            foreach (var treatmentBMPUpsertDto in treatmentBMPUpsertDtos.Where(x => x.TreatmentBMPID == 0))
            {
                treatmentBMPUpsertDto.TreatmentBMPID = existingProjectTreatmentBMPs
                    .Single(x => x.TreatmentBMPName == treatmentBMPUpsertDto.TreatmentBMPName).TreatmentBMPID;
            }

            // update TreatmentBMP and TreatmentBMPModelingAttribute records
            existingProjectTreatmentBMPs.MergeUpdate(updatedTreatmentBMPs,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.TreatmentBMPName = y.TreatmentBMPName;
                    x.LocationPoint4326 = y.LocationPoint4326;
                    x.LocationPoint = y.LocationPoint;
                    x.WatershedID = y.WatershedID;
                    x.ModelBasinID = y.ModelBasinID;
                    x.PrecipitationZoneID = y.PrecipitationZoneID;
                    x.RegionalSubbasinID = y.RegionalSubbasinID;
                    x.Notes = y.Notes;
                });

            await DbContext.SaveChangesAsync();

            // merge TreatmentBMPModelingAttributeIDs
            var updatedTreatmentBMPModelingAttributes = updatedTreatmentBMPs.Select(x => x.TreatmentBMPModelingAttributeTreatmentBMP).ToList();
            existingProjectTreatmentBMPModelingAttributes.MergeUpdate(updatedTreatmentBMPModelingAttributes,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                (x, y) =>
                {
                    x.AverageDivertedFlowrate = y.AverageDivertedFlowrate;
                    x.AverageTreatmentFlowrate = y.AverageTreatmentFlowrate;
                    x.DesignDryWeatherTreatmentCapacity = y.DesignDryWeatherTreatmentCapacity;
                    x.DesignLowFlowDiversionCapacity = y.DesignLowFlowDiversionCapacity;
                    x.DesignMediaFiltrationRate = y.DesignMediaFiltrationRate;
                    x.DiversionRate = y.DiversionRate;
                    x.DrawdownTimeForWQDetentionVolume = y.DrawdownTimeForWQDetentionVolume;
                    x.EffectiveFootprint = y.EffectiveFootprint;
                    x.EffectiveRetentionDepth = y.EffectiveRetentionDepth;
                    x.InfiltrationDischargeRate = y.InfiltrationDischargeRate;
                    x.InfiltrationSurfaceArea = y.InfiltrationSurfaceArea;
                    x.MediaBedFootprint = y.MediaBedFootprint;
                    x.PermanentPoolOrWetlandVolume = y.PermanentPoolOrWetlandVolume;
                    x.RoutingConfigurationID = y.RoutingConfigurationID;
                    x.StorageVolumeBelowLowestOutletElevation = y.StorageVolumeBelowLowestOutletElevation;
                    x.SummerHarvestedWaterDemand = y.SummerHarvestedWaterDemand;
                    x.TimeOfConcentrationID = y.TimeOfConcentrationID;
                    x.DrawdownTimeForDetentionVolume = y.DrawdownTimeForDetentionVolume;
                    x.TotalEffectiveBMPVolume = y.TotalEffectiveBMPVolume;
                    x.TotalEffectiveDrywellBMPVolume = y.TotalEffectiveDrywellBMPVolume;
                    x.TreatmentRate = y.TreatmentRate;
                    x.UnderlyingHydrologicSoilGroupID = y.UnderlyingHydrologicSoilGroupID;
                    x.UnderlyingInfiltrationRate = y.UnderlyingInfiltrationRate;
                    x.WaterQualityDetentionVolume = y.WaterQualityDetentionVolume;
                    x.WettedFootprint = y.WettedFootprint;
                    x.WinterHarvestedWaterDemand = y.WinterHarvestedWaterDemand;
                    x.MonthsOfOperationID = y.MonthsOfOperationID;
                    x.DryWeatherFlowOverrideID = y.DryWeatherFlowOverrideID;
                });
            await DbContext.SaveChangesAsync();

            await MergeDeleteTreatmentBMPs(existingProjectTreatmentBMPs, updatedTreatmentBMPs);

            return Ok();
        }

        private async Task MergeDeleteTreatmentBMPs(List<TreatmentBMP> existingProjectTreatmentBMPs, List<TreatmentBMP> updatedTreatmentBMPs)
        {
            var treatmentBMPIDsWhoAreBeingDeleted = existingProjectTreatmentBMPs.Select(x => x.TreatmentBMPID)
                .Where(x => updatedTreatmentBMPs.All(y => x != y.TreatmentBMPID)).ToList();
            // delete all the Delineation related entities
            await DbContext.ProjectHRUCharacteristics.Include(x => x.ProjectLoadGeneratingUnit)
                .ThenInclude(x => x.Delineation).Where(x =>
                    x.ProjectLoadGeneratingUnit.DelineationID.HasValue &&
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.ProjectLoadGeneratingUnit.Delineation
                        .TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.ProjectLoadGeneratingUnits.Include(x => x.Delineation)
                .Where(x => x.DelineationID.HasValue &&
                            treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.DelineationOverlaps
                .Include(x => x.Delineation).Include(x => x.OverlappingDelineation).Where(x =>
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.Delineation.TreatmentBMPID) ||
                    treatmentBMPIDsWhoAreBeingDeleted.Contains(x.OverlappingDelineation.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.Delineations.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();
            await DbContext.TreatmentBMPs.Where(x => treatmentBMPIDsWhoAreBeingDeleted.Contains(x.TreatmentBMPID))
                .ExecuteDeleteAsync();

            var treatmentBMPsWhoseLocationChanged = existingProjectTreatmentBMPs
                .Where(x => updatedTreatmentBMPs.Any(y =>
                    x.TreatmentBMPID == y.TreatmentBMPID && (!x.LocationPoint4326.Equals(y.LocationPoint4326))))
                .Select(x => x.TreatmentBMPID).ToList();

            if (treatmentBMPsWhoseLocationChanged.Any())
            {
                await DbContext.Delineations
                    .Where(x => x.DelineationTypeID == (int)DelineationTypeEnum.Centralized &&
                                treatmentBMPsWhoseLocationChanged.Contains(x.TreatmentBMPID)).ExecuteDeleteAsync();
            }
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