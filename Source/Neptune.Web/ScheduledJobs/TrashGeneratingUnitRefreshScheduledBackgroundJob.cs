using System;
using System.Collections.Generic;
using System.IO;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "TGU Refresh";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            TrashGeneratingUnitRefreshImpl();
        }

        protected virtual void TrashGeneratingUnitRefreshImpl()
        {
            Logger.Info($"Processing '{JobName}'");

            var layerName = Guid.NewGuid().ToString();
            var outputPath = $"{Path.Combine(Path.GetTempPath(), layerName)}.shp";

            // a PyQGIS script computes the TGU layer and saves it as a shapefile
            var processUtilityResult = QgisRunner.ExecuteTrashGeneratingUnitScript($"{NeptuneWebConfiguration.PyqgisTestWorkingDirectory}ComputeTrashGeneratingUnits.py", NeptuneWebConfiguration.PyqgisTestWorkingDirectory, outputPath);

            // a GDAL command pulls the shapefile into the database.
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit");
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, 4326, 210000);
            ogr2OgrCommandLineRunner.ImportTrashGeneratingUnitsFromShapefile(4326,layerName,outputPath, NeptuneWebConfiguration.DatabaseConnectionString);
        }
    }
}
