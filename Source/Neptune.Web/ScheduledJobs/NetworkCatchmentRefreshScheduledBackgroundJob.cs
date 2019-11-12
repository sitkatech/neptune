using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Neptune.Web.ScheduledJobs
{
    class NetworkCatchmentRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        public static string TestRunJob()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneWebConfiguration.RegionalSubbasinServiceUrl;
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
                        resultOffset = resultOffset,
                        resultRecordCount = 1000
                    };

                    var configurationSerialized = JsonConvert.SerializeObject(queryStringObject, Formatting.None,
                        new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                    var nameValueCollection =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(configurationSerialized);
                    var queryParameters = string.Join("&",
                        nameValueCollection.Select(x => $"{x.Key}={HttpUtility.UrlEncode((string) x.Value)}"));
                    var uri = $"{baseRequestUri}?{queryParameters}";
                    var response = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

                    resultOffset += 1000;

                    done = !JsonConvert.DeserializeObject<EsriQueryResponse>(response).ExceededTransferLimit;

                    var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(response);
                    collectedFeatureCollection.Features.AddRange(featureCollection.Features);
                }
            }

            var mergedResponse = JsonConvert.SerializeObject(collectedFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(mergedResponse,
                NeptuneWebConfiguration.DatabaseConnectionString, "NetworkCatchmentStaging",
                "CatchID as OCSurveyCatchmentID, DwnCatchID as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed");

            return mergedResponse;
        }

        protected override void RunJobImplementation()
        {
            TestRunJob();
        }
    }

    public class EsriQueryResponse
    {
        [JsonProperty("exceededTransferLimit")]
        public bool ExceededTransferLimit { get; set; }
    }
}