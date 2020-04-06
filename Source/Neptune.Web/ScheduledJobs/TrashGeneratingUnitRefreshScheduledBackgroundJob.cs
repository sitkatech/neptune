using System;
using System.Collections.Generic;
using System.IO;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "TGU Refresh";

        // only run in PROD--job takes a relatively long time to run, and QA and local can just refresh their data from PROD backups.
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Prod,
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
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}ComputeTrashGeneratingUnits.py", NeptuneWebConfiguration.PyqgisWorkingDirectory, outputPath);

            if (processUtilityResult.ReturnCode != 0)
            {
                Logger.Error("TGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            }

            Logger.Info("QGIS output:");
            Logger.Info(processUtilityResult.StdOutAndStdErr);

            try
            {
                // kill the old TGUs
                DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit");

                // a GDAL command pulls the shapefile into the database.
                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForTGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportTrashGeneratingUnitsFromShapefile2771(layerName, outputPath,
                    NeptuneWebConfiguration.DatabaseConnectionString);

            }
            catch (Ogr2OgrCommandLineException e)
            {
                Logger.Error("TGU loading (CRS: 2771) via GDAL reported the following errors. This usually means an invalid geometry was skipped. However, you may need to correct the error and re-run the TGU job.", e);
                throw;
            }
            
            // repeat but with 4326
            try
            {
                DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit4326");
                var ogr2OgrCommandLineRunner4326 =
                    new Ogr2OgrCommandLineRunnerForTGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.WGS_1984_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner4326.ImportTrashGeneratingUnitsFromShapefile4326(layerName, outputPath,
                    NeptuneWebConfiguration.DatabaseConnectionString);
            }
            catch (Ogr2OgrCommandLineException e)
            {
                Logger.Error("TGU loading (CRS: 2771) via GDAL reported the following errors. This usually means an invalid geometry was skipped. However, you may need to correct the error and re-run the TGU job.", e);
                throw;
            }
        }
    }
}

public class Ogr2OgrCommandLineRunnerForTGU : Ogr2OgrCommandLineRunner
{
    public Ogr2OgrCommandLineRunnerForTGU(string pathToOgr2OgrExecutable, int coordinateSystemId, double totalMilliseconds) : base(pathToOgr2OgrExecutable, coordinateSystemId, totalMilliseconds)
    {
    }

    /// <summary>
    /// Single-purpose method used by TGU job
    /// </summary>
    /// <param name="outputLayerName"></param>
    /// <param name="outputPath"></param>
    /// <param name="connectionString"></param>
    public void ImportTrashGeneratingUnitsFromShapefile2771(string outputLayerName,
        string outputPath, string connectionString)
    {
        var databaseConnectionString = $"MSSQL:{connectionString}";

        var commandLineArguments = new List<string>
        {
            "-skipfailures",
            "-append",
            "-sql",
            $"SELECT Stormwater as StormwaterJurisdictionID, OnlandVisu as OnlandVisualTrashAssessmentAreaID, LandUseBlo as LandUseBlockID, Delineatio as DelineationID, WaterQuali as WaterQualityManagementPlanID from '{outputLayerName}' where LandUseBlo is not null",
            "--config",
            "GDAL_DATA",
            _gdalDataPath.FullName,
            "-f",
            "MSSQLSpatial",
            databaseConnectionString,
            outputPath,
            "-t_srs",
            GetMapProjection(CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID),
            "-nln",
            "dbo.TrashGeneratingUnit"
        };

        ExecuteOgr2OgrCommand(commandLineArguments);
    }

    // same as above but it uses 4326 instead
    public void ImportTrashGeneratingUnitsFromShapefile4326(string outputLayerName,
        string outputPath, string connectionString)
    {
        var databaseConnectionString = $"MSSQL:{connectionString}";

        var commandLineArguments = new List<string>
        {
            "-skipfailures",
            "-append",
            "-sql",
            $"SELECT Stormwater as StormwaterJurisdictionID, OnlandVisu as OnlandVisualTrashAssessmentAreaID, LandUseBlo as LandUseBlockID, Delineatio as DelineationID, WaterQuali as WaterQualityManagementPlanID from '{outputLayerName}' where LandUseBlo is not null",
            "--config",
            "GDAL_DATA",
            _gdalDataPath.FullName,
            "-f",
            "MSSQLSpatial",
            databaseConnectionString,
            outputPath,
            "-t_srs",
            GetMapProjection(CoordinateSystemHelper.WGS_1984_SRID),
            "-nln",
            "dbo.TrashGeneratingUnit4326"
        };

        ExecuteOgr2OgrCommand(commandLineArguments);
    }


}