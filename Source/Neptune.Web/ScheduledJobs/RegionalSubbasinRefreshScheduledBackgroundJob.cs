using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Neptune.Web.ScheduledJobs
{
    class RegionalSubbasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public RegionalSubbasinRefreshScheduledBackgroundJob(int currentPersonPersonID, bool queueLGURefresh)
        {
            PersonID = currentPersonPersonID;
            QueueLGURefresh = queueLGURefresh;
        }

        public bool QueueLGURefresh { get; set; }

        public int PersonID { get; set; }

        // only runs in prod to avoid hitting OC Survey with multiple concurrent identical requests
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa,
            NeptuneEnvironmentType.Local
        };

        public static void RunRefresh(DatabaseEntities dbContext, Person person, bool queueLguRefresh)
        {
            dbContext.RegionalSubbasinStagings.DeleteRegionalSubbasinStaging(dbContext.RegionalSubbasinStagings.ToList());
            dbContext.SaveChanges(person);

            var newRegionalSubbasinFeatureCollection = RetrieveFeatureCollectionFromArcServer();
            ThrowIfCatchIdnNotUnique(newRegionalSubbasinFeatureCollection);
            StageFeatureCollection(newRegionalSubbasinFeatureCollection);
            ThrowIfDownstreamInvalid(dbContext);
            DeleteLoadGeneratingUnits(dbContext);
            MergeAndReproject(dbContext, person);
            RefreshCentralizedDelineations(dbContext, person);

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunDelineationDiscrepancyCheckerJob());

            if (queueLguRefresh)
            {
                UpdateLoadGeneratingUnits();
            }
        }

        private static void DeleteLoadGeneratingUnits(DatabaseEntities dbContext)
        {
            dbContext.Database.ExecuteSqlCommand(
                "EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
        }

        private static void UpdateLoadGeneratingUnits()
        {
            // Instead, just queue a total LGU update
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(null));

            // And follow it up with an HRU update
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunHRURefreshJob());

        }


        private static void RefreshCentralizedDelineations(DatabaseEntities dbContext, Person person)
        {
            foreach (var delineation in dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Centralized.DelineationTypeID))
            {
                var centralizedDelineationGeometry2771 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry2771(dbContext);
                var centralizedDelineationGeometry4326 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry4326(dbContext);

                delineation.DelineationGeometry = centralizedDelineationGeometry2771;
                delineation.DelineationGeometry4326 = centralizedDelineationGeometry4326;

                delineation.DateLastModified = DateTime.Now;
            }

            dbContext.SaveChanges(person);
        }

        private static void MergeAndReproject(DatabaseEntities dbContext, Person person)
        {
            // MergeListHelper doesn't handle same-table foreign keys well, so we use a stored proc to run the merge
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");

            // unfortunately, now we have to create the catchment geometries in 4326, since SQL isn't capable of doing this.
            dbContext.RegionalSubbasins.Load();
            dbContext.Watersheds.Load();
            foreach (var regionalSubbasin in dbContext.RegionalSubbasins)
            {
                regionalSubbasin.CatchmentGeometry4326 = CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(regionalSubbasin.CatchmentGeometry);
            }

            // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
            foreach (var watershed in dbContext.Watersheds)
            {
                watershed.WatershedGeometry4326 = CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(watershed.WatershedGeometry);
            }
            dbContext.SaveChanges(person);
            
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pTreatmentBMPUpdateWatershed");
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        }

        private static void ThrowIfDownstreamInvalid(DatabaseEntities dbContext)
        {
            // this is done against the staged feature collection because it's easier to implement in LINQ than against the raw JSON response
            var ocSurveyCatchmentIDs = dbContext.RegionalSubbasinStagings.Select(x => x.OCSurveyCatchmentID).ToList();

            dbContext.Database.CommandTimeout = 30000;
            var stagedRegionalSubbasinsWithBrokenDownstreamRel = dbContext.RegionalSubbasinStagings.Where(x =>
                    x.OCSurveyDownstreamCatchmentID != 0 && !ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID))
                .ToList();

            if (stagedRegionalSubbasinsWithBrokenDownstreamRel.Any())
            {
                throw new RemoteServiceException(
                    $"The Regional Subbasin service returned an invalid collection. The catchments with the following IDs have invalid downstream catchment IDs:\n{string.Join(", ", stagedRegionalSubbasinsWithBrokenDownstreamRel.Select(x => x.OCSurveyCatchmentID))}");
            }
        }

        private static void StageFeatureCollection(FeatureCollection newRegionalSubbasinFeatureCollection)
        {
            var jsonFeatureCollection = JsonConvert.SerializeObject(newRegionalSubbasinFeatureCollection);

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID, 600000);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(jsonFeatureCollection,
                NeptuneWebConfiguration.DatabaseConnectionString, "RegionalSubbasinStaging",
                "CatchIDN as OCSurveyCatchmentID, DwnCatchIDN as OCSurveyDownstreamCatchmentID, DrainID as DrainID, Watershed as Watershed",
                // transform from 2230 to 2771 here to avoid precision errors introduced by asking arc to do it
                CoordinateSystemHelper.NAD_83_CA_ZONE_VI_SRID, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        private static void ThrowIfCatchIdnNotUnique(FeatureCollection newRegionalSubbasinFeatureCollection)
        {
            var catchIdnsThatAreNotUnique = newRegionalSubbasinFeatureCollection.Features
                .GroupBy(x => x.Properties["CatchIDN"]).Where(x => x.Count() > 1).Select(x => int.Parse(x.Key.ToString()))
                .ToList();

            if (catchIdnsThatAreNotUnique.Any())
            {
                throw new RemoteServiceException(
                    $"The Regional Subbasin service returned an invalid collection. The following Catchment IDs are duplicated:\n{string.Join(", ", catchIdnsThatAreNotUnique)}");
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
                        outSR = 2230,
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
                            "The Regional Subbasin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
                            "The Regional Subbasin service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
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
            RunRefresh(DbContext, person, QueueLGURefresh);
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