using Neptune.QGISAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Neptune.Common.Services.GDAL;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.QGISAPI.Controllers;

[ApiController]
public class QgisRunnerController : ControllerBase
{
    private readonly ILogger<QgisRunnerController> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly QgisService _qgisService;
    private readonly IAzureStorage _azureStorage;

    public QgisRunnerController(ILogger<QgisRunnerController> logger, NeptuneDbContext dbContext, QgisService qgisService,
        IAzureStorage azureStorage)
    {
        _logger = logger;
        _dbContext = dbContext;
        _qgisService = qgisService;
        _azureStorage = azureStorage;
    }

    [HttpGet("/")]
    public ActionResult Get()
    {
        return Ok("Hello from the QGIS API!");
    }

    [HttpPost("qgis/generate-plgus")]
    public async Task<IActionResult> GenerateProjectLoadGeneratingUnits([FromForm] GenerateProjectLoadGeneratingUnitRequestDto requestDto)
    {
        var projectID = requestDto.ProjectID;
        var project = _dbContext.Projects.AsNoTracking().SingleOrDefault(x => x.ProjectID == projectID);
        if (project == null)
        {
            return NotFound($"Project with ID {projectID} does not exist!");
        }

        var projectRegionalSubbasinIDs = _dbContext.TreatmentBMPs.AsNoTracking().Where(x => x.ProjectID == projectID).Select(x => x.RegionalSubbasinID).Distinct().ToList();

        var regionalSubbasinIDs = _dbContext.vRegionalSubbasinUpstreams.AsNoTracking()
            .Where(x => projectRegionalSubbasinIDs.Contains(x.PrimaryKey) && x.RegionalSubbasinID.HasValue).Select(x => x.RegionalSubbasinID.Value).ToList();

        var regionalSubbasinInputFeatures = _dbContext.vPyQgisRegionalSubbasinLGUInputs.AsNoTracking()
            .Where(x => regionalSubbasinIDs.Contains(x.RSBID)).Select(x =>
                new Feature(x.CatchmentGeometry, new AttributesTable { { "RSBID", x.RSBID }, { "ModelID", x.ModelID } }))
            .ToList();
        var lguInputs = _dbContext.vPyQgisProjectDelineationLGUInputs.AsNoTracking()
            .Where(x => x.ProjectID == null || x.ProjectID == projectID).Select(x =>
                new Feature(x.DelineationGeometry, new AttributesTable { { "DelinID", x.DelinID } })).ToList();
        var outputFolder = Path.GetTempPath();
        var outputLayerPrefix = $"{"PLGU"}{DateTime.Now.Ticks}";
        var featureCollection = await GenerateLgUsImpl(regionalSubbasinInputFeatures, lguInputs, outputFolder, outputLayerPrefix, regionalSubbasinIDs, null);
        var projectLoadGeneratingUnits = new List<ProjectLoadGeneratingUnit>();

        foreach (var feature in featureCollection.Where(x => x.Geometry != null).ToList())
        {
            var loadGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<LoadGeneratingUnitResult>(feature, GeoJsonSerializer.DefaultSerializerOptions);

            // we should only get Polygons from the Pyqgis rodeo overlay, but when we convert geojson to Geometry, they can result in invalid geometries
            // however, when we run makevalid, it can potentially change the geometry type from Polygon to a MultiPolygon or GeometryCollection
            // so we need to explode them if that happens since we are only expecting polygons for LGUs
            var geometries = GeometryHelper.MakeValidAndExplodeIfNeeded(loadGeneratingUnitResult.Geometry);

            projectLoadGeneratingUnits.AddRange(geometries.Select(dbGeometry =>
                new ProjectLoadGeneratingUnit
                {
                    ProjectLoadGeneratingUnitGeometry = dbGeometry,
                    ProjectID = projectID,
                    DelineationID = loadGeneratingUnitResult.DelineationID,
                    WaterQualityManagementPlanID = loadGeneratingUnitResult.WaterQualityManagementPlanID,
                    ModelBasinID = loadGeneratingUnitResult.ModelBasinID,
                    RegionalSubbasinID = loadGeneratingUnitResult.RegionalSubbasinID
                }));
        }

        await _dbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID = {projectID}");

        if (projectLoadGeneratingUnits.Any())
        {
            await _dbContext.ProjectLoadGeneratingUnits.AddRangeAsync(projectLoadGeneratingUnits);
            await _dbContext.SaveChangesAsync();
        }

        DeleteTempFiles(outputFolder, outputLayerPrefix);
        return Ok();
    }

    [HttpPost("qgis/generate-lgus")]
    public async Task<IActionResult> GenerateLoadGeneratingUnits([FromForm] GenerateLoadGeneratingUnitRequestDto requestDto)
    {
        var outputFolder = Path.GetTempPath();
        var outputLayerPrefix = $"LGU{DateTime.Now.Ticks}";
        var lguInputFeatures = _dbContext.vPyQgisDelineationLGUInputs.AsNoTracking().Select(x =>
            new Feature(x.DelineationGeometry, new AttributesTable { { "DelinID", x.DelinID } })).ToList();
        var regionalSubbasinInputFeatures = _dbContext.vPyQgisRegionalSubbasinLGUInputs.AsNoTracking().Select(x =>
            new Feature(x.CatchmentGeometry, new AttributesTable { { "RSBID", x.RSBID }, { "ModelID", x.ModelID } })).ToList();
        var clipLayerPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}_inputClip.json";
        var loadGeneratingUnitRefreshArea = await CreateLoadGeneratingUnitRefreshAreaIfProvided(requestDto, clipLayerPath);
        var featureCollection = await GenerateLgUsImpl(regionalSubbasinInputFeatures, lguInputFeatures, outputFolder, outputLayerPrefix, new List<int>(), clipLayerPath);


        if (loadGeneratingUnitRefreshArea != null)
        {
            await _dbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID = {loadGeneratingUnitRefreshArea}");
        }
        else
        {
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
        }

        var loadGeneratingUnits = new List<LoadGeneratingUnit>();

        foreach (var feature in featureCollection.Where(x => x.Geometry != null).ToList())
        {
            var loadGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<LoadGeneratingUnitResult>(feature, GeoJsonSerializer.DefaultSerializerOptions);

            // we should only get Polygons from the Pyqgis rodeo overlay, but when we convert geojson to Geometry, they can result in invalid geometries
            // however, when we run makevalid, it can potentially change the geometry type from Polygon to a MultiPolygon or GeometryCollection
            // so we need to explode them if that happens since we are only expecting polygons for LGUs
            var geometries = GeometryHelper.MakeValidAndExplodeIfNeeded(loadGeneratingUnitResult.Geometry);

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

        DeleteTempFiles(outputFolder, outputLayerPrefix);
        return Ok();
    }

    private async Task<FeatureCollection> GenerateLgUsImpl(IEnumerable<Feature> regionalSubbasinInputFeatures, IEnumerable<Feature> lguInputFeatures, string outputFolder, string outputLayerPrefix, List<int> regionalSubbasinIDs, string? clipLayerPath)
    {
        var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}.geojson";
        var lguInputPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}delineationLayer.geojson";
        var modelBasinInputPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}modelBasinLayer.geojson";
        var regionalSubbasinInputPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}regionalSubbasinLayer.geojson";
        var wqmpInputPath = $"{Path.Combine(outputFolder, outputLayerPrefix)}wqmpLayer.geojson";
        var additionalCommandLineArguments = new List<string>
        {
            "ModelingOverlayAnalysis.py", outputLayerPrefix, lguInputPath, modelBasinInputPath, regionalSubbasinInputPath
        };
        if (regionalSubbasinIDs.Any())
        {
            additionalCommandLineArguments.AddRange(new List<string> { "--rsb_ids", string.Join(", ", regionalSubbasinIDs) });
        }
        if (!string.IsNullOrWhiteSpace(clipLayerPath))
        {
            additionalCommandLineArguments.AddRange(new List<string> { "--clip", clipLayerPath });
        }

        await WriteFeaturesToGeoJsonFile(lguInputPath, lguInputFeatures);

        var modelBasinInputFeatures = _dbContext.vPyQgisModelBasinLGUInputs.AsNoTracking().Select(x =>
            new Feature(x.ModelBasinGeometry, new AttributesTable { { "ModelID", x.ModelID } })).ToList();
        await WriteFeaturesToGeoJsonFile(modelBasinInputPath, modelBasinInputFeatures);

        await WriteFeaturesToGeoJsonFile(regionalSubbasinInputPath, regionalSubbasinInputFeatures);

        var wqmpInputFeatures = _dbContext.vPyQgisWaterQualityManagementPlanLGUInputs.AsNoTracking().Select(x =>
            new Feature(x.WaterQualityManagementPlanBoundary, new AttributesTable { { "WQMPID", x.WQMPID } })).ToList();
        await WriteFeaturesToGeoJsonFile(wqmpInputPath, wqmpInputFeatures);

        _qgisService.Run(additionalCommandLineArguments.ToDictionary(x => x, x => false));

        await using var openStream = System.IO.File.OpenRead(outputLayerPath);
        var featureCollection =
            await GeoJsonSerializer.GetFeatureCollectionFromGeoJsonStream(openStream,
                GeoJsonSerializer.DefaultSerializerOptions);
        return featureCollection;
    }

    private async Task<LoadGeneratingUnitRefreshArea?> CreateLoadGeneratingUnitRefreshAreaIfProvided(GenerateLoadGeneratingUnitRequestDto requestDto,
        string clipLayerPath)
    {
        LoadGeneratingUnitRefreshArea? loadGeneratingUnitRefreshArea = null;
        var loadGeneratingUnitRefreshAreaID = requestDto.LoadGeneratingUnitRefreshAreaID;
        if (loadGeneratingUnitRefreshAreaID != null)
        {
            loadGeneratingUnitRefreshArea =
                await _dbContext.LoadGeneratingUnitRefreshAreas.FindAsync(loadGeneratingUnitRefreshAreaID.Value);
            var loadGeneratingUnitRefreshAreaGeometry = loadGeneratingUnitRefreshArea
                .LoadGeneratingUnitRefreshAreaGeometry;
            var lguInputClipFeatures = _dbContext.LoadGeneratingUnits
                .Where(x => x.LoadGeneratingUnitGeometry.Intersects(loadGeneratingUnitRefreshAreaGeometry)).ToList()
                .Select(x => new Feature(x.LoadGeneratingUnitGeometry, new AttributesTable())).ToList();

            await WriteFeaturesToGeoJsonFile(clipLayerPath, lguInputClipFeatures, loadGeneratingUnitRefreshAreaGeometry);
        }

        return loadGeneratingUnitRefreshArea;
    }

    private static async Task WriteFeaturesToGeoJsonFile(string outputFilePath, IEnumerable<Feature> features)
    {
        await WriteFeaturesToGeoJsonFile(outputFilePath, features, null);
    }

    private static async Task WriteFeaturesToGeoJsonFile(string outputFilePath, IEnumerable<Feature> features, Geometry? extraGeometryToAppend)
    {
        var featureCollection = new FeatureCollection();
        foreach (var feature in features)
        {
            featureCollection.Add(feature);
        }

        if (extraGeometryToAppend != null)
        {
            featureCollection.Add(new Feature(extraGeometryToAppend, new AttributesTable()));
        }

        var geoJsonString = GeoJsonSerializer.Serialize(featureCollection);
        await System.IO.File.WriteAllTextAsync(outputFilePath, geoJsonString);
    }

    private static void DeleteTempFiles(string outputFolder, string outputLayerPrefix)
    {
        // clean up temp files if not running in a local environment
        foreach (var fileToDelete in Directory.EnumerateFiles(outputFolder, outputLayerPrefix + "*"))
        {
            System.IO.File.Delete(fileToDelete);
        }
    }
}