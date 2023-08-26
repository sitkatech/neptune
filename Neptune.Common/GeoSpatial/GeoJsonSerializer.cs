using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Neptune.Common.JsonConverters;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using NetTopologySuite.IO.Converters;
using Qanat.Common.GeoSpatial;

namespace Neptune.Common.GeoSpatial;

public static class GeoJsonSerializer
{
    public static void RemoveAllProperties(IFeature feature)
    {
        // Just replace the AttributesTable with a new one instead of deleting all properties
        // because the STJ-serialized Feature has a read-only AttributesTable. 
        feature.Attributes = new AttributesTable();
    }

    public static Envelope GetExtentForFeatureCollection(FeatureCollection featureCollection, int? optionalBuffer)
    {
        var maxX = featureCollection.Max(x => x.Geometry.EnvelopeInternal.MaxX);
        var minX = featureCollection.Min(x => x.Geometry.EnvelopeInternal.MinX);
        var maxY = featureCollection.Max(x => x.Geometry.EnvelopeInternal.MaxY);
        var minY = featureCollection.Min(x => x.Geometry.EnvelopeInternal.MinY);
        var wkt = $"POLYGON(({minX} {minY}, {minX} {maxY}, {maxX} {maxY}, {maxX} {minY}, {minX} {minY}))";

        var envelope = new Envelope(minX, maxX, minY, maxY);
        if (optionalBuffer.HasValue)
        {
            envelope.ExpandBy(optionalBuffer.Value);
        }
        return envelope;
    }

    public static string GetGeoJsonStringFromGeoJsonByteArray(byte[] fileContentsByteArray)
    {
        return Encoding.UTF8.GetString(fileContentsByteArray);
    }

    public static async Task<T> DeserializeFromFileAsync<T>(string pathToGeoJsonFile, JsonSerializerOptions jsonSerializerOptions)
    {
        await using var openStream = File.OpenRead(pathToGeoJsonFile);
        var deserializeAsync = await JsonSerializer.DeserializeAsync<T>(openStream, jsonSerializerOptions);
        await openStream.DisposeAsync();
        return deserializeAsync;
    }

    public static T DeserializeFromFile<T>(string pathToGeoJsonFile, JsonSerializerOptions jsonSerializerOptions)
    {
        using var openStream = File.OpenRead(pathToGeoJsonFile);
        return JsonSerializer.Deserialize<T>(openStream, jsonSerializerOptions);
    }

    public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonString(string geojson, JsonSerializerOptions jsonSerializerOptions)
    {
        await using var ms = new MemoryStream(Encoding.UTF8.GetBytes(geojson));
        return await GetFeatureCollectionFromGeoJsonStream(ms, jsonSerializerOptions);
    }

    public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonByteArray(byte[] fileContentsByteArray, JsonSerializerOptions jsonSerializerOptions)
    {
        await using var memoryStream = new MemoryStream(fileContentsByteArray);
        return await GetFeatureCollectionFromGeoJsonStream(memoryStream, jsonSerializerOptions);
    }

    public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonStream(Stream stream, JsonSerializerOptions jsonSerializerOptions)
    {
        return await JsonSerializer.DeserializeAsync<FeatureCollection>(stream, jsonSerializerOptions);
    }

    public static async Task<List<IFeature>> GetFeatureCollectionFromGeoJsonByteArray(byte[] fileContentsByteArray, IPreparedGeometry boundingBox, JsonSerializerOptions jsonSerializerOptions)
    {
        var featureCollection = await GetFeatureCollectionFromGeoJsonByteArray(fileContentsByteArray, jsonSerializerOptions);
        return featureCollection.Where(x => boundingBox.Intersects(x.Geometry)).ToList();
    }

    public static JsonSerializerOptions CreateGeoJSONSerializerOptions()
    {
        var jsonSerializerOptions = CreateDefaultJSONSerializerOptions(2);
        var scale = Math.Pow(10, 3);
        var geometryFactory = new GeometryFactory(new PrecisionModel(scale), 4326);
        jsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactory, false));
        return jsonSerializerOptions;
    }

    public static JsonSerializerOptions CreateGeoJSONSerializerOptions(int coordinateSystemID, int coordinatePrecision, int numberOfSignificantDigits)
    {
        var jsonSerializerOptions = CreateDefaultJSONSerializerOptions(numberOfSignificantDigits);
        var scale = Math.Pow(10, coordinatePrecision);
        var geometryFactory = new GeometryFactory(new PrecisionModel(scale), coordinateSystemID);
        jsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactory, false));
        return jsonSerializerOptions;
    }

    public static JsonSerializerOptions CreateDefaultJSONSerializerOptions(int numberOfSignificantDigits)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            PropertyNameCaseInsensitive = false,
            PropertyNamingPolicy = null
        };
        jsonSerializerOptions.Converters.Add(new DateTimeConverter());
        jsonSerializerOptions.Converters.Add(new DoubleConverter(numberOfSignificantDigits));
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        return jsonSerializerOptions;
    }

    public static async Task SerializeToStream<T>(T objectToSerialize, JsonSerializerOptions jsonSerializerOptions, MemoryStream stream)
    {
        await JsonSerializer.SerializeAsync(stream, objectToSerialize, jsonSerializerOptions);
    }

    public static async Task SerializeToFileAsync<T>(T objectToSerialize, string fileOutput, JsonSerializerOptions jsonSerializerOptions)
    {
        await using var createStream = File.Create(fileOutput);
        await JsonSerializer.SerializeAsync(createStream, objectToSerialize, jsonSerializerOptions);
        await createStream.DisposeAsync();
    }

    public static void SerializeToFile<T>(T objectToSerialize, string fileOutput, JsonSerializerOptions jsonSerializerOptions)
    {
        using var createStream = File.Create(fileOutput);
        JsonSerializer.Serialize(createStream, objectToSerialize, jsonSerializerOptions);
    }

    public static void SerializeAsFeatureCollectionToFile(IEnumerable<IHasGeometry> features, string fileOutput, JsonSerializerOptions jsonSerializerOptions)
    {
        SerializeToFile(features.ToFeatureCollection(), fileOutput, jsonSerializerOptions);
    }

    public static async Task SerializeAsFeatureCollectionToFileAsync(IEnumerable<IHasGeometry> features, string fileOutput, JsonSerializerOptions jsonSerializerOptions)
    {
        await SerializeToFileAsync(features.ToFeatureCollection(), fileOutput, jsonSerializerOptions);
    }

    public static FeatureCollection ToFeatureCollection(this IEnumerable<IHasGeometry> features)
    {
        var featureCollection = new FeatureCollection();
        foreach (var feature in features)
        {
            featureCollection.Add(feature.ToGeoJsonFeature());
        }

        return featureCollection;
    }

    public static async Task SerializeAsGeoJsonToStream(FeatureCollection featureCollection, JsonSerializerOptions jsonSerializerOptions, MemoryStream stream)
    {
        await SerializeToStream<FeatureCollection>(featureCollection, jsonSerializerOptions, stream);
    }

    public static byte[] WriteFeaturesToByteArray(IEnumerable<IFeature> features, JsonSerializerOptions jsonSerializerOptions)
    {
        var featureCollection = new FeatureCollection();
        foreach (var feature in features)
        {
            featureCollection.Add(feature);
        }

        return SerializeToByteArray(featureCollection, jsonSerializerOptions);
    }

    private static byte[] SerializeToByteArray<T>(T objectToSerialize, JsonSerializerOptions jsonSerializerOptions)
    {
        return JsonSerializer.SerializeToUtf8Bytes(objectToSerialize, jsonSerializerOptions);
    }

    public static T DeserializeFromFeature<T>(IFeature feature, JsonSerializerOptions geoJSONSerializerOptions) where T : IHasGeometry
    {
        feature.Attributes.TryDeserializeJsonObject<T>(geoJSONSerializerOptions, out var deserialized);
        deserialized.Geometry = feature.Geometry;
        return deserialized;
    }

    public static T DeserializeFromFeatureWithNoGeometry<T>(IFeature feature, JsonSerializerOptions geoJSONSerializerOptions)
    {
        feature.Attributes.TryDeserializeJsonObject<T>(geoJSONSerializerOptions, out var deserialized);
        return deserialized;
    }

    public static async Task<List<T>> DeserializeFromFeatureCollection<T>(byte[] byteArray, JsonSerializerOptions geoJSONSerializerOptions) where T : IHasGeometry
    {
        var featureCollection = await GetFeatureCollectionFromGeoJsonByteArray(byteArray, geoJSONSerializerOptions);
        return DeserializeFromFeatureCollection<T>(featureCollection, geoJSONSerializerOptions);
    }

    public static List<T> DeserializeFromFeatureCollection<T>(FeatureCollection featureCollection, JsonSerializerOptions geoJSONSerializerOptions) where T : IHasGeometry
    {
        return featureCollection.AsParallel().Select(x => DeserializeFromFeature<T>(x, geoJSONSerializerOptions)).ToList();
    }

    public static async Task<List<T>> DeserializeFromFeatureCollectionWithNoGeometry<T>(byte[] byteArray, JsonSerializerOptions geoJSONSerializerOptions)
    {
        var featureCollection = await GetFeatureCollectionFromGeoJsonByteArray(byteArray, geoJSONSerializerOptions);
        return DeserializeFromFeatureCollectionWithNoGeometry<T>(featureCollection, geoJSONSerializerOptions);
    }

    public static List<T> DeserializeFromFeatureCollectionWithNoGeometry<T>(FeatureCollection featureCollection, JsonSerializerOptions geoJSONSerializerOptions)
    {
        return featureCollection.AsParallel().Select(x => DeserializeFromFeatureWithNoGeometry<T>(x, geoJSONSerializerOptions)).ToList();
    }

    public static Feature ToGeoJsonFeature<T>(this T featureClass) where T : IHasGeometry
    {
        var dictionary = ToKeyValuePairList(featureClass);
        var attributesTable = new AttributesTable(dictionary);
        return new Feature(featureClass.Geometry, attributesTable);
    }

    public static Dictionary<string, object> ToKeyValuePairList<T>(T obj)
    {
        return obj.GetType().GetProperties().Where(x => !x.IsDefined(typeof(JsonIgnoreAttribute), false)).ToDictionary(p => p.Name, p => p.GetValue(obj));
        //            return obj.GetType().GetProperties().Where(x => !x.IsDefined(typeof(JsonIgnoreAttribute), false)).Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(obj))).ToList();
    }
}