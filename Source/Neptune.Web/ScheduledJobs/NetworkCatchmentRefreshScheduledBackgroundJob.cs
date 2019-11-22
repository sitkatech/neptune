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
using System.Threading.Tasks;
using System.Web;
using ApprovalUtilities.Utilities;
using Microsoft.Ajax.Utilities;
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

        public static void RunRefresh(DatabaseEntities dbContext, Person person)
        {
            dbContext.NetworkCatchmentStagings.DeleteNetworkCatchmentStaging(dbContext.NetworkCatchmentStagings.ToList());
            dbContext.SaveChanges(person);
            
            var newNetworkCatchmentFeatureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfCatchIdnNotUnique(newNetworkCatchmentFeatureCollection);
            StageFeatureCollection(newNetworkCatchmentFeatureCollection);
            ThrowIfDownstreamInvalid(dbContext);
            MergeAndReproject(dbContext, person);
        }

        private static void MergeAndReproject(DatabaseEntities dbContext, Person person)
        {
            // MergeListHelper is doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.CommandTimeout = 30000;
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
        }

        private static void ThrowIfDownstreamInvalid(DatabaseEntities dbContext)
        {
            // this is done against the staged feature collection because it's easier to implement in LINQ than against the raw JSON response
            var ocSurveyCatchmentIDs = dbContext.NetworkCatchmentStagings.Select(x => x.OCSurveyCatchmentID).ToList();

            dbContext.Database.CommandTimeout = 30000;
            var stagedNetworkCatchmentsWithBrokenDownstreamRel = dbContext.NetworkCatchmentStagings.Where(x =>
                    x.OCSurveyDownstreamCatchmentID != 0 && !ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID))
                .ToList();

            if (stagedNetworkCatchmentsWithBrokenDownstreamRel.Any())
            {
                throw new RemoteServiceException(
                    $"The Network Catchment service returned an invalid collection. The catchments with the following IDs have invalid downstream catchment IDs:\n{string.Join(", ", stagedNetworkCatchmentsWithBrokenDownstreamRel.Select(x => x.OCSurveyCatchmentID))}");
            }
        }

        private static void StageFeatureCollection(FeatureCollection newNetworkCatchmentFeatureCollection)
        {
            var jsonFeatureCollection = JsonConvert.SerializeObject(newNetworkCatchmentFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "NetworkCatchmentStaging",
                "CatchIDN as OCSurveyCatchmentID, DwnCatchIDN as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfCatchIdnNotUnique(FeatureCollection newNetworkCatchmentFeatureCollection)
        {
            var catchIdnsThatAreNotUnique = newNetworkCatchmentFeatureCollection.Features
                .GroupBy(x => x.Properties["CatchIDN"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (catchIdnsThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The Network Catchment service returned an invalid collection. The following Catchment IDs are duplicated:\n{string.Join(", ", catchIdnsThatAreNotUnique)}");
            }
        }

        private static FeatureCollection RetrieveFeatureCollectionFromArcServer()
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
                    string response;
                    try
                    {
                        response = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
                    }
                    catch (TaskCanceledException tce)
                    {
                        throw new RemoteServiceException(
                            $"The Network Catchment service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            $"The Network Catchment service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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

    public class RemoteServiceException : Exception
    {
        public RemoteServiceException(String message) : base(message)
        {

        }

        public RemoteServiceException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class EsriQueryResponse
    {
        [JsonProperty("exceededTransferLimit")]
        public bool ExceededTransferLimit { get; set; }
    }
}