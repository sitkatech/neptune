using System;
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
        public NetworkCatchmentRefreshScheduledBackgroundJob(int currentPersonPersonID)
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

        public static string RunRefresh(DatabaseEntities dbContext, Person person)
        {
            dbContext.NetworkCatchmentStagings.DeleteNetworkCatchmentStaging(dbContext.NetworkCatchmentStagings.ToList());
            dbContext.SaveChanges(person);

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
                    try
                    {
                        done = !JsonConvert.DeserializeObject<EsriQueryResponse>(response).ExceededTransferLimit;
                    }
                    catch (JsonReaderException jre)
                    {
                        throw new RemoteServiceException(
                            $"The Network Catchment service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Please do not file a bug report for this error.",
                            jre);
                    }

                    var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(response);
                    collectedFeatureCollection.Features.AddRange(featureCollection.Features);
                }
            }

            //// build a new feature collection where Watershed,CatchID is unique
            //// shouldn't have to do this, but the data source is u n r e l i a b l e

            IEnumerable<Feature> featuresWhereCatchIdnIsUnique = collectedFeatureCollection.Features.GroupBy(x => x.Properties["CatchIDN"] ).Select(x => x.FirstOrDefault());

            var featureCollectionWhereCatchIdnIsUnique = new FeatureCollection(featuresWhereCatchIdnIsUnique.ToList());

            var mergedResponse = JsonConvert.SerializeObject(featureCollectionWhereCatchIdnIsUnique);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(mergedResponse,
                NeptuneWebConfiguration.DatabaseConnectionString, "NetworkCatchmentStaging",
                "CatchIDN as OCSurveyCatchmentID, DwnCatchIDN as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

            // merge the things


            var ocSurveyCatchmentIDs = dbContext.NetworkCatchmentStagings.Select(x=>x.OCSurveyCatchmentID).ToList();

            // null out the downstream catchments that don't exist
            // should't have to do this but the data source is u n r e l i a b l e

            foreach (var networkCatchmentStaging in dbContext.NetworkCatchmentStagings.Where(x => ! ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID)))
            {
                networkCatchmentStaging.OCSurveyDownstreamCatchmentID = null;
            }

            dbContext.SaveChanges(person);


            // MergeListHelper is too unsophisticated to handle same-table foreign keys, so we use a SQL Server merge instead, which actually works

            dbContext.Database.CommandTimeout = 300;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pUpdateNetworkCatchmentLiveFromStaging");
            
            // unfortunately, now we have to create the catchment geometries in 4326, since SQL isn't capable of doing this.
            dbContext.NetworkCatchments.Load();
            foreach (var networkCatchment in dbContext.NetworkCatchments)
            {
                networkCatchment.CatchmentGeometry4326 =
                    CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(
                        networkCatchment.CatchmentGeometry);
            }

            dbContext.SaveChanges(person);



            return mergedResponse;
        }

        protected override void RunJobImplementation()
        {
            var person = DbContext.People.Find(PersonID);
            RunRefresh(DbContext, person);
        }
    }

    public class RemoteServiceException : Exception
    {
        public RemoteServiceException(String message, Exception innerException) : base(message, innerException) { }
    }

    public class EsriQueryResponse
    {
        [JsonProperty("exceededTransferLimit")]
        public bool ExceededTransferLimit { get; set; }
    }
}