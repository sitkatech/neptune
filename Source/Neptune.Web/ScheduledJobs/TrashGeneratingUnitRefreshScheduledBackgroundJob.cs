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

            if (processUtilityResult.ReturnCode != 0)
            {
                Logger.Error("TGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                return;
            }

            try
            {
                // a GDAL command pulls the shapefile into the database.
                DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit");
                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForTGU(NeptuneWebConfiguration.Ogr2OgrExecutable, 4326, 210000);

                ogr2OgrCommandLineRunner.ImportTrashGeneratingUnitsFromShapefile(4326, layerName, outputPath,
                    NeptuneWebConfiguration.DatabaseConnectionString);
            }
            catch (Ogr2OgrCommandLineException e)
            {
                Logger.Error("TGU loading via GDAL failed. TGU layer may be corrupted as a result.", e);
                throw;
            }
        }
    }
}

// No reason for this code to live in 
public class Ogr2OgrCommandLineRunnerForTGU : Ogr2OgrCommandLineRunner
{
    public Ogr2OgrCommandLineRunnerForTGU(string pathToOgr2OgrExecutable, int coordinateSystemId, double totalMilliseconds) : base(pathToOgr2OgrExecutable, coordinateSystemId, totalMilliseconds)
    {
    }

    /// <summary>
    /// Single-purpose method used by TGU job
    /// </summary>
    /// <param name="coordinateSystemId"></param>
    /// <param name="outputLayerName"></param>
    /// <param name="outputPath"></param>
    /// <param name="connectionString"></param>
    public void ImportTrashGeneratingUnitsFromShapefile(int coordinateSystemId, string outputLayerName,
        string outputPath, string connectionString)
    {
        var databaseConnectionString = $"MSSQL:{connectionString}";

        var commandLineArguments = new List<string>
        {
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
            GetMapProjection(coordinateSystemId),
            "-nln",
            "dbo.TrashGeneratingUnit"
        };

        ExecuteOgr2OgrCommand(commandLineArguments);
    }


}