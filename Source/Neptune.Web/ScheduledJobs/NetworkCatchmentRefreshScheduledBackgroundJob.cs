using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using ApprovalUtilities.Utilities;
using Neptune.Web.Models;

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
            HttpRequestStorage.DatabaseEntities.NetworkCatchmentStagings.DeleteNetworkCatchmentStaging(HttpRequestStorage.DatabaseEntities.NetworkCatchmentStagings.ToList());
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            //var readAllText = File.ReadAllText(@"C:\Users\nick.padinha\Documents\Neptune\networkcatchmos\networkcatchmos.json");
            //var collectedFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(readAllText);

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
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var nameValueCollection =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(configurationSerialized);
                    var queryParameters = string.Join("&",
                        nameValueCollection.Select(x => $"{x.Key}={HttpUtility.UrlEncode((string)x.Value)}"));
                    var uri = $"{baseRequestUri}?{queryParameters}";
                    var response = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

                    resultOffset += 1000;

                    done = !JsonConvert.DeserializeObject<EsriQueryResponse>(response).ExceededTransferLimit;

                    var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(response);
                    collectedFeatureCollection.Features.AddRange(featureCollection.Features);
                }
            }

            // build a new feature collection where Watershed,CatchID is unique
            // shouldn't have to do this, but the data source is u n r e l i a b l e

            IEnumerable<Feature> enumerable = collectedFeatureCollection.Features.GroupBy(x => new
                    { Watershed = x.Properties["Watershed"], CatchID = x.Properties["CatchID"] })
                .Select(x => x.FirstOrDefault());

            var goodFeatureCollection = new FeatureCollection(enumerable.ToList());

            var mergedResponse = JsonConvert.SerializeObject(goodFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(mergedResponse,
                NeptuneWebConfiguration.DatabaseConnectionString, "NetworkCatchmentStaging",
                "CatchID as OCSurveyCatchmentID, DwnCatchID as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

            // merge the things

            HttpRequestStorage.DatabaseEntities.NetworkCatchments.Load();

            var existingNetworkCatchments = HttpRequestStorage.DatabaseEntities.NetworkCatchments.Local;
            var newNetworkCatchments = HttpRequestStorage.DatabaseEntities.NetworkCatchmentStagings.ToList().Select(x =>
                new NetworkCatchment(x.DrainID, x.Watershed, x.CatchmentGeometry, x.OCSurveyCatchmentID)
                {
                    CatchmentGeometry4326 =
                        CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(x.CatchmentGeometry),
                        OCSurveyDownstreamCatchmentID = x.OCSurveyDownstreamCatchmentID != 0 ? x.OCSurveyDownstreamCatchmentID : null
                }).ToList();
            existingNetworkCatchments.Merge(newNetworkCatchments,
                HttpRequestStorage.DatabaseEntities.NetworkCatchments.Local,
                (x, y) => x.OCSurveyCatchmentID == y.OCSurveyCatchmentID && x.Watershed == y.Watershed,
                (x, y) =>
                {
                    x.CatchmentGeometry = y.CatchmentGeometry;
                    x.CatchmentGeometry4326 = y.CatchmentGeometry4326;
                    x.OCSurveyDownstreamCatchmentID = y.OCSurveyDownstreamCatchmentID;
                    x.DrainID = y.DrainID;
                });



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