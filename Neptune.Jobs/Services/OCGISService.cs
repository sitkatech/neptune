using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.Jobs.Services;

public class OCGISService : BaseAPIService<OCGISService>
{
    private const int MAX_RETRIES = 3;
    private const string HRUServiceEndPoint = "Environmental_Resources/HRUSummary/GPServer/HRUSummary";

    private readonly NeptuneDbContext _dbContext;

    public OCGISService(HttpClient httpClient, ILogger<OCGISService> logger, NeptuneDbContext dbContext) : base(httpClient, logger, "OCGIS Service")
    {
        _dbContext = dbContext;
    }

    /*
    AutoDelineateServiceUrl=        https://ocgis.com/arcpub/rest/services/Flood/GlobalStormwaterDelineation/GPServer/Global%20Stormwater%20Delineation/
    HRUServiceBaseUrl=              https://ocgis.com/arcpub/rest/services/Environmental_Resources/HRUSummary/GPServer/HRUSummary
    RegionalSubbasinServiceUrl=     https://ocgis.com/arcpub/rest/services/Environmental_Resources/RegionalSubbasins/MapServer/0/query
    ModelBasinServiceUrl=           https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/7/query
    PrecipitationZoneServiceUrl=    https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query
    OCTAPrioritizationServiceUrl=   https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query
     */

    public async Task RefreshSubbasins()
    {
        _dbContext.Database.SetCommandTimeout(30000);
        await _dbContext.RegionalSubbasinStagings.ExecuteDeleteAsync();
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
        await _dbContext.RegionalSubbasinStagings.AddRangeAsync(regionalSubbasinStagings);
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");
        
        // reproject to 4326
        foreach (var regionalSubbasin in _dbContext.RegionalSubbasins)
        {
            regionalSubbasin.CatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry.ProjectTo4326();
        }

        // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
        foreach (var watershed in _dbContext.Watersheds)
        {
            watershed.WatershedGeometry4326 = watershed.WatershedGeometry.ProjectTo4326();
        }
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateWatershed");
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
    }

    public async Task RefreshPrecipitationZones()
    {
        _dbContext.Database.SetCommandTimeout(30000);
        await _dbContext.PrecipitationZoneStagings.ExecuteDeleteAsync();
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
        await _dbContext.PrecipitationZoneStagings.AddRangeAsync(precipitationZoneStagings);
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pPrecipitationZoneUpdateFromStaging");
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdatePrecipitationZone");
    }

    public async Task RefreshModelBasins()
    {
        _dbContext.Database.SetCommandTimeout(30000);
        await _dbContext.ModelBasinStagings.ExecuteDeleteAsync();
        var modelBasinFromEsris = await RetrieveModelBasins();
        var modelBasinStagings = modelBasinFromEsris.Select(x =>
        {
            var modelBasinGeometry = x.Geometry;
            modelBasinGeometry.SRID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;
            return new ModelBasinStaging()
            {
                ModelBasinGeometry = modelBasinGeometry,
                ModelBasinKey = x.ModelBasinKey,
                ModelBasinState = x.ModelBasinState,
                ModelBasinRegion = x.ModelBasinRegion
            };
        }).ToList();
        await _dbContext.ModelBasinStagings.AddRangeAsync(modelBasinStagings);
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pModelBasinUpdateFromStaging");
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateModelBasin");
    }

    public async Task RefreshOCTAPrioritizations()
    {
        _dbContext.Database.SetCommandTimeout(30000);
        await _dbContext.OCTAPrioritizationStagings.ExecuteDeleteAsync();
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
        await _dbContext.OCTAPrioritizationStagings.AddRangeAsync(octaPrioritizationStagings);
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pOCTAPrioritizationUpdateFromStaging");
        foreach (var octaPrioritization in _dbContext.OCTAPrioritizations)
        {
            octaPrioritization.OCTAPrioritizationGeometry4326 = octaPrioritization.OCTAPrioritizationGeometry.ProjectTo4326();
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OCTAPrioritizationFromEsri>> RetrieveOCTAPrioritizations()
    {
        const string serviceName = "OCTA Prioritization";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query", serviceName);
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<OCTAPrioritizationFromEsri>(featureCollection, GeoJsonSerializer.CreateGeoJSONSerializerOptions(1));
        ThrowIfNotUnique(result.GroupBy(x => x.OCTAPrioritizationKey), serviceName);
        return result;
    }

    public async Task<List<ModelBasinFromEsri>> RetrieveModelBasins()
    {
        const string serviceName = "Model Basin";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/7/query", serviceName);
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<ModelBasinFromEsri>(featureCollection);
        ThrowIfNotUnique(result.GroupBy(x => x.ModelBasinKey), serviceName);
        return result;
    }

    public async Task<List<RegionalSubbasinFromEsri>> RetrieveRegionalSubbasins()
    {
        const string serviceName = "Regional Subbasin";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/RegionalSubbasins/MapServer/0/query", serviceName);
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
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query", serviceName);
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<PrecipitationZoneFromEsri>(featureCollection, GeoJsonSerializer.CreateGeoJSONSerializerOptions(2));
        ThrowIfNotUnique(result.GroupBy(x => x.PrecipitationZoneKey), serviceName);
        return result;
    }


    public async Task<List<HRUResponseFeature>> RetrieveHRUResponseFeatures(List<HRURequestFeature> hruRequestFeatures)
    {
        var newHRUCharacteristics = new List<HRUResponseFeature>();
        var rawResponse = string.Empty;
        try
        {
            var esriGPRecordSetLayer = (await SubmitHRURequestJobAndRetrieveResults(hruRequestFeatures)).Value;
            newHRUCharacteristics.AddRange(esriGPRecordSetLayer.Features.Where(x => x.Attributes.ImperviousAcres != null));
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex.Message, ex);
            Logger.LogWarning($"Skipped entities (ProjectLGUs if Project modeling, otherwise LGUs) with these IDs: {string.Join(", ", hruRequestFeatures.Select(x => x.Attributes.QueryFeatureID.ToString()))}");
            Logger.LogWarning(rawResponse);
        }

        return newHRUCharacteristics;
    }

    private async Task<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>
        SubmitHRURequestJobAndRetrieveResults(List<HRURequestFeature> featuresForHRURequest)
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
        while (retry && attempts < MAX_RETRIES)
        {
            var httpResponseMessage = await HttpClient.PostAsync($"{HRUServiceEndPoint}/submitJob", new FormUrlEncodedContent(keyValuePairs));
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

        switch (jobStatusResponse.jobStatus)
        {
            case EsriJobStatus.esriJobSucceeded:
                var resultContent = await HttpClient.GetFromJsonAsync<EsriAsynchronousJobOutputParameter<EsriGPRecordSetLayer<HRUResponseFeature>>>($"{HRUServiceEndPoint}/jobs/{jobID}/results/output_table/?f=json", GeoJsonSerializer.DefaultSerializerOptions);
                return resultContent;
            case EsriJobStatus.esriJobCancelling:
            case EsriJobStatus.esriJobCancelled:
                throw new EsriAsynchronousJobCancelledException(jobStatusResponse.jobId);
            case EsriJobStatus.esriJobFailed:
                throw new EsriAsynchronousJobFailedException(jobStatusResponse, requestObject.ToString());
            default:
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("jobStatusResponse.jobStatus",
                    $"Unexpected job status from HRU job {jobStatusResponse.jobId}. Last message: {jobStatusResponse.messages.Last().description}");
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

    private async Task<FeatureCollection> RetrieveFeatureCollectionFromArcServer(string url, string serviceName)
    {
        var collectedFeatureCollection = new FeatureCollection();
        var resultOffset = 0;
        var done = false;

        while (!done)
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>
            {
                new("where", "1=1"),
                new("geometryType", "esriGeometryEnvelope"),
                new("spatialRel", "esriSpatialRelIntersects"),
                new("outFields", "*"),
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

    public class PrecipitationZoneFromEsri : IHasGeometry
    {
        public Geometry Geometry { get; set; }
        [JsonPropertyName("ID")]
        public int PrecipitationZoneKey { get; set; }
        [JsonPropertyName("RainfallZo")]
        public double DesignStormwaterDepthInInches { get; set; }
    }
}