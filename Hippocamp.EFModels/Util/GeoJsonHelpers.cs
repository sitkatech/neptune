using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hippocamp.API.Util
{
    public static class GeoJsonHelpers
    {

        public static IEnumerable<Feature> GetFeaturesFromGeoJsons(List<string> geoJsonStrings)
        {
            var returnList = new List<Feature>();

            geoJsonStrings.ForEach(geoJsonString =>
            {
                returnList.Add(GetFeatureFromGeoJson(geoJsonString));
            });

            return returnList;
        }

        public static Feature GetFeatureFromGeoJson(string geoJsonString)
        {
            using (var stringReader = new StringReader(geoJsonString))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                var geometryFactory = new GeometryFactory();
                var reader = new GeoJsonReader(geometryFactory, new JsonSerializerSettings());
                return reader.Read<Feature>(jsonReader);
            }
        }

        public static IEnumerable<string> GetGeoJsonsFromGeometries(IEnumerable<Geometry> geometries)
        {
            var returnList = new List<string>();

            foreach (var geometry in geometries)
            {
                returnList.Add(GetGeoJsonFromGeometry(geometry));
            }

            return returnList;
        }

        public static string GetGeoJsonFromGeometry(Geometry geometry)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                serializer.Serialize(jsonWriter, geometry);
                return stringWriter.ToString();
            }
        }
    }
}
