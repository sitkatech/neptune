﻿using System;
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

            var catchIdnsThatAreNotUniqueAndAreThereforeBad = collectedFeatureCollection.Features
                .GroupBy(x => x.Properties["CatchIDN"]).Where(x => x.Count() > 1).Select(x=> int.Parse(x.Key.ToString())).ToList();

            if (catchIdnsThatAreNotUniqueAndAreThereforeBad.Any())
            {
                throw new RemoteServiceException(
                    $"The Network Catchment service returned an invalid collection. The following Catchment IDs are duplicated:\n{string.Join(", ", catchIdnsThatAreNotUniqueAndAreThereforeBad)}");
            }

            var jsonFeatureCollection = JsonConvert.SerializeObject(collectedFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "NetworkCatchmentStaging",
                "CatchIDN as OCSurveyCatchmentID, DwnCatchIDN as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

            // merge the things
            
            var ocSurveyCatchmentIDs = dbContext.NetworkCatchmentStagings.Select(x=>x.OCSurveyCatchmentID).ToList();

            var stagedNetworkCatchmentsWithBrokenDownstreamRel = dbContext.NetworkCatchmentStagings.Where(x => x.OCSurveyDownstreamCatchmentID != 0 && !ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID)).ToList();

            if (stagedNetworkCatchmentsWithBrokenDownstreamRel.Any())
            {
                throw new RemoteServiceException(
                    $"The Network Catchment service returned an invalid collection. The catchments with the following IDs have invalid downstream catchment IDs:\n{string.Join(", ", stagedNetworkCatchmentsWithBrokenDownstreamRel.Select(x => x.OCSurveyCatchmentID))}");
            }
            
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



            return jsonFeatureCollection;
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