using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class PlannedProjectNetworkSolveJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Nereid Planned Project Network Solve";

        public HttpClient HttpClient { get; set; }
        public int PlannedProjectID { get; }

        public PlannedProjectNetworkSolveJob(int plannedProjectID) : base()
        {
            HttpClient = new HttpClient();
            PlannedProjectID = plannedProjectID;
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var plannedProject = DbContext.Projects.First(x => x.ProjectID == PlannedProjectID);
            var plannedProjectRSBIDs = plannedProject.GetRegionalSubbasinIDs(DbContext);
            //Get our LGUs
            LoadGeneratingUnitRefreshImpl(plannedProjectRSBIDs);

            NereidUtilities.PlannedProjectNetworkSolve(out _, out _, out _, DbContext, HttpClient, false, plannedProject);
        }

        private void LoadGeneratingUnitRefreshImpl(List<int> regionalSubbasinIDs)
        {
            Logger.Info($"Processing '{JobName}'-LoadGeneratingUnitRefresh for {PlannedProjectID}");

            var outputLayerName = Guid.NewGuid().ToString();
            var outputLayerPath = $"{Path.Combine(Path.GetTempPath(), outputLayerName)}.shp";

            var additionalCommandLineArguments = new List<string> { outputLayerPath, PlannedProjectID.ToString(), String.Join(", ", regionalSubbasinIDs) };

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
                    $"EXEC dbo.pDeletePlannedProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID = {PlannedProjectID}");

                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForLGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportLoadGeneratingUnitsFromShapefile(outputLayerName, outputLayerPath,
                    NeptuneWebConfiguration.DatabaseConnectionString, PlannedProjectID);
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
    }
}
