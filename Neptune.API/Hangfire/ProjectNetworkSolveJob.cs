using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Models.EsriAsynchronousJob;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class ProjectNetworkSolveJob : ScheduledBackgroundJobBase<ProjectNetworkSolveJob>
    {
        public const string JobName = "Nereid Planned Project Network Solve";

        private readonly NereidService _nereidService;
        private readonly int projectID;
        private readonly int ProjectNetworkSolveHistoryID;

        public ProjectNetworkSolveJob(ILogger<ProjectNetworkSolveJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService, int projectID, int projectNetworkSolveHistoryID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            _nereidService = nereidService;
            this.projectID = projectID;
            ProjectNetworkSolveHistoryID = projectNetworkSolveHistoryID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            var project = DbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                throw new NullReferenceException($"Project with ID {projectID} does not exist!");
            }
            var projectNetworkSolveHistory = DbContext.ProjectNetworkSolveHistories.Include(x => x.RequestedByPerson).AsNoTracking().First(x => x.ProjectNetworkSolveHistoryID == ProjectNetworkSolveHistoryID);
            var projectRegionalSubbasinIDs = DbContext.TreatmentBMPs.AsNoTracking().Where(x => x.ProjectID == projectID).Select(x => x.RegionalSubbasinID).Distinct().ToList();

            var regionalSubbasinIDs = DbContext.vRegionalSubbasinUpstreams.AsNoTracking()
                .Where(x => projectRegionalSubbasinIDs.Contains(x.PrimaryKey) && x.RegionalSubbasinID.HasValue).Select(x => x.RegionalSubbasinID.Value).ToList();
            var projectDistributedDelineationIDs = project.TreatmentBMPs.Select(x => x.Delineation).Where(y => y.DelineationTypeID == (int)DelineationTypeEnum.Distributed).Select(x => x.DelineationID).ToList();
            try
            {
                //Get our LGUs
                LoadGeneratingUnitRefreshImpl(regionalSubbasinIDs);
                //Get our HRUs
                HRURefreshImpl();
                _nereidService.ProjectNetworkSolve(DbContext, false, projectID, regionalSubbasinIDs, projectDistributedDelineationIDs);
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                DbContext.SaveChanges();
                SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, true, null);
            }
            catch (Exception ex)
            {
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Failed;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                projectNetworkSolveHistory.ErrorMessage = ex.Message;
                DbContext.SaveChanges();
                SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, false, ex.Message);
                throw;
            }
            
        }

        private void SendProjectNetworkSolveTerminalStatusEmail(Person requestPerson,
            Project project, bool successful, string errorMessage)
        {
            var projectName = project.ProjectName;
            var subject = successful ? $"Modeled Results calculated for Project: {projectName}" : $"Model Results calculation failed for Project: {projectName}";
            var requestPersonEmail = requestPerson.Email;
            var errorContext = $"<br /><br/>See the provided error message for more details:\n {errorMessage}";
            var planningURL = $"{NeptuneConfiguration.PlanningModuleBaseUrl}/projects/edit/{project.ProjectID}/stormwater-treatments/modeled-performance-and-metrics";
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
                From = new MailAddress(NeptuneConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(requestPersonEmail);

            if (!successful)
            {
                foreach (var email in People.GetEmailAddressesForAdminsThatReceiveSupportEmails(DbContext))
                {
                    mailMessage.CC.Add(email);
                }
            }

            _sitkaSmtpClient.Send(mailMessage);
        }

        private void LoadGeneratingUnitRefreshImpl(List<int> regionalSubbasinIDs)
        {
            Logger.LogInformation($"Processing '{JobName}'-LoadGeneratingUnitRefresh for {projectID}");
            var outputLayerName = $"PLGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}.geojson";

            var additionalCommandLineArguments = new List<string> { outputFolder, outputLayerName, "--planned_project_id", projectID.ToString(), "--rsb_ids", String.Join(", ", regionalSubbasinIDs) };

            //todo: pyqgis
            //// a PyQGIS script computes the LGU layer and saves it as a shapefile
            //var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", NeptuneConfiguration.PyqgisWorkingDirectory, additionalCommandLineArguments);

            //if (processUtilityResult.ReturnCode != 0)
            //{
            //    Logger.LogError("LGU Geoprocessing failed. Output:");
            //    Logger.LogError(processUtilityResult.StdOutAndStdErr);
            //    throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            //}

            DbContext.Database.ExecuteSqlRaw(
                $"EXEC dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID = {projectID}");

            var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions();
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
                    DbContext.ProjectLoadGeneratingUnits.AddRange(projectLoadGeneratingUnits);
                    DbContext.SaveChanges();
                }
            }

            // clean up temp files if not running in a local environment
            if (!_webHostEnvironment.IsDevelopment())
            {
                File.Delete(outputLayerPath);
            }
        }

        private void HRURefreshImpl()
        {
            // collect the load generating units that require updates, which will be all for the Project
            // group them by Model basin so requests to the HRU service are spatially bounded. It is possible that a number of these won't have a model basin, but if the system is used in a logical manner any of these that don't have model basins will be in an rsb that is in North OC, so technically will still be spatially bounded
            // and batch them for processing 25 at a time so requests are small.
            Logger.LogInformation($"Processing '{JobName}'-HRURefresh for {projectID}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = DbContext.ProjectLoadGeneratingUnits.Where(x => x.ProjectID == projectID && x.ProjectLoadGeneratingUnitGeometry.Area >= 10).ToList();
            var loadGeneratingUnitsToUpdateGroupedByModelBasin = loadGeneratingUnitsToUpdate.GroupBy(x => x.ModelBasin);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Chunk(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var projectLoadGeneratingUnits = batch.ToList();
                        var batchHRUResponseFeatures = HRUUtilities.RetrieveHRUResponseFeatures(projectLoadGeneratingUnits.GetHRURequestFeatures().ToList(), Logger).ToList();

                        if (!batchHRUResponseFeatures.Any())
                        {
                            foreach (var loadGeneratingUnit in projectLoadGeneratingUnits)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;
                            }
                            Logger.LogWarning($"No data for ProjectLGUs with these IDs: {string.Join(", ", projectLoadGeneratingUnits.Select(x => x.ProjectLoadGeneratingUnitID.ToString()))}");
                        }

                        var projectHruCharacteristics = batchHRUResponseFeatures.Select(x =>
                        {
                            var hruCharacteristic = x.ToProjectHRUCharacteristic(projectID);
                            return hruCharacteristic;
                        }).ToList();
                        DbContext.ProjectHRUCharacteristics.AddRange(projectHruCharacteristics);
                        DbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        Logger.LogWarning(ex.Message);
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
