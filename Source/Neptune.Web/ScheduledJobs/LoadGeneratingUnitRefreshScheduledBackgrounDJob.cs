using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Neptune.Web.ScheduledJobs
{
    public class LoadGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public int? LoadGeneratingUnitRefreshAreaID { get; }

        public LoadGeneratingUnitRefreshScheduledBackgroundJob(int? loadGeneratingUnitRefreshAreaID)
        {
            LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshAreaID;
        }

        public LoadGeneratingUnitRefreshScheduledBackgroundJob(DatabaseEntities dbContext) : base(dbContext)
        {

        }

        public new static string JobName => "LGU Refresh";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Local,
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

            var additionalCommandLineArguments = new List<string> { outputLayerPath };

            LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea = null;

            if (loadGeneratingUnitRefreshAreaID != null)
            {
                loadGeneratingUnitRefreshArea = DbContext.LoadGeneratingUnitRefreshAreas.Find(loadGeneratingUnitRefreshAreaID);
                var lguInputClipFeatures = DbContext.LoadGeneratingUnits
                    .Where(x => x.LoadGeneratingUnitGeometry.Intersects(loadGeneratingUnitRefreshArea
                        .LoadGeneratingUnitRefreshAreaGeometry)).ToList().Select(x => DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LoadGeneratingUnitGeometry)).ToList();

                var lguInputClipFeatureCollection = new FeatureCollection(lguInputClipFeatures)
                {
                    CRS = new NamedCRS("EPSG:2771")
                };

                // in case the load-generating units were deleted by an update, add the refresh area itself to the clip collection
                lguInputClipFeatureCollection.Features.Add(
                    DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(loadGeneratingUnitRefreshArea
                        .LoadGeneratingUnitRefreshAreaGeometry));

                //var lguInputClipGeoJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(dbGeometry);
                var lguInputClipGeoJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(lguInputClipFeatureCollection);

                File.WriteAllText(clipLayerPath, lguInputClipGeoJsonString);
                additionalCommandLineArguments.AddRange(new List<string>{
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
                if (loadGeneratingUnitRefreshAreaID != null)
                {
                    DbContext.Database.ExecuteSqlCommand(
                        $"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID = {loadGeneratingUnitRefreshAreaID}");
                }
                else
                {
                    DbContext.Database.ExecuteSqlCommand(
                        $"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
                }

                var ogr2OgrCommandLineRunner =
                    new Ogr2OgrCommandLineRunnerForLGU(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 3.6e+6);

                ogr2OgrCommandLineRunner.ImportLoadGeneratingUnitsFromShapefile(outputLayerName, outputLayerPath,
                    NeptuneWebConfiguration.DatabaseConnectionString);

                if (loadGeneratingUnitRefreshArea != null)
                {
                    loadGeneratingUnitRefreshArea.ProcessDate = DateTime.Now;
                    DbContext.SaveChangesWithNoAuditing();
                }
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
                if (loadGeneratingUnitRefreshAreaID != null)
                {
                    File.Delete(clipLayerPath);
                }
            }
        }

        private FeatureCollection MakeClipFeatureCollectionFromRefreshArea(
            LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea)
        {
            if (loadGeneratingUnitRefreshArea == null)
            {
                return null;
            }

            var lguInputClipFeatures = DbContext.LoadGeneratingUnits
                .Where(x => x.LoadGeneratingUnitGeometry.Intersects(loadGeneratingUnitRefreshArea
                    .LoadGeneratingUnitRefreshAreaGeometry)).ToList().Select(x =>
                    DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.LoadGeneratingUnitGeometry)).ToList();

            return new FeatureCollection(lguInputClipFeatures)
            {
                CRS = new NamedCRS("EPSG:2771")
            };
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