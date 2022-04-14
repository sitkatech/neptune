using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.ScheduledJobs
{
    public class OCTAPrioritizationRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public OCTAPrioritizationRefreshScheduledBackgroundJob(int currentPersonPersonID)
        {
            PersonID = currentPersonPersonID;
        }

        public int PersonID { get; set; }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        public static void RunRefresh(DatabaseEntities dbContext, Person person)
        {
            dbContext.OCTAPrioritizationStagings.DeleteOCTAPrioritizationStaging(dbContext.OCTAPrioritizationStagings.ToList());
            dbContext.SaveChanges(person);

            var featureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfOCTAPrioritizationKeyNotUnique(featureCollection);
            StageFeatureCollection(featureCollection);
            MergeAndReproject(dbContext, person);
        }

        private static void MergeAndReproject(DatabaseEntities dbContext, Person person)
        {
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pOCTAPrioritizationUpdateFromStaging");
            foreach (var octaPrioritization in dbContext.OCTAPrioritizations)
            {
                octaPrioritization.OCTAPrioritizationGeometry4326 =
                                    CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(octaPrioritization.OCTAPrioritizationGeometry);
            }
            dbContext.SaveChanges(person);
        }

        private static void StageFeatureCollection(FeatureCollection newOCTAPrioritizationFeatureCollection)
        {
            var jsonFeatureCollection = JsonConvert.SerializeObject(newOCTAPrioritizationFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "OCTAPrioritizationStaging",
                "ix as OCTAPrioritizationKey, Watershed as Watershed, CID as CatchIDN, TPI as TPI, WQNLU as WQNLU, WQNMON as WQNMON, IMPAIR as IMPAIR, MON as MON, SEA as SEA, SEA_PCTL as SEA_PCTL, PC_VOL_PCT as PC_VOL_PCT, PC_NUT_PCT as PC_NUT_PCT, PC_BAC_PCT as PC_BAC_PCT, PC_MET_PCT as PC_MET_PCT, PC_TSS_PCT as PC_TSS_PCT",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfOCTAPrioritizationKeyNotUnique(FeatureCollection featureCollection)
        {
            var octaPrioritizationKeysThatAreNotUnique = featureCollection.Features
                .GroupBy(x => x.Properties["ix"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (octaPrioritizationKeysThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The OCTA Prioritization service returned an invalid collection. The following IDs are duplicated:\n{string.Join(", ", octaPrioritizationKeysThatAreNotUnique)}");
            }
        }

        private static FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneWebConfiguration.OCTAPrioritizationServiceUrl;
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

                    var configurationSerialized = JsonConvert.SerializeObject(queryStringObject, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var nameValueCollection =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(configurationSerialized);
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
                        done = !JsonConvert.DeserializeObject<EsriQueryResponse>(response).ExceededTransferLimit;
                    }
                    catch (JsonReaderException jre)
                    {
                        throw new RemoteServiceException(
                            "The OCTA Prioritization service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                            jre);
                    }

                    var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(response);
                    collectedFeatureCollection.Features.AddRange(featureCollection.Features);
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