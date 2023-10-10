using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
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
    public class PrecipitationZoneRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<PrecipitationZoneRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Precipitation Zone Refresh";

        public int PersonID { get; set; }

        public PrecipitationZoneRefreshScheduledBackgroundJob(ILogger<PrecipitationZoneRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            PersonID = personID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        public void RunRefresh(NeptuneDbContext dbContext, Person person)
        {
            dbContext.PrecipitationZoneStagings.ExecuteDelete();
            dbContext.SaveChanges();
            
            var featureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfPrecipitationZoneKeyNotUnique(featureCollection);
            StageFeatureCollection(featureCollection);
            MergeAndUpdateTreatmentBMPProperties(dbContext);
        }

        private static void MergeAndUpdateTreatmentBMPProperties(NeptuneDbContext dbContext)
        {
            // MergeListHelper is doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.SetCommandTimeout(30000);
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pPrecipitationZoneUpdateFromStaging");
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pTreatmentBMPUpdatePrecipitationZone");
        }

        private void StageFeatureCollection(FeatureCollection newFeatureCollection)
        {
            var jsonFeatureCollection = GeoJsonSerializer.Serialize(newFeatureCollection);
            //todo: gdalapi
            //var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneConfiguration.Ogr2OgrExecutable,
            //    Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            //ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
            //    NeptuneConfiguration.DatabaseConnectionString, "PrecipitationZoneStaging",
            //    "ID as PrecipitationZoneKey, RainfallZo as DesignStormwaterDepthInInches",
            //    Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfPrecipitationZoneKeyNotUnique(FeatureCollection featureCollection)
        {
            var precipitationZoneKeysThatAreNotUnique = featureCollection
                .GroupBy(x => x.Attributes["ID"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (precipitationZoneKeysThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The Precipitation Zone service returned an invalid collection. The following IDs are duplicated:\n{string.Join(", ", precipitationZoneKeysThatAreNotUnique)}");
            }
        }

        private FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneConfiguration.PrecipitationZoneServiceUrl;
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
                        outSR = 2771,
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
                            "The Precipitation Zone service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            "The Precipitation Zone service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
            RunRefresh(DbContext, person);
        }
    }
}