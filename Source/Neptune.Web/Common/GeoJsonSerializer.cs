using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LtInfo.Common.GdalOgr;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using NetTopologySuite.IO.Converters;

namespace Neptune.Web.Common
{
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

        public static async Task<T> DeserializeFromFile<T>(string pathToGeoJsonFile, JsonSerializerOptions jsonSerializerOptions)
        {
            using var streamReader = new StreamReader(File.OpenRead(pathToGeoJsonFile));
            return await Deserialize<T>(streamReader.BaseStream, jsonSerializerOptions);
        }

        public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonString(string geojson, JsonSerializerOptions jsonSerializerOptions)
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(geojson));
            return await GetFeatureCollectionFromGeoJsonStream(ms, jsonSerializerOptions);
        }

        public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonByteArray(byte[] fileContentsByteArray, JsonSerializerOptions jsonSerializerOptions)
        {
            using var memoryStream = new MemoryStream(fileContentsByteArray);
            return await GetFeatureCollectionFromGeoJsonStream(memoryStream, jsonSerializerOptions);
        }

        public static async Task<FeatureCollection> GetFeatureCollectionFromGeoJsonStream(Stream stream, JsonSerializerOptions jsonSerializerOptions)
        {
            return await Deserialize<FeatureCollection>(stream, jsonSerializerOptions);
        }

        public static async Task<List<IFeature>> GetFeatureCollectionFromGeoJsonByteArray(byte[] fileContentsByteArray, IPreparedGeometry boundingBox, JsonSerializerOptions jsonSerializerOptions)
        {
            var featureCollection = await GetFeatureCollectionFromGeoJsonByteArray(fileContentsByteArray, jsonSerializerOptions);
            return featureCollection.Where(x => boundingBox.Intersects(x.Geometry)).ToList();
        }

        public static JsonSerializerOptions CreateGeoJSONSerializerOptions(int coordinatePrecision, int numberOfSignificantDigits)
        {
            var jsonSerializerOptions = CreateDefaultJSONSerializerOptions(numberOfSignificantDigits);
            var scale = Math.Pow(10, coordinatePrecision);
            var geometryFactory = new GeometryFactory(new PrecisionModel(scale), Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId);
            jsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactory));
            return jsonSerializerOptions;
        }

        public static JsonSerializerOptions CreateDefaultJSONSerializerOptions(int numberOfSignificantDigits)
        {
            var jsonSerializerOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
            jsonSerializerOptions.Converters.Add(new IntConverter());
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            jsonSerializerOptions.Converters.Add(new NullableConverterFactory());
            jsonSerializerOptions.PropertyNameCaseInsensitive = false;
            jsonSerializerOptions.PropertyNamingPolicy = null;
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            jsonSerializerOptions.IgnoreNullValues = false;
            return jsonSerializerOptions;
        }

        public static async Task<T> Deserialize<T>(Stream input, JsonSerializerOptions defaultSerializerOptions)
        {
            return await JsonSerializer.DeserializeAsync<T>(input, defaultSerializerOptions);
        }

        public static async Task WriteFeaturesToGeoJsonStream(IEnumerable<IFeature> features, JsonSerializerOptions jsonSerializerOptions, MemoryStream memoryStream)
        {
            var featureCollection = new FeatureCollection();
            foreach (var feature in features)
            {
                featureCollection.Add(feature);
            }

            await WriteFeaturesToGeoJsonStream(featureCollection, jsonSerializerOptions, memoryStream);
        }

        public static async Task WriteFeaturesToGeoJsonStream(FeatureCollection featureCollection, JsonSerializerOptions jsonSerializerOptions, MemoryStream memoryStream)
        {
            await SerializeToStream(featureCollection, jsonSerializerOptions, memoryStream);
        }

        public static async Task SerializeToStream<T>(T objectToSerialize, JsonSerializerOptions jsonSerializerOptions, MemoryStream stream)
        {
            await JsonSerializer.SerializeAsync(stream, objectToSerialize, jsonSerializerOptions);
        }

        public static async Task SerializeToFile<T>(T objectToSerialize, string fileOutput, JsonSerializerOptions jsonSerializerOptions)
        {
            using var createStream = File.Create(fileOutput);
            await JsonSerializer.SerializeAsync(createStream, objectToSerialize, jsonSerializerOptions);
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
            var success = feature.Attributes.TryDeserializeJsonObject<T>(geoJSONSerializerOptions, out var deserialized);
            if (success)
            {
                deserialized.Geometry = feature.Geometry;
            }
            return deserialized;
        }

        public static T DeserializeFromFeatureWithNoGeometry<T>(IFeature feature, JsonSerializerOptions geoJSONSerializerOptions)
        {
            feature.Attributes.TryDeserializeJsonObject<T>(geoJSONSerializerOptions, out var deserialized);
            return deserialized;
        }

        public static Feature ToGeoJsonFeature<T>(this T featureClass, JsonSerializerOptions jsonSerializerOptions) where T : IHasGeometry
        {
            var dictionary = ToDictionary<T, object>(featureClass, jsonSerializerOptions);
            var attributesTable = new AttributesTable(dictionary);
            return new Feature(featureClass.Geometry, attributesTable);
        }

        public static Dictionary<string, TValue> ToDictionary<T, TValue>(T obj, JsonSerializerOptions jsonSerializerOptions)
        {
            return JsonSerializer.Deserialize<Dictionary<string, TValue>>(JsonSerializer.Serialize(obj, jsonSerializerOptions), jsonSerializerOptions);
        }

        public static Dictionary<string, TValue> ToDictionary<T, TValue>(T obj, string attributesPrefix, JsonSerializerOptions jsonSerializerOptions)
        {
            var dictionary = ToDictionary<T, TValue>(obj, jsonSerializerOptions);
            return dictionary.ToDictionary(x => attributesPrefix + x.Key, x => x.Value);
        }
    }
}