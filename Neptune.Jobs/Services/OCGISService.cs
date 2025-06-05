using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.Common.JsonConverters;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Hangfire;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;

namespace Neptune.Jobs.Services;

public class OCGISService(
    HttpClient httpClient,
    ILogger<OCGISService> logger,
    NeptuneDbContext dbContext,
    IOptions<NeptuneJobConfiguration> neptuneJobConfiguration,
    SitkaSmtpClientService sitkaSmtpClientService)
    : BaseAPIService<OCGISService>(httpClient, logger, "OCGIS Service")
{
    private const int MAX_RETRIES = 3;
    private const string HRUServiceEndPoint = "Environmental_Resources/HRUSummary/GPServer/HRUSummary";
    private const string ParcelFileName = "ParcelsFromOC.json";


    protected readonly BlobContainerClient BlobContainerClient = new BlobServiceClient(neptuneJobConfiguration.Value.AzureBlobStorageConnectionString).GetBlobContainerClient("file-resource");

    /*
    AutoDelineateServiceUrl=        https://ocgis.com/arcpub/rest/services/Flood/GlobalStormwaterDelineation/GPServer/Global%20Stormwater%20Delineation/
    HRUServiceBaseUrl=              https://ocgis.com/arcpub/rest/services/Environmental_Resources/HRUSummary/GPServer/HRUSummary
    RegionalSubbasinServiceUrl=     https://ocgis.com/arcpub/rest/services/Environmental_Resources/RegionalSubbasins/MapServer/0/query
    ModelBasinServiceUrl=           https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/7/query
    PrecipitationZoneServiceUrl=    https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query
    OCTAPrioritizationServiceUrl=   https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query
    ParcelServiceUrl = https://ocgis.com/arcpub/rest/services/LegalLotsAttributeOpenData/FeatureServer/0/query?outFields=*&where=1%3D1&f=geojson
     */

    public async Task RefreshSubbasins()
    {
        dbContext.Database.SetCommandTimeout(30000);
        await dbContext.RegionalSubbasinStagings.ExecuteDeleteAsync();
        var regionalSubbasinFromEsris = await RetrieveRegionalSubbasins();
        var regionalSubbasinStagings = regionalSubbasinFromEsris.Select(x =>
            {
                var catchmentGeometry = x.Geometry;
                catchmentGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
                return new RegionalSubbasinStaging()
                {
                    CatchmentGeometry = catchmentGeometry,
                    Watershed = x.Watershed,
                    OCSurveyCatchmentID = x.OCSurveyCatchmentID,
                    OCSurveyDownstreamCatchmentID = x.OCSurveyDownstreamCatchmentID,
                    DrainID = x.DrainID
                };
            })
            .ToList();
        dbContext.RegionalSubbasinStagings.AddRange(regionalSubbasinStagings);
        await dbContext.SaveChangesAsync();
        //We need to delete everything that has a foreign key relationship to RSBs prior to the update for the merge delete to not fail
        //This is NOT everything, but waiting until we run up against an actual instance of needing to delete the remaining entities before enforcing (ie.ProjectNereidResults)
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteNereidResults");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");
        
        // reproject to 4326
        foreach (var regionalSubbasin in dbContext.RegionalSubbasins)
        {
            regionalSubbasin.CatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry.ProjectTo4326();
        }

        // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
        foreach (var watershed in dbContext.Watersheds)
        {
            watershed.WatershedGeometry4326 = watershed.WatershedGeometry.ProjectTo4326();
        }
        await dbContext.SaveChangesAsync();
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pWatershedMakeValid");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateWatershed");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
    }

    public async Task RefreshPrecipitationZones()
    {
        dbContext.Database.SetCommandTimeout(30000);
        await dbContext.PrecipitationZoneStagings.ExecuteDeleteAsync();
        var precipitationZoneFromEsris = await RetrievePrecipitationZones();
        var precipitationZoneStagings = precipitationZoneFromEsris.Select(x =>
            {
                var precipitationZoneGeometry = x.Geometry;
                precipitationZoneGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
                return new PrecipitationZoneStaging()
                {
                    PrecipitationZoneGeometry = precipitationZoneGeometry,
                    PrecipitationZoneKey = x.PrecipitationZoneKey,
                    DesignStormwaterDepthInInches = x.DesignStormwaterDepthInInches
                };
            })
            .ToList();
        dbContext.PrecipitationZoneStagings.AddRange(precipitationZoneStagings);
        await dbContext.SaveChangesAsync();
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pPrecipitationZoneUpdateFromStaging");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdatePrecipitationZone");
    }

    public async Task RefreshModelBasins()
    {
        dbContext.Database.SetCommandTimeout(30000);
        await dbContext.ModelBasinStagings.ExecuteDeleteAsync();
        var parcelFromEsris = await RetrieveModelBasins();
        var parcelStagings = parcelFromEsris.Select(x =>
        {
            var parcelGeometry = x.Geometry;
            parcelGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
            return new ModelBasinStaging()
            {
                ModelBasinGeometry = parcelGeometry,
                ModelBasinKey = x.ModelBasinKey,
                ModelBasinState = x.ModelBasinState,
                ModelBasinRegion = x.ModelBasinRegion
            };
        }).ToList();
        dbContext.ModelBasinStagings.AddRange(parcelStagings);
        await dbContext.SaveChangesAsync();
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pModelBasinUpdateFromStaging");
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateModelBasin");
    }

    public async Task RefreshOCTAPrioritizations()
    {
        dbContext.Database.SetCommandTimeout(30000);
        await dbContext.OCTAPrioritizationStagings.ExecuteDeleteAsync();
        var octaPrioritizationFromEsris = await RetrieveOCTAPrioritizations();
        var octaPrioritizationStagings = octaPrioritizationFromEsris.Select(x =>
        {
            var octaPrioritizationGeometry = x.Geometry;
            octaPrioritizationGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
            return new OCTAPrioritizationStaging()
            {
                OCTAPrioritizationGeometry = octaPrioritizationGeometry,
                OCTAPrioritizationKey = x.OCTAPrioritizationKey,
                Watershed = x.Watershed,
                CatchIDN = x.CatchIDN,
                TPI = x.TPI,
                WQNLU = x.WQNLU,
                WQNMON = x.WQNMON,
                IMPAIR = x.IMPAIR,
                MON = x.MON,
                SEA = x.SEA,
                SEA_PCTL = x.SEA_PCTL,
                PC_VOL_PCT = x.PC_VOL_PCT,
                PC_NUT_PCT = x.PC_NUT_PCT,
                PC_BAC_PCT = x.PC_BAC_PCT,
                PC_MET_PCT = x.PC_MET_PCT,
                PC_TSS_PCT = x.PC_TSS_PCT
            };
        }).ToList();
        dbContext.OCTAPrioritizationStagings.AddRange(octaPrioritizationStagings);
        await dbContext.SaveChangesAsync();
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pOCTAPrioritizationUpdateFromStaging");
        foreach (var octaPrioritization in dbContext.OCTAPrioritizations)
        {
            octaPrioritization.OCTAPrioritizationGeometry4326 = octaPrioritization.OCTAPrioritizationGeometry.ProjectTo4326();
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task RefreshParcels()
    {
        dbContext.Database.SetCommandTimeout(30000);
        await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pParcelStagingDelete");

        var parcelFromEsris = await RetrieveParcels();
        var parcelStagings = parcelFromEsris.Select(x =>
        {
            var parcelGeometry = x.Geometry;
            parcelGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
            return new ParcelStaging()
            {
                Geometry = parcelGeometry,
                ParcelNumber = x.ParcelNumber,
                ParcelAreaInSquareFeet = x.ParcelAreaInSquareFeet,
                ParcelAddress = x.ParcelAddress,
                ParcelCityState = x.ParcelCityState,
                ParcelZipCode = x.ParcelZipCode

            };
        }).ToList();
        dbContext.ParcelStagings.AddRange(parcelStagings);
        await dbContext.SaveChangesAsync();

        if (parcelStagings.Count > 0)
        {
            // first wipe the dependent WQMPParcel table, then wipe the old parcels
            await dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.pParcelUpdateFromStaging");

            // we need to get the 4326 representation of the geometry; unfortunately can't do it in sql
            var parcels = dbContext.ParcelGeometries.ToList();
            parcels.AsParallel().ForAll(x => x.Geometry4326 = x.GeometryNative.ProjectTo4326());
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<ParcelFromEsri>> RetrieveParcels()
    {
        //https://ocgis.com/arcpub/rest/services/LegalLotsAttributeOpenData/FeatureServer/0/query?where=AssessmentNo+is+not+null&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=LegalLotID%2CAssessmentNo%2CSiteAddress%2CShape__Area&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&timeReferenceUnknownClient=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=geojson
        const string serviceName = "Parcel";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("LegalLotsAttributeOpenData/FeatureServer/0/query", "AssessmentNo is not null", "LegalLotID,AssessmentNo,SiteAddress,SiteCityState,SiteZip5,Shape__Area");
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<ParcelFromEsri>(featureCollection);
        await SerializeAndUploadToBlobStorage(featureCollection, ParcelFileName);
        //ThrowIfNotUnique(result.GroupBy(x => x.ParcelKey), serviceName);
        return result;
    }

    public async Task<List<OCTAPrioritizationFromEsri>> RetrieveOCTAPrioritizations()
    {
        const string serviceName = "OCTA Prioritization";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query");
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<OCTAPrioritizationFromEsri>(featureCollection, GeoJsonSerializer.CreateGeoJSONSerializerOptions(1));
        ThrowIfNotUnique(result.GroupBy(x => x.OCTAPrioritizationKey), serviceName);
        return result;
    }

    public async Task<List<ModelBasinFromEsri>> RetrieveModelBasins()
    {
        const string serviceName = "Model Basin";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/7/query");
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<ModelBasinFromEsri>(featureCollection);
        ThrowIfNotUnique(result.GroupBy(x => x.ModelBasinKey), serviceName);
        return result;
    }

    public async Task<List<RegionalSubbasinFromEsri>> RetrieveRegionalSubbasins()
    {
        const string serviceName = "Regional Subbasin";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/RegionalSubbasins/MapServer/0/query");
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<RegionalSubbasinFromEsri>(featureCollection);
        ThrowIfNotUnique(result.GroupBy(x => x.OCSurveyCatchmentID), serviceName);

        // check if downstream is invalid
        var ocSurveyCatchmentIDs = result.Select(x => x.OCSurveyCatchmentID).ToList();
        var stagedRegionalSubbasinsWithBrokenDownstreamRel = result.Where(x =>
                x.OCSurveyDownstreamCatchmentID.HasValue &&
                x.OCSurveyDownstreamCatchmentID != 0 &&
                !ocSurveyCatchmentIDs.Contains(x.OCSurveyDownstreamCatchmentID.Value))
            .ToList();

        if (stagedRegionalSubbasinsWithBrokenDownstreamRel.Any())
        {
            throw new RemoteServiceException(
                $"The {serviceName} service returned an invalid collection. The catchments with the following IDs have invalid downstream catchment IDs:\n{string.Join(", ", stagedRegionalSubbasinsWithBrokenDownstreamRel.Select(x => x.OCSurveyCatchmentID))}");
        }

        return result;
    }

    public async Task<List<PrecipitationZoneFromEsri>> RetrievePrecipitationZones() 
    {
        const string serviceName = "Precipitation Zone";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query");
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<PrecipitationZoneFromEsri>(featureCollection, GeoJsonSerializer.CreateGeoJSONSerializerOptions(2));
        ThrowIfNotUnique(result.GroupBy(x => x.PrecipitationZoneKey), serviceName);
        return result;
    }


    public async Task<HRUResponseResult> RetrieveHRUResponseFeatures(List<HRURequestFeature> hruRequestFeatures, NeptuneDbContext dbContext)
    {
        var hruResponseResult = new HRUResponseResult();
        var rawResponse = string.Empty;
        try
        {
            try
            {
                var esriAsyncJobOutputParameter =
                    await SubmitHRURequestJobAndRetrieveResults(hruRequestFeatures, dbContext);
                var esriGPRecordSetLayer = esriAsyncJobOutputParameter.Value;
                hruResponseResult.HRUResponseFeatures = [.. esriGPRecordSetLayer.Features.Where(x => x.Attributes.ImperviousAcres != null)];
                hruResponseResult.HRULogID = esriAsyncJobOutputParameter.HRULogID;
            }
            catch (EsriAsynchronousJobException ex)
            {
                hruResponseResult.HRULogID = ex.HRULogID;
                throw new Exception(ex.Message);
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex.Message, ex);
            Logger.LogWarning($"Skipped entities (ProjectLGUs if Project modeling, otherwise LGUs) with these IDs: {string.Join(", ", hruRequestFeatures.Select(x => x.Attributes.QueryFeatureID.ToString()))}");
            Logger.LogWarning(rawResponse);
        }

        return hruResponseResult;
    }

    private async Task<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>
        SubmitHRURequestJobAndRetrieveResults(List<HRURequestFeature> featuresForHRURequest, NeptuneDbContext dbContext)
    {
        var requestObject = GetGPRecordSetLayer(featuresForHRURequest);
        var keyValuePairs = new List<KeyValuePair<string, string>>
        {
            new("input_fc", GeoJsonSerializer.Serialize(requestObject)),
            new("returnZ", "false"),
            new("returnM", "false"),
            new("returnTrueCurves", "false"),
            new("f", "json"),
            new("env:outSR", ""),
            new("env:processSR", ""),
            new("context", ""),
        };

        var jobID = "";
        var retry = true;
        var attempts = 0;
        EsriJobStatusResponse? jobStatusResponse = null;
        string requestUri = $"{HRUServiceEndPoint}/submitJob";
        while (retry && attempts < MAX_RETRIES)
        {
            var httpResponseMessage = await HttpClient.PostAsync(requestUri, new FormUrlEncodedContent(keyValuePairs));
            httpResponseMessage.EnsureSuccessStatusCode();

            jobStatusResponse = await GeoJsonSerializer.DeserializeAsync<EsriJobStatusResponse>(await httpResponseMessage.Content.ReadAsStreamAsync());
            jobID = jobStatusResponse.jobId;
            // wait 5 seconds before checking for process on first attempt, 30 on second, and 90 on third
            var timeout = attempts switch
            {
                0 => 5000,
                1 => 30000,
                2 => 90000,
                _ => 5000
            };

            retry = await CheckShouldRetry(jobID, timeout);
            attempts++;
        }

        if (retry && attempts >= MAX_RETRIES)
        {
            throw new TimeoutException("Remote service failed to respond within the timeout.");
        }

        var isExecuting = jobStatusResponse.IsExecuting();

        while (isExecuting)
        {
            jobStatusResponse = await CheckEsriJobStatus(jobID, 5000);
            isExecuting = jobStatusResponse.IsExecuting();
        }

        //Maybe this is overkill, but I don't want to double serialize the requestObject, since input_fc has it stored as a serialized object
        //So make a new list that will allow it to sit as an object, then serialize the whole list
        var logKeyValuePairs = new List<KeyValuePair<string, object>> { new("input_fc", requestObject) };
        keyValuePairs.RemoveAt(0);
        foreach (var keyValuePair in keyValuePairs)
        {
            KeyValuePair<string, object> pair = new(keyValuePair.Key, Convert.ChangeType(keyValuePair.Value, typeof(object)));
            logKeyValuePairs.Add(pair);
        }

        var responseURI = $"{HttpClient.BaseAddress}{HRUServiceEndPoint}/jobs/{jobID}/?f=json";
        var hruLog = new HRULog()
        {
            RequestDate = DateTime.UtcNow,
            Success = jobStatusResponse.jobStatus == EsriJobStatus.esriJobSucceeded,
            HRURequest = GeoJsonSerializer.Serialize(new {
                    RequestURI = $"{HttpClient.BaseAddress}{requestUri}",
                    RequestBody = logKeyValuePairs
                }),
            HRUResponse = GeoJsonSerializer.Serialize(new
            {
                ResponseURI = responseURI,
                ResponseContent = jobStatusResponse
            })
        };
        await dbContext.AddAsync(hruLog);
        await dbContext.SaveChangesAsync();
        var hruLogID = hruLog.HRULogID;

        switch (jobStatusResponse.jobStatus)
        {
            case EsriJobStatus.esriJobSucceeded:
                var resultURI = $"{HRUServiceEndPoint}/jobs/{jobID}/results/output_table/?f=json";
                var resultContent = await HttpClient.GetFromJsonAsync<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>(resultURI, GeoJsonSerializer.DefaultSerializerOptions);
                resultContent.HRULogID = hruLogID;

                //If we're successful, provide the final response in the log as well.
                jobStatusResponse.jobResult = resultContent;
                jobStatusResponse.jobResult.ResultURI = $"{HttpClient.BaseAddress}{resultURI}";
                hruLog.HRUResponse = GeoJsonSerializer.Serialize(new
                {
                    ResponseURI = responseURI,
                    ResponseContent = jobStatusResponse
                });
                await dbContext.SaveChangesAsync();
                
                return resultContent;
            case EsriJobStatus.esriJobCancelling:
            case EsriJobStatus.esriJobCancelled:
                throw new EsriAsynchronousJobCancelledException(jobStatusResponse.jobId, hruLogID);
            case EsriJobStatus.esriJobFailed:
                throw new EsriAsynchronousJobFailedException(jobStatusResponse, requestObject.ToString(), hruLogID);
            default:
                throw new EsriAsynchronousJobUnrecognizedStatusException(jobStatusResponse, hruLogID);
        }
    }

    private async Task<bool> CheckShouldRetry(string jobID, int millisecondsTimeout)
    {
        var jobStatusResponse = await CheckEsriJobStatus(jobID, millisecondsTimeout);

        // if we don't have any messages on the status response,
        // this request ended up in a bad state and we should just abandon it and try again.
        // A little rude of us to send two requests for the same data, but the server should
        // respond correctly the first time if it doesn't want us to keep asking.
        return jobStatusResponse.messages.Count == 0;
    }

    private async Task<EsriJobStatusResponse?> CheckEsriJobStatus(string jobID, int millisecondsTimeout)
    {
        Thread.Sleep(millisecondsTimeout);
        var jobStatusResponse = await HttpClient.GetFromJsonAsync<EsriJobStatusResponse>($"{HRUServiceEndPoint}/jobs/{jobID}/?f=json");
        return jobStatusResponse;
    }


    private static EsriGPRecordSetLayer<HRURequestFeature> GetGPRecordSetLayer(
        List<HRURequestFeature> features)
    {
        return new EsriGPRecordSetLayer<HRURequestFeature>
        {
            Features = features,
            DisplayFieldName = "",
            GeometryType = "esriGeometryPolygon",
            ExceededTransferLimit = false,
            SpatialReference = new EsriSpatialReference { wkid = 102646, latestWkid = 2230 },
            Fields = new List<EsriField>
                {
                    new()
                    {
                        Name = "OBJECTID",
                        Type = "esriFieldTypeOID",
                        Alias = "OBJECTID"

                    },

                    new()
                    {
                        Name = "QueryFeatureID",
                        Type = "esriFieldTypeString",
                        Alias = "QueryFeatureID",
                        Length = 255
                    },

                    new()
                    {
                        Name = "Shape_Length",
                        Type = "esriFieldTypeDouble",
                        Alias = "Shape_Length"
                    },

                    new()
                    {
                        Name = "Shape_Area",
                        Type = "esriFieldTypeDouble",
                        Alias = "Shape_Area"
                    }
                }
        };
    }

    private static void ThrowIfNotUnique<T>(IEnumerable<IGrouping<int, T>> groupBy, string serviceName)
    {
        var keysThatAreNotUnique = groupBy.Where(x => x.Count() > 1)
            .Select(x => int.Parse(x.Key.ToString()))
            .ToList();

        if (keysThatAreNotUnique.Any())
        {
            throw new RemoteServiceException(
                $"The {serviceName} service returned an invalid collection. The following IDs are duplicated:\n{string.Join(", ", keysThatAreNotUnique)}");
        }
    }

    private async Task<FeatureCollection> RetrieveFeatureCollectionFromArcServer(string url)
    {
        return await RetrieveFeatureCollectionFromArcServer(url, "1=1", "*");
    }

    private async Task<FeatureCollection> RetrieveFeatureCollectionFromArcServer(string url, string filterCriteria, string outFields)
    {
        var collectedFeatureCollection = new FeatureCollection();
        var resultOffset = 0;
        var done = false;

        while (!done)
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>
            {
                new("where", filterCriteria),
                new("geometryType", "esriGeometryEnvelope"),
                new("spatialRel", "esriSpatialRelIntersects"),
                new("outFields", outFields),
                new("outSR", "2771"),
                new("returnGeometry", "true"),
                new("returnTrueCurves", "false"),
                new("returnIdsOnly", "false"),
                new("returnCountOnly", "false"),
                new("returnZ", "false"),
                new("returnM", "false"),
                new("returnDistinctValues", "false"),
                new("returnExtentOnly", "false"),
                new("f", "geojson"),
                new("resultOffset", resultOffset.ToString()),
                new("resultRecordCount", "1000")
            };

            HttpResponseMessage response;
            try
            {
                response = await PostFormContent(url, keyValuePairs);
            }
            catch (TaskCanceledException tce)
            {
                throw new RemoteServiceException(
                    "The service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                    tce);
            }

            resultOffset += 1000;

            var featureCollection = await GeoJsonSerializer.DeserializeAsync<FeatureCollection>(await response.Content.ReadAsStreamAsync());
            if (featureCollection.Count == 0)
            {
                done = true;
            }
            else
            {
                foreach (var feature in featureCollection)
                {
                    collectedFeatureCollection.Add(feature);
                }
            }
        }
        return collectedFeatureCollection;
    }

    public async Task SerializeAndUploadToBlobStorage(IEnumerable list, string blobName)
    {
        var blobClient = BlobContainerClient.GetBlobClient(blobName);

        try
        {
            var stream = new MemoryStream();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                WriteIndented = false,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = null,
            };
            jsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory());
            jsonSerializerOptions.Converters.Add(new DateTimeConverter());
            jsonSerializerOptions.Converters.Add(new DoubleConverter(4));
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            await GeoJsonSerializer.SerializeToStream(list, jsonSerializerOptions, stream);
            stream.Position = 0;
            await blobClient.UploadAsync(stream, overwrite: true);
        }
        catch (Exception ex)
        {
            Logger.LogError("Failed to write to blob storage; Exception details: " + ex.Message);
        }
    }


    public class OCTAPrioritizationFromEsri : IHasGeometry
    {
        public Geometry Geometry { get; set; }
        [JsonPropertyName("ix")]
        public int OCTAPrioritizationKey { get; set; }
        public string? Watershed { get; set; }
        [JsonPropertyName("CID")]
        public string? CatchIDN { get; set; }
        public double TPI { get; set; }
        public double WQNLU { get; set; }
        public double WQNMON { get; set; }
        public double IMPAIR { get; set; }
        public double MON { get; set; }
        public double SEA { get; set; }
        public string? SEA_PCTL { get; set; }
        public double PC_VOL_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
    }

    public class RegionalSubbasinFromEsri : IHasGeometry
    {
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public Geometry Geometry { get; set; }
        [JsonPropertyName("CatchIDN")]
        public int OCSurveyCatchmentID { get; set; }
        [JsonPropertyName("DwnCatchIDN")]
        public int? OCSurveyDownstreamCatchmentID { get; set; }
    }

    public class ModelBasinFromEsri : IHasGeometry
    {
        public Geometry Geometry { get; set; }
        [JsonPropertyName("MODEL_BASIN")]
        public int ModelBasinKey { get; set; }
        [JsonPropertyName("STATE")]
        public string ModelBasinState { get; set; }
        [JsonPropertyName("REGION")]
        public string ModelBasinRegion { get; set; }
    }

    public class ParcelFromEsri : IHasGeometry
    {
        public Geometry Geometry { get; set; }
        [JsonPropertyName("LegalLotID")]
        public int ParcelKey { get; set; }
        [JsonPropertyName("AssessmentNo")]
        public string ParcelNumber { get; set; }
        [JsonPropertyName("SiteAddress")]
        public string ParcelAddress { get; set; }
        [JsonPropertyName("Shape__Area")]
        public double ParcelAreaInSquareFeet { get; set; }
        [JsonPropertyName("SiteCityState")]
        public string ParcelCityState { get; set; }
        [JsonPropertyName("SiteZip5")]
        public string ParcelZipCode { get; set; }
    }

    public class PrecipitationZoneFromEsri : IHasGeometry
    {
        public Geometry Geometry { get; set; }
        [JsonPropertyName("ID")]
        public int PrecipitationZoneKey { get; set; }
        [JsonPropertyName("RainfallZo")]
        public double DesignStormwaterDepthInInches { get; set; }
    }
}