using System;
using System.Collections.Generic;
using System.IO;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class LoadGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "LGU Refresh";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };
        protected override void RunJobImplementation()
        {
            LoadGeneratingUnitRefreshImpl();
        }

        private void LoadGeneratingUnitRefreshImpl()
        {
            Logger.Info($"Processing '{JobName}'");

            var layerName = Guid.NewGuid().ToString();
            var outputPath = $"{Path.Combine(Path.GetTempPath(), layerName)}.shp";

            // a PyQGIS script computes the LGU layer and saves it as a shapefile
            var processUtilityResult = QgisRunner.ExecuteTrashGeneratingUnitScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", NeptuneWebConfiguration.PyqgisWorkingDirectory, outputPath);

            if (processUtilityResult.ReturnCode != 0)
            {
                Logger.Error("LGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            }

            // todo: Slurp. 
        }
    }
}