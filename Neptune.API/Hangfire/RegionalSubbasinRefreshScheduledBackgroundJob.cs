using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.API.Hangfire
{
    public class RegionalSubbasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<RegionalSubbasinRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Regional Subbasin Refresh";

        public bool QueueLGURefresh { get; set; }

        public int PersonID { get; set; }

        public RegionalSubbasinRefreshScheduledBackgroundJob(ILogger<RegionalSubbasinRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, int personID, bool queueLGURefresh) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            PersonID = personID;
            QueueLGURefresh = queueLGURefresh;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };
    
        public void RunRefresh(NeptuneDbContext dbContext, bool queueLguRefresh)
        {
            dbContext.RegionalSubbasinStagings.ExecuteDelete();

            var newRegionalSubbasinFeatureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfCatchIdnNotUnique(newRegionalSubbasinFeatureCollection);
            StageFeatureCollection(newRegionalSubbasinFeatureCollection);
            ThrowIfDownstreamInvalid(dbContext);
            DeleteLoadGeneratingUnits(dbContext);
            MergeAndReproject(dbContext);
            RefreshCentralizedDelineations(dbContext);

            BackgroundJob.Enqueue<DelineationDiscrepancyCheckerBackgroundJob>(x => x.RunJob(null));

            if (queueLguRefresh)
            {
                UpdateLoadGeneratingUnits();
            }
        }

        private static void DeleteLoadGeneratingUnits(NeptuneDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
        }

        private static void UpdateLoadGeneratingUnits()
        {
            // Instead, just queue a total LGU update
            BackgroundJob.Enqueue<LoadGeneratingUnitRefreshScheduledBackgroundJob>(x => x.RunJob(null));

            // And follow it up with an HRU update
            BackgroundJob.Enqueue<HRURefreshBackgroundJob>(x => x.RunJob(null));
        }

        private static void RefreshCentralizedDelineations(NeptuneDbContext dbContext)
        {
            foreach (var delineation in dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Centralized.DelineationTypeID))
            {
                var centralizedDelineationGeometry2771 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry2771(dbContext);
                var centralizedDelineationGeometry4326 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry4326(dbContext);

                delineation.DelineationGeometry = centralizedDelineationGeometry2771;
                delineation.DelineationGeometry4326 = centralizedDelineationGeometry4326;

                delineation.DateLastModified = DateTime.Now;
            }

            dbContext.SaveChanges();
        }

        private static void MergeAndReproject(NeptuneDbContext dbContext)
        {
            // MergeListHelper doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.SetCommandTimeout(30000);
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");

            // unfortunately, now we have to create the catchment geometries in 4326, since SQL isn't capable of doing this.
            dbContext.RegionalSubbasins.Load();
            dbContext.Watersheds.Load();
            foreach (var regionalSubbasin in dbContext.RegionalSubbasins)
            {
                regionalSubbasin.CatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry.ProjectTo4326();
            }

            // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
            foreach (var watershed in dbContext.Watersheds)
            {
                watershed.WatershedGeometry4326 = watershed.WatershedGeometry.ProjectTo4326();
            }
            dbContext.SaveChanges();
            
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pTreatmentBMPUpdateWatershed");
            dbContext.Database.SetCommandTimeout(30000);
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        }

        private static void ThrowIfDownstreamInvalid(NeptuneDbContext dbContext)
        {
            // this is done against the staged feature collection because it's easier to implement in LINQ than against the raw JSON response
            var ocSurveyCatchmentIDs = dbContext.RegionalSubbasinStagings.Select(x => x.OCSurveyCatchmentID).ToList();

            dbContext.Database.SetCommandTimeout(30000);
            var stagedRegionalSubbasinsWithBrokenDownstreamRel = dbContext.RegionalSubbasinStagings.Where(x =>
                    x.OCSurveyDownstreamCatchmentID != 0 && !ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID))
                .ToList();

            if (stagedRegionalSubbasinsWithBrokenDownstreamRel.Any())
            {
                throw new RemoteServiceException(
                    $"The Regional Subbasin service returned an invalid collection. The catchments with the following IDs have invalid downstream catchment IDs:\n{string.Join(", ", stagedRegionalSubbasinsWithBrokenDownstreamRel.Select(x => x.OCSurveyCatchmentID))}");
            }
        }

        private void StageFeatureCollection(FeatureCollection newFeatureCollection)
        {
            var jsonFeatureCollection = GeoJsonSerializer.Serialize(newFeatureCollection);
            //todo: gdalapi
            //var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneConfiguration.Ogr2OgrExecutable,
            //    Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            //ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
            //    NeptuneConfiguration.DatabaseConnectionString, "RegionalSubbasinStaging",
            //    "CatchIDN as OCSurveyCatchmentID, DwnCatchIDN as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
            //    // transform from 2230 to 2771 here to avoid precision errors introduced by asking arc to do it
            //    Proj4NetHelper.NAD_83_CA_ZONE_VI_SRID, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfCatchIdnNotUnique(FeatureCollection newRegionalSubbasinFeatureCollection)
        {
            var catchIdnsThatAreNotUnique = newRegionalSubbasinFeatureCollection
                .GroupBy(x => x.Attributes["CatchIDN"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (catchIdnsThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The Regional Subbasin service returned an invalid collection. The following Catchment IDs are duplicated:\n{string.Join(", ", catchIdnsThatAreNotUnique)}");
            }
        }

        private FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneConfiguration.RegionalSubbasinServiceUrl;
                var done = false;

                while (!done)
                {
                    var queryStringObject = new
                    {
                        where = "1=1",
                        geometryType = "esriGeometryEnvelope",
                        spatialRel = "esriSpatialRelIntersects",
                        outFields = "*",
                        returnGeometry = true,
                        returnTrueCurves = false,
                        outSR = 2230,
                        returnIdsOnly = false,
                        returnCountOnly = false,
                        returnZ = false,
                        returnM = false,
                        returnDistinctValues = false,
                        returnExtentOnly = false,
                        f = "geojson",
                        resultOffset,
                        resultRecordCount = 1000
                    };

                    var configurationSerialized = GeoJsonSerializer.Serialize(queryStringObject);
                    //todo:, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                    var nameValueCollection = GeoJsonSerializer.Deserialize<Dictionary<string, string>>(configurationSerialized);
                    var queryParameters = string.Join("&",
                        nameValueCollection.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
                    var uri = $"{baseRequestUri}?{queryParameters}";
                    string response;
                    try
                    {
                        response = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
                    }
                    catch (TaskCanceledException tce)
                    {
                        throw new RemoteServiceException(
                            "The Regional Subbasin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                            tce);
                    }

                    resultOffset += 1000;
                    try
                    {
                        done = !GeoJsonSerializer.Deserialize<EsriQueryResponse>(response).ExceededTransferLimit;
                    }
                    catch (JsonException jre)
                    {
                        throw new RemoteServiceException(
                            "The Regional Subbasin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                            jre);
                    }

                    var featureCollection = GeoJsonSerializer.Deserialize<FeatureCollection>(response);
                    foreach (var feature in featureCollection)
                    {
                        collectedFeatureCollection.Add(feature);
                    }
                }
            }

            return collectedFeatureCollection;
        }

        protected override void RunJobImplementation()
        {
            var person = DbContext.People.Find(PersonID);
            RunRefresh(DbContext, QueueLGURefresh);
        }
    }
}