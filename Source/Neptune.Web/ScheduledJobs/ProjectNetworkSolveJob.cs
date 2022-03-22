using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Net.Http;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using MoreLinq;
using Neptune.Web.Common;
using Neptune.Web.Common.EsriAsynchronousJob;
using Neptune.Web.Models;
using System.Net.Mail;
using LtInfo.Common.Email;

namespace Neptune.Web.ScheduledJobs
{
    public class ProjectNetworkSolveJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Nereid Planned Project Network Solve";

        public HttpClient HttpClient { get; set; }
        public int ProjectID { get; }
        public int ProjectNetworkSolveHistoryID { get; }

        public ProjectNetworkSolveJob(int projectID, int projectNetworkSolveHistoryID) : base()
        {
            HttpClient = new HttpClient();
            ProjectID = projectID;
            ProjectNetworkSolveHistoryID = projectNetworkSolveHistoryID;
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var project = DbContext.Projects.First(x => x.ProjectID == ProjectID);
            var projectNetworkSolveHistory = DbContext.ProjectNetworkSolveHistories.Include(x => x.RequestedByPerson).First(x => x.ProjectNetworkSolveHistoryID == ProjectNetworkSolveHistoryID);
            var regionalSubbasinIDs = project.GetRegionalSubbasinIDs(DbContext);

            try
            {
                //Get our LGUs
                LoadGeneratingUnitRefreshImpl(regionalSubbasinIDs);
                //Get our HRUs
                HRURefreshImpl();
                throw new Exception("Oh no an exception!!!!");
                //NereidUtilities.ProjectNetworkSolve(out _, out _, out _, DbContext, HttpClient, false, ProjectID, regionalSubbasinIDs);
                //projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded;
                //DbContext.SaveChangesWithNoAuditing();
                //SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, true, null);
            }
            catch (Exception ex)
            {
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Failed;
                projectNetworkSolveHistory.ErrorMessage = ex.Message;
                DbContext.SaveChangesWithNoAuditing();
                SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, false, ex.Message);
                throw;
            }
            
        }

        private void SendProjectNetworkSolveTerminalStatusEmail(Models.Person requestPerson,
            Models.Project project, bool successful, string errorMessage)
        {
            var projectName = project.ProjectName;
            var subject = successful ? $"Modeled Results calculated for Project:{projectName}" : $"Model Results calculation failed for Project:{projectName}";
            var requestPersonEmail = requestPerson.Email;
            var errorContext = $"<br /><br/>See the provided error message for more details:\n {errorMessage}";
            var planningURL = $"{NeptuneWebConfiguration.CanonicalHostNamePlanning}/projects/edit/{project.ProjectID}/stormwater-treatments/modeled-performance";
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
                From = new MailAddress(Common.NeptuneWebConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(requestPersonEmail);

            foreach (var revisionRequestPeople in HttpRequestStorage.DatabaseEntities.People
                .GetPeopleWhoReceiveSupportEmails())
            {
                mailMessage.CC.Add(revisionRequestPeople.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }

        private void LoadGeneratingUnitRefreshImpl(List<int> regionalSubbasinIDs)
        {
            Logger.Info($"Processing '{JobName}'-LoadGeneratingUnitRefresh for {ProjectID}");

            var outputLayerName = Guid.NewGuid().ToString();
            var outputLayerPath = $"{Path.Combine(Path.GetTempPath(), outputLayerName)}.shp";
            var additionalCommandLineArguments = new List<string> { outputLayerPath, "--planned_project_id", ProjectID.ToString(), "--rsb_ids", String.Join(", ", regionalSubbasinIDs) };

            // a PyQGIS script computes the LGU layer and saves it as a shapefile
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", NeptuneWebConfiguration.PyqgisWorkingDirectory, additionalCommandLineArguments);

            if (processUtilityResult.ReturnCode != 0)
            {
                Logger.Error("LGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            }

            try
            {
                DbContext.Database.ExecuteSqlCommand(
                    $"EXEC dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID = {ProjectID}");

                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForLGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportLoadGeneratingUnitsFromShapefile(outputLayerName, outputLayerPath,
                    NeptuneWebConfiguration.DatabaseConnectionString, ProjectID);
            }
            catch (Ogr2OgrCommandLineException e)
            {
                Logger.Error("LGU loading (CRS: 2771) via GDAL reported the following errors. This usually means an invalid geometry was skipped. However, you may need to correct the error and re-run the TGU job.", e);
                throw;
            }

            // clean up temp files if not running in a local environment
            if (NeptuneWebConfiguration.NeptuneEnvironment.NeptuneEnvironmentType != NeptuneEnvironmentType.Local)
            {
                File.Delete(outputLayerPath);
            }
        }

        private void HRURefreshImpl()
        {
            // collect the load generating units that require updates, which will be all for the Project
            // group them by Model basin so requests to the HRU service are spatially bounded. It is possible that a number of these won't have a model basin, but if the system is used in a logical manner any of these that don't have model basins will be in an rsb that is in North OC, so technically will still be spatially bounded
            // and batch them for processing 25 at a time so requests are small.
            Logger.Info($"Processing '{JobName}'-HRURefresh for {ProjectID}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = DbContext.ProjectLoadGeneratingUnits.Where(x => x.ProjectID == ProjectID).ToList();
            var loadGeneratingUnitsToUpdateGroupedByModelBasin = loadGeneratingUnitsToUpdate.GroupBy(x => x.ModelBasin);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Batch(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var batchHRUResponseFeatures =
                            HRUUtilities.RetrieveHRUResponseFeatures(batch.GetHRURequestFeatures().ToList(), Logger).ToList();

                        if (!batchHRUResponseFeatures.Any())
                        {
                            foreach (var loadGeneratingUnit in batch)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;
                            }
                            Logger.Warn($"No data for ProjectLGUs with these IDs: {string.Join(", ", batch.Select(x => x.ProjectLoadGeneratingUnitID.ToString()))}");
                        }

                        DbContext.ProjectHRUCharacteristics.AddRange(batchHRUResponseFeatures.Select(x =>
                        {
                            var hruCharacteristic = x.ToProjectHRUCharacteristic(ProjectID);
                            return hruCharacteristic;
                        }));
                        DbContext.SaveChangesWithNoAuditing();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        Logger.Warn(ex.Message);
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
