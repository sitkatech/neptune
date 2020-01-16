using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class LSPCBasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public LSPCBasinRefreshScheduledBackgroundJob(int currentPersonPersonID)
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
            dbContext.LSPCBasinStagings.DeleteLSPCBasinStaging(dbContext.LSPCBasinStagings.ToList());
            dbContext.SaveChanges(person);
            
            var featureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfLSPCBasinKeyNotUnique(featureCollection);
            StageFeatureCollection(featureCollection);
            MergeAndUpdateTreatmentBMPProperties(dbContext, person);
        }

        private static void MergeAndUpdateTreatmentBMPProperties(DatabaseEntities dbContext, Person person)
        {
            // MergeListHelper is doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pLSPCBasinUpdateFromStaging");
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pTreatmentBMPUpdateLSPCBasin");
        }

        private static void StageFeatureCollection(FeatureCollection newLSPCBasinFeatureCollection)
        {
            var jsonFeatureCollection = JsonConvert.SerializeObject(newLSPCBasinFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "LSPCBasinStaging",
                "LSPC_BASIN as LSPCBasinKey, Name as LSPCBasinName",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfLSPCBasinKeyNotUnique(FeatureCollection featureCollection)
        {
            var lspcBasinKeysThatAreNotUnique = featureCollection.Features
                .GroupBy(x => x.Properties["LSPC_BASIN"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (lspcBasinKeysThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The LSPC Basin service returned an invalid collection. The following LSPC Basin IDs are duplicated:\n{string.Join(", ", lspcBasinKeysThatAreNotUnique)}");
            }
        }

        private static FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneWebConfiguration.LSPCBasinServiceUrl;
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
                            $"The LSPC Basin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            $"The LSPC Basin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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