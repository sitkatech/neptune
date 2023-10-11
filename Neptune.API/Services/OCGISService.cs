using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Hangfire;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.API.Services;

public class OCGISService : BaseAPIService<OCGISService>
{
    public OCGISService(HttpClient httpClient, ILogger<OCGISService> logger, IOptions<NeptuneConfiguration> neptuneConfigurationOptions) : base(httpClient, logger, neptuneConfigurationOptions, "OCGIS Service")
    {
    }

    /*
    AutoDelineateServiceUrl=        https://ocgis.com/arcpub/rest/services/Flood/GlobalStormwaterDelineation/GPServer/Global%20Stormwater%20Delineation/
    HRUServiceBaseUrl=              https://ocgis.com/arcpub/rest/services/Environmental_Resources/HRUSummary/GPServer/HRUSummary
    RegionalSubbasinServiceUrl=     https://ocgis.com/arcpub/rest/services/Environmental_Resources/RegionalSubbasins/MapServer/0/query
    ModelBasinServiceUrl=           https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/7/query
    PrecipitationZoneServiceUrl=    https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query
    OCTAPrioritizationServiceUrl=   https://ocgis.com/arcpub/rest/services/Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query
     */

    public async Task<List<OCTAPrioritizationFromEsri>> RetrieveOCTAPrioritizations()
    {
        const string serviceName = "OCTA Prioritization";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/8/query", serviceName);
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<OCTAPrioritizationFromEsri>(featureCollection);
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
        return result;
    }

    public async Task<List<PrecipitationZoneFromEsri>> RetrievePrecipitationZones()
    {
        const string serviceName = "Precipitation Zone";
        var featureCollection = await RetrieveFeatureCollectionFromArcServer("Environmental_Resources/Hydrologic_Response_Unit/MapServer/3/query", serviceName);
        var result = GeoJsonSerializer.DeserializeFromFeatureCollection<PrecipitationZoneFromEsri>(featureCollection);
        ThrowIfNotUnique(result.GroupBy(x => x.PrecipitationZoneKey), serviceName);
        return result;
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
            var queryStringObject = new EsriRequest
            {
                WhereClause = "1=1", 
                GeometryType = "esriGeometryEnvelope", 
                SpatialRel = "esriSpatialRelIntersects",
                OutputFields = "*",
                ReturnGeometry = true,
                ReturnTrueCurves = false,
                OutputSRID = 2771,
                ReturnIDsOnly = false,
                ReturnCountOnly = false,
                ReturnZ = false,
                ReturnM = false,
                ReturnDistinctValues = false,
                ReturnExtentOnly = false,
                Format = "geojson",
                ResultOffset = resultOffset,
                ResultRecordCount = 1000
            };

            var configurationSerialized = GeoJsonSerializer.Serialize(queryStringObject);
            var nameValueCollection = GeoJsonSerializer.Deserialize<Dictionary<string, string>>(configurationSerialized);
            var queryParameters = string.Join("&", nameValueCollection.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
            string response;
            try
            {
                response = await HttpClient.GetStringAsync($"{url}?{queryParameters}");
            }
            catch (TaskCanceledException tce)
            {
                throw new RemoteServiceException(
                    "The service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                    tce);
            }

            resultOffset += 1000;
            try
            {
                done = !GeoJsonSerializer.Deserialize<EsriQueryResponse>(response).ExceededTransferLimit;
            }
            catch (JsonException jre)
            {
                throw new RemoteServiceException(
                    $"The {serviceName} service failed to respond correctly. This happens occasionally for no particular reason, is outside of the Sitka development team's control, and will resolve on its own after a short wait. Do not file a bug report for this error.",
                    jre);
            }

            var featureCollection = GeoJsonSerializer.Deserialize<FeatureCollection>(response);
            foreach (var feature in featureCollection)
            {
                collectedFeatureCollection.Add(feature);
            }
        }

        return collectedFeatureCollection;
    }

    public class EsriRequest
    {
        [JsonPropertyName("where")]
        public string WhereClause { get; set; }
        [JsonPropertyName("geometryType")]
        public string GeometryType { get; set; }
        [JsonPropertyName("spatialRel")]
        public string SpatialRel { get; set; }
        [JsonPropertyName("outFields")]
        public string OutputFields { get; set; }
        [JsonPropertyName("returnGeometry")]
        public bool ReturnGeometry { get; set; }
        [JsonPropertyName("returnTrueCurves")]
        public bool ReturnTrueCurves { get; set; }
        [JsonPropertyName("outSR")]
        public int OutputSRID { get; set; }
        [JsonPropertyName("returnIdsOnly")]
        public bool ReturnIDsOnly { get; set; }
        [JsonPropertyName("returnCountOnly")]
        public bool ReturnCountOnly { get; set; }
        [JsonPropertyName("returnZ")]
        public bool ReturnZ { get; set; }
        [JsonPropertyName("returnM")]
        public bool ReturnM { get; set; }
        [JsonPropertyName("returnDistinctValues")]
        public bool ReturnDistinctValues { get; set; }
        [JsonPropertyName("returnExtentOnly")]
        public bool ReturnExtentOnly { get; set; }
        [JsonPropertyName("f")]
        public string Format { get; set; }
        [JsonPropertyName("resultOffset")]
        public int ResultOffset { get; set; }
        [JsonPropertyName("resultRecordCount")]
        public int ResultRecordCount { get; set; }
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