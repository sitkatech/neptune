using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;

namespace Neptune.Common.GeoSpatial;

public enum PolygonalRepairOutcome
{
    Valid,
    Repaired,
    Skipped
}

public record PolygonalRepairResult(PolygonalRepairOutcome Outcome, Geometry? Geometry, string? SkipReason)
{
    public static PolygonalRepairResult Skip(string reason) => new(PolygonalRepairOutcome.Skipped, null, reason);
}

/// <summary>
/// NPT-1140: repairs a geometry that must end up as a valid, non-empty Polygon or MultiPolygon
/// (e.g. a parcel). Invalid input goes through NTS <see cref="GeometryFixer"/>; a fix that comes
/// back as a GeometryCollection keeps only its polygonal parts. Anything that still isn't a valid
/// polygon is reported as skipped so the caller can record it instead of loading it.
/// </summary>
public static class PolygonalGeometryRepair
{
    public static PolygonalRepairResult Repair(Geometry? geometry)
    {
        if (geometry == null || geometry.IsEmpty)
        {
            return PolygonalRepairResult.Skip("empty geometry");
        }

        if (IsPolygonal(geometry) && geometry.IsValid)
        {
            return new PolygonalRepairResult(PolygonalRepairOutcome.Valid, geometry, null);
        }

        Geometry fixedGeometry;
        try
        {
            fixedGeometry = GeometryFixer.Fix(geometry);
        }
        catch (Exception ex)
        {
            return PolygonalRepairResult.Skip($"repair failed: {ex.Message}");
        }

        var polygonal = ExtractPolygonal(fixedGeometry);
        if (polygonal == null || polygonal.IsEmpty)
        {
            return PolygonalRepairResult.Skip($"not a polygon after repair ({fixedGeometry.GeometryType})");
        }

        if (!polygonal.IsValid)
        {
            return PolygonalRepairResult.Skip("still invalid after repair");
        }

        polygonal.SRID = geometry.SRID;
        return new PolygonalRepairResult(PolygonalRepairOutcome.Repaired, polygonal, null);
    }

    private static bool IsPolygonal(Geometry geometry) => geometry is Polygon or MultiPolygon;

    private static Geometry? ExtractPolygonal(Geometry geometry)
    {
        if (IsPolygonal(geometry))
        {
            return geometry;
        }

        if (geometry is not GeometryCollection collection)
        {
            return null;
        }

        var polygons = new List<Polygon>();
        for (var i = 0; i < collection.NumGeometries; i++)
        {
            switch (collection.GetGeometryN(i))
            {
                case Polygon polygon when !polygon.IsEmpty:
                    polygons.Add(polygon);
                    break;
                case MultiPolygon multiPolygon:
                    for (var j = 0; j < multiPolygon.NumGeometries; j++)
                    {
                        polygons.Add((Polygon)multiPolygon.GetGeometryN(j));
                    }
                    break;
            }
        }

        return polygons.Count switch
        {
            0 => null,
            1 => polygons[0],
            _ => geometry.Factory.CreateMultiPolygon(polygons.ToArray())
        };
    }
}
