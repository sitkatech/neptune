using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.Jobs.Hangfire
{
    public class LoadGeneratingUnitRefreshJob
    {
        private readonly ILogger<LoadGeneratingUnitRefreshJob> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly NeptuneDbContext _dbContext;
        private readonly NeptuneJobConfiguration _neptuneJobConfiguration;
        private readonly SitkaSmtpClientService _sitkaSmtpClient;


        public LoadGeneratingUnitRefreshJob(ILogger<LoadGeneratingUnitRefreshJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext dbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _neptuneJobConfiguration = neptuneJobConfiguration.Value;
            _sitkaSmtpClient = sitkaSmtpClientService;
        }

        public async Task RunJob(int? loadGeneratingUnitRefreshAreaID)
        {
            var outputLayerName = $"LGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}.geojson";
            var clipLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}_inputClip.json";

            var additionalCommandLineArguments = new List<string> { outputFolder, outputLayerName};

            LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea = null;

            if (loadGeneratingUnitRefreshAreaID != null)
            {
                loadGeneratingUnitRefreshArea = await _dbContext.LoadGeneratingUnitRefreshAreas.FindAsync(loadGeneratingUnitRefreshAreaID.Value);
                var lguInputClipFeatures = _dbContext.LoadGeneratingUnits
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

                await File.WriteAllTextAsync(clipLayerPath, lguInputClipGeoJsonString);
                additionalCommandLineArguments.AddRange(new List<string>{
                    "--clip", clipLayerPath
                });
            }

            //todo: pyqgis
            // a PyQGIS script computes the LGU layer and saves it as a shapefile
            //var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{_neptuneJobConfiguration.PyqgisWorkingDirectory}ModelingOverlayAnalysis.py", _neptuneJobConfiguration.PyqgisWorkingDirectory, additionalCommandLineArguments);

            //if (processUtilityResult.ReturnCode > 0)
            //{
            //    _logger.LogError("LGU Geoprocessing failed. Output:");
            //    _logger.LogError(processUtilityResult.StdOutAndStdErr);
            //    throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            //}

            //if (loadGeneratingUnitRefreshAreaID != null)
            //{
            //    await _dbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID = {loadGeneratingUnitRefreshAreaID}");
            //}
            //else
            //{
            //    await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
            //}

            var jsonSerializerOptions = GeoJsonSerializer.DefaultSerializerOptions;
            await using (var openStream = File.OpenRead(outputLayerPath))
            {
                var featureCollection = await GeoJsonSerializer.GetFeatureCollectionFromGeoJsonStream(openStream, jsonSerializerOptions);
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
                    await _dbContext.LoadGeneratingUnits.AddRangeAsync(loadGeneratingUnits);
                    await _dbContext.SaveChangesAsync();
                }

                if (loadGeneratingUnitRefreshArea != null)
                {
                    loadGeneratingUnitRefreshArea.ProcessDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();
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