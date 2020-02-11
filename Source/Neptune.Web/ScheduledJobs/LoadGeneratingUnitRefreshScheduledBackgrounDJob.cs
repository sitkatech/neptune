using System;
using System.Collections.Generic;
using System.IO;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
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
            try
            {
                // todo: when HRU characteristics are updated to key on LGUs, we will no longer be able to truncate
                DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.LoadGeneratingUnit");

                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForLGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportLoadGeneratingUnitsFromShapefile(layerName, outputPath,
                    NeptuneWebConfiguration.DatabaseConnectionString);
            }
            catch (Ogr2OgrCommandLineException e)
            {
                Logger.Error("LGU loading (CRS: 2771) via GDAL reported the following errors. This usually means an invalid geometry was skipped. However, you may need to correct the error and re-run the TGU job.", e);
                throw;
            }
        }
    }
}
public class Ogr2OgrCommandLineRunnerForLGU : Ogr2OgrCommandLineRunner
{
    public Ogr2OgrCommandLineRunnerForLGU(string pathToOgr2OgrExecutable, int coordinateSystemId, double totalMilliseconds) : base(pathToOgr2OgrExecutable, coordinateSystemId, totalMilliseconds)
    {
    }

    /// <summary>
    /// Single-purpose method used by TGU job
    /// </summary>
    /// <param name="outputLayerName"></param>
    /// <param name="outputPath"></param>
    /// <param name="connectionString"></param>
    public void ImportLoadGeneratingUnitsFromShapefile(string outputLayerName,
        string outputPath, string connectionString)
    {
        var databaseConnectionString = $"MSSQL:{connectionString}";
        // todo: fix this
        var selectStatement =
            $"Select LSPCID as LSPCBasinID, RSBID as RegionalSubbasinID, DelinID as DelineationID, WQMPID as WaterQualityManagementPlanID from '{outputLayerName}'";

        var commandLineArguments = new List<string>
        {
            "-skipfailures",
            "-append",
            "-sql",
            selectStatement,
            "--config",
            "GDAL_DATA",
            _gdalDataPath.FullName,
            "-f",
            "MSSQLSpatial",
            databaseConnectionString,
            outputPath,
            "-a_srs",
            GetMapProjection(CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID),
            "-nln",
            "dbo.LoadGeneratingUnit"
        };

        ExecuteOgr2OgrCommand(commandLineArguments);
    }
}