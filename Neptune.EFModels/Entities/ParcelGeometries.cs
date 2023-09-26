using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class ParcelGeometries
{
    public static Geometry UnionAggregateByParcelIDs(NeptuneDbContext dbContext, IEnumerable<int> parcelIDs)
    {
        return dbContext.ParcelGeometries.AsNoTracking()
            .Where(x => parcelIDs.Contains(x.ParcelID) && x.Geometry4326 != null).Select(x => x.Geometry4326).ToList()
            .UnionListGeometries();
    }

    public static IQueryable<ParcelGeometry> GetIntersected(NeptuneDbContext dbContext, Geometry geometryToIntersect)
    {
        return dbContext.ParcelGeometries.AsNoTracking().Where(x => x.GeometryNative.Intersects(geometryToIntersect));
    }
}