﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class LoadGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public int? LoadGeneratingUnitRefreshAreaID { get; }

        public LoadGeneratingUnitRefreshScheduledBackgroundJob(int? loadGeneratingUnitRefreshAreaID)
        {
            LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshAreaID;
        }

        public new static string JobName => "LGU Refresh";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };
        protected override void RunJobImplementation()
        {
            LoadGeneratingUnitRefreshImpl(LoadGeneratingUnitRefreshAreaID);
        }

        private void LoadGeneratingUnitRefreshImpl(int? loadGeneratingUnitRefreshAreaID)
        {
            Logger.Info($"Processing '{JobName}'");

            var outputLayerName = Guid.NewGuid().ToString();
            var outputLayerPath = $"{Path.Combine(Path.GetTempPath(), outputLayerName)}.shp";

            var clipLayerPath = $"{Path.Combine(Path.GetTempPath(), outputLayerName)}_inputClip.json";

            var additionalCommandLineArguments = new List<string> {outputLayerPath};
            
            if (loadGeneratingUnitRefreshAreaID != null)
            {
                // todo: update to handle lguRefreshAreaID
                var loadGeneratingUnitRefreshArea = DbContext.LoadGeneratingUnitRefreshAreas.Find(loadGeneratingUnitRefreshAreaID);
                var lguInputClipFeatures = DbContext.LoadGeneratingUnits
                    .Where(x => x.LoadGeneratingUnitGeometry.Intersects(loadGeneratingUnitRefreshArea
                        .LoadGeneratingUnitRefreshAreaGeometry)).ToList().Select(x => DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LoadGeneratingUnitGeometry)).ToList();

                var lguInputClipFeatureCollection = new FeatureCollection(lguInputClipFeatures);


                //var lguInputClipGeoJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(dbGeometry);
                var lguInputClipGeoJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(lguInputClipFeatureCollection);

                File.WriteAllText(clipLayerPath, lguInputClipGeoJsonString);
                additionalCommandLineArguments.AddRange( new List<string>{
                    "--clip", clipLayerPath
                });
            }

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
                // todo: when HRU characteristics are updated to key on LGUs, we will no longer be able to truncate
                DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.LoadGeneratingUnit");

                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForLGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportLoadGeneratingUnitsFromShapefile(outputLayerName, outputLayerPath,
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