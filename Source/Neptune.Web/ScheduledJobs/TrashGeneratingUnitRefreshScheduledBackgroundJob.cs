using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Neptune.Web.ScheduledJobs
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "TGU Refresh";

        // only run in PROD--job takes a relatively long time to run, and QA and local can just refresh their data from PROD backups.
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Prod, NeptuneEnvironmentType.Qa, NeptuneEnvironmentType.Local
        };

        protected override void RunJobImplementation()
        {
            TrashGeneratingUnitRefreshImpl();
        }

        protected virtual void TrashGeneratingUnitRefreshImpl()
        {
            Logger.Info($"Processing '{JobName}'");

            var layerName = $"TGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputFilename = $"{layerName}.geojson";
            var outputPath = $"{Path.Combine(outputFolder, outputFilename)}";

            // a PyQGIS script computes the TGU layer and saves it as a geojson
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}ComputeTrashGeneratingUnits.py", NeptuneWebConfiguration.PyqgisWorkingDirectory, outputFolder,
                outputFilename);

            if (processUtilityResult.ReturnCode > 0)
            {
                Logger.Error("TGU Geoprocessing failed. Output:");
                Logger.Error(processUtilityResult.StdOutAndStdErr);
                throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            }

            Logger.Info("QGIS output:");
            Logger.Info(processUtilityResult.StdOutAndStdErr);

            // kill the old TGUs
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit");
            var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions(4, 2);
            using var openStream = File.OpenRead(outputPath);
            var featureCollection =
                JsonSerializer.DeserializeAsync<FeatureCollection>(openStream, jsonSerializerOptions).Result;
            var features = featureCollection.Where(x =>
                x.Geometry != null && x.Attributes["LUBID"] != null && x.Attributes["SJID"] != null).ToList();
            var trashGeneratingUnits = new List<TrashGeneratingUnit>();
            var trashGeneratingUnit4326s = new List<TrashGeneratingUnit4326>();
            foreach (var feature in features)
            {
                var trashGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<TrashGeneratingUnitResult>(feature,
                    jsonSerializerOptions);
                var stormwaterJurisdictionID = trashGeneratingUnitResult.StormwaterJurisdictionID;
                var delineationID = trashGeneratingUnitResult.DelineationID;
                var waterQualityManagementPlanID = trashGeneratingUnitResult.WaterQualityManagementPlanID;
                var landUseBlockID = trashGeneratingUnitResult.LandUseBlockID;
                var onlandVisualTrashAssessmentAreaID = trashGeneratingUnitResult.OnlandVisualTrashAssessmentAreaID;
                var trashGeneratingUnit = new TrashGeneratingUnit(stormwaterJurisdictionID,
                    DbGeometry.FromBinary(trashGeneratingUnitResult.Geometry.AsBinary(), CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID))
                {
                    DelineationID = delineationID,
                    WaterQualityManagementPlanID = waterQualityManagementPlanID,
                    LandUseBlockID = landUseBlockID,
                    OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID
                };
                
                trashGeneratingUnits.Add(trashGeneratingUnit);
                var trashGeneratingUnit4326 = new TrashGeneratingUnit4326(stormwaterJurisdictionID,
                    DbGeometry.FromBinary(trashGeneratingUnitResult.Geometry.ProjectTo4326().AsBinary(), CoordinateSystemHelper.WGS_1984_SRID))
                {
                    DelineationID = delineationID,
                    WaterQualityManagementPlanID = waterQualityManagementPlanID,
                    LandUseBlockID = landUseBlockID,
                    OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID
                };
                trashGeneratingUnit4326s.Add(trashGeneratingUnit4326);
            }

            if (trashGeneratingUnits.Any())
            {
                DbContext.TrashGeneratingUnits.AddRange(trashGeneratingUnits);
                DbContext.SaveChangesWithNoAuditing();
            }

            // repeat but with 4326
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.TrashGeneratingUnit4326");
            if (trashGeneratingUnit4326s.Any())
            {
                DbContext.TrashGeneratingUnit4326s.AddRange(trashGeneratingUnit4326s);
                DbContext.SaveChangesWithNoAuditing();
            }
        }
    }
}

public class TrashGeneratingUnitResult : IHasGeometry
{
    [JsonPropertyName("LUBID")]
    public int LandUseBlockID { get; set; }
    [JsonPropertyName("SJID")]
    public int StormwaterJurisdictionID { get; set; }
    [JsonPropertyName("DelinID")]
    public int? DelineationID { get; set; }
    [JsonPropertyName("WQMPID")]
    public int? WaterQualityManagementPlanID { get; set; }
    [JsonPropertyName("OVTAID")]
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public Geometry Geometry { get; set; }
}

[TestFixture]
public class GeoJsonTest
{
    [Test]
    public void CanDeserailizeGeojson()
    {
        var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions(4, 2);
        var outputPath = $"c:/temp/test7.geojson";
        using (var openStream = File.OpenRead(outputPath))
        {
            var featureCollection = JsonSerializer.DeserializeAsync<FeatureCollection>(openStream, jsonSerializerOptions).Result;
            var features = featureCollection.Where(x => x.Geometry != null && x.Attributes["LUBID"] != null && x.Attributes["SJID"] != null).ToList();
            Assert.That(features.Count, Is.GreaterThan(0));
        }
    }
}