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
    public class OCTAPrioritizationRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<OCTAPrioritizationRefreshScheduledBackgroundJob>
    {
        public const string JobName = "OCTA Prioritization Refresh";

        public int PersonID { get; set; }

        public OCTAPrioritizationRefreshScheduledBackgroundJob(ILogger<OCTAPrioritizationRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            PersonID = personID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };


        public void RunRefresh(NeptuneDbContext dbContext, Person person)
        {
            dbContext.OCTAPrioritizationStagings.ExecuteDelete();

            var featureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfOCTAPrioritizationKeyNotUnique(featureCollection);
            StageFeatureCollection(featureCollection);
            MergeAndReproject(dbContext, person);
        }

        private static void MergeAndReproject(NeptuneDbContext dbContext, Person person)
        {
            dbContext.Database.SetCommandTimeout(30000);
            dbContext.Database.ExecuteSqlRaw("EXEC dbo.pOCTAPrioritizationUpdateFromStaging");
            foreach (var octaPrioritization in dbContext.OCTAPrioritizations)
            {
                octaPrioritization.OCTAPrioritizationGeometry4326 = octaPrioritization.OCTAPrioritizationGeometry.ProjectTo4326();
            }
            dbContext.SaveChanges();
        }

        private void StageFeatureCollection(FeatureCollection featureCollection)
        {
            var jsonFeatureCollection = GeoJsonSerializer.Serialize(featureCollection);
            //todo: gdalapi
            //var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneConfiguration.Ogr2OgrExecutable,
            //    Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            //ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
            //    NeptuneConfiguration.DatabaseConnectionString, "OCTAPrioritizationStaging",
            //    "ix as OCTAPrioritizationKey, Watershed as Watershed, CID as CatchIDN, TPI as TPI, WQNLU as WQNLU, WQNMON as WQNMON, IMPAIR as IMPAIR, MON as MON, SEA as SEA, SEA_PCTL as SEA_PCTL, PC_VOL_PCT as PC_VOL_PCT, PC_NUT_PCT as PC_NUT_PCT, PC_BAC_PCT as PC_BAC_PCT, PC_MET_PCT as PC_MET_PCT, PC_TSS_PCT as PC_TSS_PCT",
            //    Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfOCTAPrioritizationKeyNotUnique(FeatureCollection featureCollection)
        {
            var octaPrioritizationKeysThatAreNotUnique = featureCollection
                .GroupBy(x => x.Attributes["ix"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (octaPrioritizationKeysThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The OCTA Prioritization service returned an invalid collection. The following IDs are duplicated:\n{string.Join(", ", octaPrioritizationKeysThatAreNotUnique)}");
            }
        }

        private FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneConfiguration.OCTAPrioritizationServiceUrl;
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
                            "The OCTA Prioritization service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            "The OCTA Prioritization service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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