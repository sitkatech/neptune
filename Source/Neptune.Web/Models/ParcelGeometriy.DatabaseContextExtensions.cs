using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DbSpatial;

namespace Neptune.Web.Models;

public static partial class DatabaseContextExtensions
{
    public static DbGeometry UnionAggregateByParcelIDs(this IQueryable<ParcelGeometry> parcelGeometries,
        IEnumerable<int> parcelIDs)
    {
        return parcelGeometries
            .Where(x => parcelIDs.Contains(x.ParcelID)).Select(x => x.GeometryNative).ToList()
            .UnionListGeometries();
    }

    public static IQueryable<ParcelGeometry> GetIntersected(this IQueryable<ParcelGeometry> parcelGeometries,
        DbGeometry geometryToIntersect)
    {
        return parcelGeometries.Where(x => x.GeometryNative.Intersects(geometryToIntersect));
    }
}