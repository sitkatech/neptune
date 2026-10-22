using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanBoundaries
{
    public static IQueryable<WaterQualityManagementPlanBoundary> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanBoundaries
            .Include(x => x.WaterQualityManagementPlan);
    }

    public static WaterQualityManagementPlanBoundary? GetByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
    }

    public static WaterQualityManagementPlanBoundary? GetByWaterQualityManagementPlanIDWithChangeTracking(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
    }

    public static FeatureCollection GetBoundaryAsFeatureCollection(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetBoundaryAsFeatureCollection(GetByWaterQualityManagementPlanID(dbContext, waterQualityManagementPlanID));
    }

    // NPT-1092: overload that reuses an already-loaded boundary so GetBoundary doesn't re-query it.
    public static FeatureCollection GetBoundaryAsFeatureCollection(WaterQualityManagementPlanBoundary? boundary)
    {
        if (boundary?.Geometry4326 != null)
        {
            return boundary.Geometry4326.MultiPolygonToFeatureCollection();
        }
        return new FeatureCollection();
    }

    public static async Task<Geometry?> UpdateBoundary(NeptuneDbContext dbContext, int waterQualityManagementPlanID, string? geometryAsGeoJson)
    {
        var boundary = GetByWaterQualityManagementPlanIDWithChangeTracking(dbContext, waterQualityManagementPlanID);
        var oldGeometryNative = boundary?.GeometryNative;

        if (string.IsNullOrWhiteSpace(geometryAsGeoJson))
        {
            if (boundary != null)
            {
                boundary.GeometryNative = null;
                boundary.Geometry4326 = null;
                await dbContext.SaveChangesAsync();
            }
            return oldGeometryNative;
        }

        var feature = GeoJsonSerializer.Deserialize<IFeature>(geometryAsGeoJson);
        feature.Geometry.SRID = Proj4NetHelper.WEB_MERCATOR;
        var geometry4326 = feature.Geometry.MakeValid();
        var geometryNative = geometry4326.ProjectTo2771();

        if (boundary == null)
        {
            boundary = new WaterQualityManagementPlanBoundary
            {
                WaterQualityManagementPlanID = waterQualityManagementPlanID
            };
            dbContext.WaterQualityManagementPlanBoundaries.Add(boundary);
        }

        boundary.Geometry4326 = geometry4326;
        boundary.GeometryNative = geometryNative;
        await dbContext.SaveChangesAsync();

        return oldGeometryNative;
    }

    /// <summary>
    /// NPT-1140: while parcels had no Geometry4326 (2026-09-23 sync), boundaries saved from parcel
    /// selection got a native geometry but an empty Geometry4326 (the 4326 union skips null parcel
    /// geometries), so they didn't draw on maps. Re-derives Geometry4326 from GeometryNative for
    /// those. Returns the WQMP IDs that were fixed.
    /// </summary>
    public static async Task<List<int>> Backfill4326FromNativeAsync(NeptuneDbContext dbContext)
    {
        var boundaries = await dbContext.WaterQualityManagementPlanBoundaries
            .Where(x => x.GeometryNative != null && (x.Geometry4326 == null || x.Geometry4326.IsEmpty))
            .ToListAsync();
        foreach (var boundary in boundaries)
        {
            boundary.Geometry4326 = boundary.GeometryNative!.ProjectTo4326().MakeValid();
        }

        await dbContext.SaveChangesAsync();
        return boundaries.Select(x => x.WaterQualityManagementPlanID).ToList();
    }

    public static double? CalculateAcreage(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return CalculateAcreage(GetByWaterQualityManagementPlanID(dbContext, waterQualityManagementPlanID));
    }

    // NPT-1092: overload that reuses an already-loaded boundary so GetBoundary doesn't re-query it.
    public static double? CalculateAcreage(WaterQualityManagementPlanBoundary? boundary)
    {
        if (boundary?.GeometryNative != null)
        {
            return Math.Round(boundary.GeometryNative.Area * Constants.SquareMetersToAcres, 1);
        }
        return null;
    }
}