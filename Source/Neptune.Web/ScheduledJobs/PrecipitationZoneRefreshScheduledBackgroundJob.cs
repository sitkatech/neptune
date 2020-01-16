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
    public class PrecipitationZoneRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public PrecipitationZoneRefreshScheduledBackgroundJob(int currentPersonPersonID)
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
            dbContext.PrecipitationZoneStagings.DeletePrecipitationZoneStaging(dbContext.PrecipitationZoneStagings.ToList());
            dbContext.SaveChanges(person);
            
            var featureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfPrecipitationZoneKeyNotUnique(featureCollection);
            StageFeatureCollection(featureCollection);
            MergeAndUpdateTreatmentBMPProperties(dbContext, person);
        }

        private static void MergeAndUpdateTreatmentBMPProperties(DatabaseEntities dbContext, Person person)
        {
            // MergeListHelper is doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pPrecipitationZoneUpdateFromStaging");
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pTreatmentBMPUpdatePrecipitationZone");
        }

        private static void StageFeatureCollection(FeatureCollection newPrecipitationZoneFeatureCollection)
        {
            var jsonFeatureCollection = JsonConvert.SerializeObject(newPrecipitationZoneFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "PrecipitationZoneStaging",
                "ID as PrecipitationZoneKey, RainfallZo as DesignStormwaterDepthInInches",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfPrecipitationZoneKeyNotUnique(FeatureCollection featureCollection)
        {
            var precipitationZoneKeysThatAreNotUnique = featureCollection.Features
                .GroupBy(x => x.Properties["ID"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (precipitationZoneKeysThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The Precipitation Zone service returned an invalid collection. The following IDs are duplicated:\n{string.Join(", ", precipitationZoneKeysThatAreNotUnique)}");
            }
        }

        private static FeatureCollection RetrieveFeatureCollectionFromArcServer()
        {
            var collectedFeatureCollection = new FeatureCollection();
            using (var client = new HttpClient())
            {
                var resultOffset = 0;
                var baseRequestUri = NeptuneWebConfiguration.PrecipitationZoneServiceUrl;
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
                            $"The Precipitation Zone service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            $"The Precipitation Zone service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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