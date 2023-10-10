using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Neptune.API.Hangfire
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<TrashGeneratingUnitRefreshScheduledBackgroundJob>
    {
        public const string JobName = "TGU Refresh";

        public TrashGeneratingUnitRefreshScheduledBackgroundJob(ILogger<TrashGeneratingUnitRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            TrashGeneratingUnitRefreshImpl();
        }

        protected virtual void TrashGeneratingUnitRefreshImpl()
        {
            var outputLayerName = $"TGU{DateTime.Now.Ticks}";
            var outputFolder = Path.GetTempPath();
            var outputLayerPath = $"{Path.Combine(outputFolder, outputLayerName)}.geojson";

            //todo: pyqgis
            //// a PyQGIS script computes the TGU layer and saves it as a geojson
            //var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneConfiguration.PyqgisWorkingDirectory}ComputeTrashGeneratingUnits.py", NeptuneConfiguration.PyqgisWorkingDirectory, new List<string>{outputFolder,
            //    outputLayerName});

            //if (processUtilityResult.ReturnCode > 0)
            //{
            //    Logger.LogError("TGU Geoprocessing failed. Output:");
            //    Logger.LogError(processUtilityResult.StdOutAndStdErr);
            //    throw new GeoprocessingException(processUtilityResult.StdOutAndStdErr);
            //}

            //Logger.LogInformation("QGIS output:");
            //Logger.LogInformation(processUtilityResult.StdOutAndStdErr);

            SaveTrashGeneratingUnits(outputLayerPath, DbContext);

            // clean up temp files if not running in a local environment
            if (!_webHostEnvironment.IsDevelopment())
            {
                File.Delete(outputLayerPath);
            }
        }

        public static void SaveTrashGeneratingUnits(string outputLayerPath, NeptuneDbContext dbContext)
        {
            // kill the old TGUs
            dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.TrashGeneratingUnit");
            var jsonSerializerOptions = GeoJsonSerializer.CreateGeoJSONSerializerOptions();
            using var openStream = File.OpenRead(outputLayerPath);
            var featureCollection = JsonSerializer.DeserializeAsync<FeatureCollection>(openStream, jsonSerializerOptions)
                .Result;
            var features = featureCollection.Where(x =>
                x.Geometry != null && x.Attributes["LUBID"] != null && x.Attributes["SJID"] != null).ToList();
            var trashGeneratingUnits = new List<TrashGeneratingUnit>();
            var trashGeneratingUnit4326s = new List<TrashGeneratingUnit4326>();
            foreach (var feature in features)
            {
                // TODO: We need to handle GeometryCollections, i.e. Polygons + Linestrings; we ideally want to remove the linestrings and convert to a MultiPolygon
                var trashGeneratingUnitResult = GeoJsonSerializer.DeserializeFromFeature<TrashGeneratingUnitResult>(feature,
                    jsonSerializerOptions);
                var stormwaterJurisdictionID = trashGeneratingUnitResult.StormwaterJurisdictionID;
                var delineationID = trashGeneratingUnitResult.DelineationID;
                var waterQualityManagementPlanID = trashGeneratingUnitResult.WaterQualityManagementPlanID;
                var landUseBlockID = trashGeneratingUnitResult.LandUseBlockID;
                var onlandVisualTrashAssessmentAreaID = trashGeneratingUnitResult.OnlandVisualTrashAssessmentAreaID;
                var trashGeneratingUnit = new TrashGeneratingUnit
                {
                    StormwaterJurisdictionID = stormwaterJurisdictionID,
                    TrashGeneratingUnitGeometry = trashGeneratingUnitResult.Geometry,
                    DelineationID = delineationID,
                    WaterQualityManagementPlanID = waterQualityManagementPlanID,
                    LandUseBlockID = landUseBlockID,
                    OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID,
                    LastUpdateDate = DateTime.Now
                };

                trashGeneratingUnits.Add(trashGeneratingUnit);
                var trashGeneratingUnit4326 = new TrashGeneratingUnit4326
                {
                    StormwaterJurisdictionID = stormwaterJurisdictionID,
                    TrashGeneratingUnit4326Geometry = trashGeneratingUnitResult.Geometry.ProjectTo4326(),
                    DelineationID = delineationID,
                    WaterQualityManagementPlanID = waterQualityManagementPlanID,
                    LandUseBlockID = landUseBlockID,
                    OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID,
                    LastUpdateDate = DateTime.Now
                };
                trashGeneratingUnit4326s.Add(trashGeneratingUnit4326);
            }

            if (trashGeneratingUnits.Any())
            {
                dbContext.TrashGeneratingUnits.AddRange(trashGeneratingUnits);
                dbContext.SaveChanges();
            }

            // repeat but with 4326
            dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.TrashGeneratingUnit4326");
            if (trashGeneratingUnit4326s.Any())
            {
                dbContext.TrashGeneratingUnit4326s.AddRange(trashGeneratingUnit4326s);
                dbContext.SaveChanges();
            }

            // we get invalid geometries from qgis so we need to make them valid
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pTrashGeneratingUnitsMakeValid");
        }
    }
}