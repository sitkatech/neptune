using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.API.Hangfire
{
    public class LoadGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<LandUseBlockUploadBackgroundJob>
    {
        public int? LoadGeneratingUnitRefreshAreaID { get; }

        public LoadGeneratingUnitRefreshScheduledBackgroundJob(ILogger<LandUseBlockUploadBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            LoadGeneratingUnitRefreshAreaID = personID;
        }

        public const string JobName = "LGU Refresh";

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            LoadGeneratingUnitRefreshImpl(LoadGeneratingUnitRefreshAreaID);
        }

        private void LoadGeneratingUnitRefreshImpl(int? loadGeneratingUnitRefreshAreaID)
        {
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
                        .LoadGeneratingUnitRefreshAreaGeometry)).ToList().Select(x => 
                        new Feature(x.LoadGeneratingUnitGeometry, new AttributesTable())).ToList();

                var lguInputClipFeatureCollection = new FeatureCollection();
                foreach (var feature in lguInputClipFeatures)
                {
                    lguInputClipFeatureCollection.Add(feature);
                }

                // in case the load-generating units were deleted by an update, add the refresh area itself to the clip collection
                lguInputClipFeatureCollection.Add(new Feature(loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaGeometry, new AttributesTable()));

                //var lguInputClipGeoJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(dbGeometry);
                var lguInputClipGeoJsonString = GeoJsonSerializer.Serialize(lguInputClipFeatureCollection);

                File.WriteAllText(clipLayerPath, lguInputClipGeoJsonString);
                additionalCommandLineArguments.AddRange(new List<string>{
                    "--clip", clipLayerPath
                });
            }

            //todo: pyqgis
            //// a PyQGIS script computes the LGU layer and saves it as a shapefile
            //var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", NeptuneConfiguration.PyqgisWorkingDirectory, additionalCommandLineArguments);

            //if (processUtilityResult.ReturnCode > 0)
            //{
            //    Logger.LogError("LGU Geoprocessing failed. Output:");
            //    Logger.LogError(processUtilityResult.StdOutAndStdErr);
            //    throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            //}

            //if (loadGeneratingUnitRefreshAreaID != null)
            //{
            //    DbContext.Database.ExecuteSqlRaw($"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID = {loadGeneratingUnitRefreshAreaID}");
            //}
            //else
            //{
            //    DbContext.Database.ExecuteSqlRaw("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
            //}

            var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions();
            using (var openStream = File.OpenRead(outputLayerPath))
            {
                var featureCollection = JsonSerializer.DeserializeAsync<FeatureCollection>(openStream, jsonSerializerOptions).Result;
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
                    var geometries = GeometryHelper.GeometryToDbGeometryAndMakeValidAndExplodeIfNeeded(loadGeneratingUnitResult.Geometry);

                    loadGeneratingUnits.AddRange(geometries.Select(dbGeometry => new LoadGeneratingUnit()
                    {
                        LoadGeneratingUnitGeometry = dbGeometry,
                        DelineationID = loadGeneratingUnitResult.DelineationID,
                        WaterQualityManagementPlanID = loadGeneratingUnitResult.WaterQualityManagementPlanID,
                        ModelBasinID = loadGeneratingUnitResult.ModelBasinID,
                        RegionalSubbasinID = loadGeneratingUnitResult.RegionalSubbasinID
                    }));
                }

                if (loadGeneratingUnits.Any())
                {
                    DbContext.LoadGeneratingUnits.AddRange(loadGeneratingUnits);
                    DbContext.SaveChanges();
                }

                if (loadGeneratingUnitRefreshArea != null)
                {
                    loadGeneratingUnitRefreshArea.ProcessDate = DateTime.Now;
                    DbContext.SaveChanges();
                }
            }

            // clean up temp files if not running in a local environment
            if (!_webHostEnvironment.IsDevelopment())
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