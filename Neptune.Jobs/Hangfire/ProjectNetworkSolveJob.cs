﻿using System.Diagnostics;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire
{
    public class ProjectNetworkSolveJob
    {
        public const string JobName = "Nereid Planned Project Network Solve";

        private readonly NereidService _nereidService;
        private readonly OCGISService _ocgisService;
        private readonly ILogger<ProjectNetworkSolveJob> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly NeptuneDbContext _dbContext;
        private readonly NeptuneJobConfiguration _neptuneJobConfiguration;
        private readonly SitkaSmtpClientService _sitkaSmtpClient;

        public ProjectNetworkSolveJob(ILogger<ProjectNetworkSolveJob> logger, IWebHostEnvironment webHostEnvironment, NeptuneDbContext dbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService, OCGISService ocgisService)
        {
            _nereidService = nereidService;
            _ocgisService = ocgisService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _neptuneJobConfiguration = neptuneJobConfiguration.Value;
            _sitkaSmtpClient = sitkaSmtpClientService;
        }

        public async void RunNetworkSolveForProject(int projectID, int projectNetworkSolveHistoryID)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                throw new NullReferenceException($"Project with ID {projectID} does not exist!");
            }
            var projectNetworkSolveHistory = _dbContext.ProjectNetworkSolveHistories.Include(x => x.RequestedByPerson).AsNoTracking().First(x => x.ProjectNetworkSolveHistoryID == projectNetworkSolveHistoryID);
            var projectRegionalSubbasinIDs = _dbContext.TreatmentBMPs.AsNoTracking().Where(x => x.ProjectID == projectID).Select(x => x.RegionalSubbasinID).Distinct().ToList();

            var regionalSubbasinIDs = _dbContext.vRegionalSubbasinUpstreams.AsNoTracking()
                .Where(x => projectRegionalSubbasinIDs.Contains(x.PrimaryKey) && x.RegionalSubbasinID.HasValue).Select(x => x.RegionalSubbasinID.Value).ToList();
            var projectDistributedDelineationIDs = project.TreatmentBMPs.Select(x => x.Delineation).Where(y => y.DelineationTypeID == (int)DelineationTypeEnum.Distributed).Select(x => x.DelineationID).ToList();
            try
            {
                //Get our LGUs
                LoadGeneratingUnitRefreshImpl(regionalSubbasinIDs, projectID);
                //Get our HRUs
                await HRURefreshImpl(projectID);
                await _nereidService.ProjectNetworkSolve(_dbContext, false, projectID, regionalSubbasinIDs, projectDistributedDelineationIDs);
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                await SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, true, null);
            }
            catch (Exception ex)
            {
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Failed;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                projectNetworkSolveHistory.ErrorMessage = ex.Message;
                await _dbContext.SaveChangesAsync();
                await SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, false, ex.Message);
                throw;
            }
        }

        private async Task SendProjectNetworkSolveTerminalStatusEmail(Person requestPerson,
            Project project, bool successful, string errorMessage)
        {
            var projectName = project.ProjectName;
            var subject = successful ? $"Modeled Results calculated for Project: {projectName}" : $"Model Results calculation failed for Project: {projectName}";
            var requestPersonEmail = requestPerson.Email;
            var errorContext = $"<br /><br/>See the provided error message for more details:\n {errorMessage}";
            var planningURL = $"{_neptuneJobConfiguration.PlanningModuleBaseUrl}/projects/edit/{project.ProjectID}/stormwater-treatments/modeled-performance-and-metrics";
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
Model results calculation for Project:{projectName} has completed{(!successful ? " but encountered errors." : ".")}
<br /><br />
You can view the results or trigger another network solve <a href='{planningURL}'>here</a>.

{(!successful ? errorContext : "")}
";
            // Create Notification
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(requestPersonEmail);

            if (!successful)
            {
                foreach (var email in People.GetEmailAddressesForAdminsThatReceiveSupportEmails(_dbContext))
                {
                    mailMessage.CC.Add(email);
                }
            }

            await _sitkaSmtpClient.Send(mailMessage);
        }

        private void LoadGeneratingUnitRefreshImpl(List<int> regionalSubbasinIDs, int projectID)
        {
            _logger.LogInformation($"Processing '{JobName}'-LoadGeneratingUnitRefresh for {projectID}");
            var outputLayerName = $"PLGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}.geojson";

            var additionalCommandLineArguments = new List<string> { outputFolder, outputLayerName, "--planned_project_id", projectID.ToString(), "--rsb_ids", String.Join(", ", regionalSubbasinIDs) };

            //todo: pyqgis
            //// a PyQGIS script computes the LGU layer and saves it as a shapefile
            //var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{neptuneJobConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", neptuneJobConfiguration.PyqgisWorkingDirectory, additionalCommandLineArguments);

            //if (processUtilityResult.ReturnCode != 0)
            //{
            //    Logger.LogError("LGU Geoprocessing failed. Output:");
            //    Logger.LogError(processUtilityResult.StdOutAndStdErr);
            //    throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            //}

            _dbContext.Database.ExecuteSqlRaw(
                $"EXEC dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID = {projectID}");

            var jsonSerializerOptions = GeoJsonSerializer.DefaultSerializerOptions;
            using(var openStream = File.OpenRead(outputLayerPath))
            {
                var featureCollection = JsonSerializer.DeserializeAsync<NetTopologySuite.Features.FeatureCollection>(openStream, jsonSerializerOptions).Result;
                var features = featureCollection.Where(x => x.Geometry != null).ToList();
                var projectLoadGeneratingUnits = new List<ProjectLoadGeneratingUnit>();

                foreach (var feature in features)
                {
                    var loadGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<LoadGeneratingUnitResult>(feature, jsonSerializerOptions);

                    // we should only get Polygons from the Pyqgis rodeo overlay
                    // when we convert geojson to dbgeometry, they can result in invalid geometries
                    // however, when we run makevalid, it can potentially change the geometry type 
                    // from Polygon to a MultiPolygon or GeometryCollection
                    // so we need to explode them if that happens since we are only expecting polygons for LGUs
                    var geometries = GeometryHelper.GeometryToDbGeometryAndMakeValidAndExplodeIfNeeded(loadGeneratingUnitResult.Geometry);

                    projectLoadGeneratingUnits.AddRange(geometries.Select(dbGeometry =>
                        new ProjectLoadGeneratingUnit
                        {
                            ProjectLoadGeneratingUnitGeometry = dbGeometry,
                            ProjectID = projectID,
                            DelineationID = loadGeneratingUnitResult.DelineationID,
                            WaterQualityManagementPlanID = loadGeneratingUnitResult.WaterQualityManagementPlanID,
                            ModelBasinID = loadGeneratingUnitResult.ModelBasinID,
                            RegionalSubbasinID = loadGeneratingUnitResult.RegionalSubbasinID
                        }));
                }

                if (projectLoadGeneratingUnits.Any())
                {
                    _dbContext.ProjectLoadGeneratingUnits.AddRange(projectLoadGeneratingUnits);
                    _dbContext.SaveChanges();
                }
            }

            // clean up temp files if not running in a local environment
            if (!_webHostEnvironment.IsDevelopment())
            {
                File.Delete(outputLayerPath);
            }
        }

        private async Task HRURefreshImpl(int projectID)
        {
            // collect the load generating units that require updates, which will be all for the Project
            // group them by Model basin so requests to the HRU service are spatially bounded. It is possible that a number of these won't have a model basin, but if the system is used in a logical manner any of these that don't have model basins will be in an rsb that is in North OC, so technically will still be spatially bounded
            // and batch them for processing 25 at a time so requests are small.
            _logger.LogInformation($"Processing '{JobName}'-HRURefresh for {projectID}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = _dbContext.ProjectLoadGeneratingUnits.Where(x => x.ProjectID == projectID && x.ProjectLoadGeneratingUnitGeometry.Area >= 10).ToList();
            var loadGeneratingUnitsToUpdateGroupedByModelBasin = loadGeneratingUnitsToUpdate.GroupBy(x => x.ModelBasinID);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Chunk(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var projectLoadGeneratingUnits = batch.ToList();
                        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(projectLoadGeneratingUnits.ToDictionary(x => x.ProjectLoadGeneratingUnitID, x => x.ProjectLoadGeneratingUnitGeometry), true);
                        var hruResponseFeatures = await _ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList());

                        if (!hruResponseFeatures.Any())
                        {
                            foreach (var loadGeneratingUnit in projectLoadGeneratingUnits)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;
                            }
                            _logger.LogWarning($"No data for ProjectLGUs with these IDs: {string.Join(", ", projectLoadGeneratingUnits.Select(x => x.ProjectLoadGeneratingUnitID.ToString()))}");
                        }

                        var projectHruCharacteristics = hruResponseFeatures.Select(x =>
                        {
                            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => y.HRUCharacteristicLandUseCodeName == x.Attributes.ModelBasinLandUseDescription);
                            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => y.HRUCharacteristicLandUseCodeName == x.Attributes.BaselineLandUseDescription);

                            var projectHRUCharacteristic = new ProjectHRUCharacteristic
                            {
                                ProjectLoadGeneratingUnitID = x.Attributes.QueryFeatureID,
                                ProjectID = projectID,
                                HydrologicSoilGroup = x.Attributes.HydrologicSoilGroup,
                                SlopePercentage = x.Attributes.SlopePercentage.GetValueOrDefault(),
                                ImperviousAcres = x.Attributes.ImperviousAcres.GetValueOrDefault(),
                                LastUpdated = DateTime.Now,
                                Area = x.Attributes.Acres.GetValueOrDefault(),
                                HRUCharacteristicLandUseCodeID = hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                                BaselineImperviousAcres = x.Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID
                            };
                            return projectHRUCharacteristic;
                        }).ToList();
                        await _dbContext.ProjectHRUCharacteristics.AddRangeAsync(projectHruCharacteristics);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        _logger.LogWarning(ex.Message);
                    }

                    if (stopwatch.Elapsed.Minutes > 20)
                    {
                        break;
                    }
                }

                if (stopwatch.Elapsed.Minutes > 20)
                {
                    break;
                }
            }

            stopwatch.Stop();
        }
    }
}
