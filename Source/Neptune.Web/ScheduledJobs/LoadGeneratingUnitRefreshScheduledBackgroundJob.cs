using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

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
            //NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Qa,
            NeptuneEnvironmentType.Prod
        };
        protected override void RunJobImplementation()
        {
            LoadGeneratingUnitRefreshImpl(LoadGeneratingUnitRefreshAreaID);
        }

        private void LoadGeneratingUnitRefreshImpl(int? loadGeneratingUnitRefreshAreaID)
        {
            Logger.Info($"Processing '{JobName}'");

            var outputLayerName = $"LGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}.geojson";
            var clipLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}_inputClip.json";

            var additionalCommandLineArguments = new List<string> { outputFolder, outputLayerName};

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

            if (processUtilityResult.ReturnCode > 0)
            {
                Logger.Error("LGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            }

            if (loadGeneratingUnitRefreshAreaID != null)
            {
                DbContext.Database.ExecuteSqlCommand($"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID = {loadGeneratingUnitRefreshAreaID}");
            }
            else
            {
                DbContext.Database.ExecuteSqlCommand("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
            }

            var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions(4, 2);
            using (var openStream = File.OpenRead(outputLayerPath))
            {
                var featureCollection = JsonSerializer.DeserializeAsync<NetTopologySuite.Features.FeatureCollection>(openStream, jsonSerializerOptions).Result;
                var features = featureCollection.Where(x => x.Geometry != null).ToList();
                var loadGeneratingUnits = new List<LoadGeneratingUnit>();

                foreach (var feature in features)
                {
                    var loadGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<LoadGeneratingUnitResult>(feature, jsonSerializerOptions);
                    // we should only get Polygons from the Pyqgis rodeo overlay
                    // when we convert geojson to dbgeometry, they can result in invalid geometries
                    // however, when we run makevalid, it can potentially change the geometry type 
                    // from Polygon to a MultiPolygon or GeometryCollection
                    // so we need to explode them if that happens since we are only expecting polygons for LGUs
                    var dbGeometries = GeometryHelpers.GeometryToDbGeometryAndMakeValidAndExplodeIfNeeded(loadGeneratingUnitResult.Geometry, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

                    loadGeneratingUnits.AddRange(dbGeometries.Select(dbGeometry => new LoadGeneratingUnit(dbGeometry)
                    {
                        DelineationID = loadGeneratingUnitResult.DelineationID,
                        WaterQualityManagementPlanID = loadGeneratingUnitResult.WaterQualityManagementPlanID,
                        ModelBasinID = loadGeneratingUnitResult.ModelBasinID,
                        RegionalSubbasinID = loadGeneratingUnitResult.RegionalSubbasinID
                    }));
                }

                if (loadGeneratingUnits.Any())
                {
                    DbContext.LoadGeneratingUnits.AddRange(loadGeneratingUnits);
                    DbContext.SaveChangesWithNoAuditing();
                }

                if (loadGeneratingUnitRefreshArea != null)
                {
                    loadGeneratingUnitRefreshArea.ProcessDate = DateTime.Now;
                    DbContext.SaveChangesWithNoAuditing();
                }
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
    }
}