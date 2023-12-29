// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class HRURequestFeature
    {
        [JsonPropertyName("attributes")]
        public HRURequestFeatureAttributes Attributes { get; set; }
        [JsonPropertyName("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        public HRURequestFeature(Polygon catchmentGeometry, HRURequestFeatureAttributes baseAttributes, int i)
        {
            var rings = new List<List<double[]>>();
            var catchmentGeometryExteriorRing = catchmentGeometry.ExteriorRing;

            var exteriorRingCoordinates = GetRingCoordinates(catchmentGeometryExteriorRing);

            // need to account for interior rings
            // need to skip geometries with exterior rings I guess

            rings.Add(exteriorRingCoordinates);

            for (var j = 0; j < catchmentGeometry.InteriorRings.Length; j++)
            {
                var interiorRing = catchmentGeometry.GetInteriorRingN(j);
                var interiorRingCoordinates = GetRingCoordinates(interiorRing);
                rings.Add(interiorRingCoordinates);
            }

            Geometry = new EsriPolygonGeometry
            {
                Rings = rings
            };

            Attributes = new HRURequestFeatureAttributes
            {
                ObjectID = baseAttributes.ObjectID,
                QueryFeatureID = i,
                Area = baseAttributes.Area,
                Length = baseAttributes.Length
            };
        }

        private static List<double[]> GetRingCoordinates(LineString catchmentGeometryExteriorRing)
        {
            var coordinates = new List<double[]>();
            for (var j = 0; j < catchmentGeometryExteriorRing.NumPoints; j++)
            {
                var point = catchmentGeometryExteriorRing.GetPointN(j);
                var lon = point.X;
                var lat = point.Y;

                coordinates.Add(new[] {lon, lat});
            }

            return coordinates;
        }
    }
}