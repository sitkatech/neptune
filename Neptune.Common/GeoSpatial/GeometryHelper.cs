using NetTopologySuite.Geometries;

namespace Neptune.Common.GeoSpatial;

public static class GeometryHelper
{
    public static Geometry CreateLocationPoint4326FromLatLong(double latitude, double longitude)
    {
        return new Point(longitude, latitude) { SRID = 4326 };
    }

    public static Geometry UnionListGeometries(this IList<Geometry> inputGeometries)
    {
        if (inputGeometries.Count == 0)
        {
            return null;
        }

        Geometry union;

        try
        {

            // all geometries have to have the same SRS or the union isn't defined anyway, so just grab the first one
            var coordinateSystemId = inputGeometries.First().SRID;

            var reader = new NetTopologySuite.IO.WKBReader();

            var internalGeometries = inputGeometries.Select(x => x.Buffer(0)).Select(x => reader.Read(x.AsBinary()))
                .ToList();

            union = NetTopologySuite.Operation.Union.CascadedPolygonUnion.Union(internalGeometries);
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

            return union;
        }
    }

}