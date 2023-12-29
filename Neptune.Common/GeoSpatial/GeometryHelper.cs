using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Neptune.Common.GeoSpatial;

public static class GeometryHelper
{
    public const string POLYGON_EMPTY = "POLYGON EMPTY";

    public static Geometry MakeValid(this Geometry geometry)
    {
        return !geometry.IsValid ? NetTopologySuite.Geometries.Utilities.GeometryFixer.Fix(geometry) : geometry;
    }

    public static Geometry CreateLocationPoint4326FromLatLong(double latitude, double longitude)
    {
        return new Point(longitude, latitude) { SRID = 4326 };
    }

    public static Geometry? UnionListGeometries(this IList<Geometry> inputGeometries)
    {
        if (inputGeometries.Count == 0)
        {
            return null;
        }

        Geometry union;
        // all geometries have to have the same SRS or the union isn't defined anyway, so just grab the first one
        var coordinateSystemId = inputGeometries.First().SRID;

        try
        {
            var reader = new NetTopologySuite.IO.WKBReader();

            var internalGeometries = inputGeometries.Select(x => x.MakeValid()).Select(x => reader.Read(x.AsBinary()))
                .ToList();

            union = NetTopologySuite.Operation.Union.CascadedPolygonUnion.Union(internalGeometries);
            union.SRID = coordinateSystemId;
            return union;
        }
        catch (TopologyException)
        {
            // fall back on the iterative union 

            union = inputGeometries.First();

            for (var i = 1; i < inputGeometries.Count; i++)
            {
                var temp = union.Union(inputGeometries[i]);
                union = temp;
            }
            union.SRID = coordinateSystemId;
            return union;
        }
    }

    public static Geometry? FromWKT(string? wkt, int srid)
    {
        if (string.IsNullOrWhiteSpace(wkt))
            return null;

        var geoReader = new WKTReader();
        var geometry = geoReader.Read(wkt);
        geometry.SRID = srid;
        return geometry;
    }

    public static FeatureCollection MultiPolygonToFeatureCollection(this Geometry potentialMultiPolygon)
    {
        if (potentialMultiPolygon.GeometryType.ToUpper() == "MULTIPOLYGON")
        {
            var featureCollection = new FeatureCollection();

            // Leaflet.Draw does not support multipolgyon editing because its dev team decided it wasn't necessary.
            // Unless https://github.com/Leaflet/Leaflet.draw/issues/268 is resolved, we have to break into separate polys.
            // On an unrelated note, DbGeometry.ElementAt is 1-indexed instead of 0-indexed, which is terrible.
            for (var i = 0; i < potentialMultiPolygon.NumGeometries; i++)
            {
                var geometry = potentialMultiPolygon.GetGeometryN(i);
                // Reduce is SQL Server's implementation of the Douglas–Peucker downsampling algorithm
                featureCollection.Add(new Feature(geometry.MakeValid(), new AttributesTable()));
            }

            return featureCollection;
        }

        return new FeatureCollection() { new Feature(potentialMultiPolygon, new AttributesTable())};
    }

    public static List<Geometry> MakeValidAndExplodeIfNeeded(Geometry geometry)
    {
        var geometries = new List<Geometry>();
        if (!geometry.IsValid)
        {
            var validGeometry = geometry.MakeValid();
            for (var i = 0; i < validGeometry.NumGeometries; i++)
            {
                var geometryPart = validGeometry.GetGeometryN(i);
                if (geometryPart.GeometryType.ToUpper() == "POLYGON")
                {
                    geometries.Add(geometryPart);
                }
            }
        }
        else
        {
            geometries.Add(geometry);
        }

        return geometries;
    }

}