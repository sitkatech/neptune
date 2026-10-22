using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class ParcelGeometries
{
    public const int ProjectionBatchSize = 5000;

    /// <summary>
    /// NPT-1140: fills in missing Geometry4326 from GeometryNative, a bounded batch at a time.
    /// Projecting all ~985K parcels in one tracked list ran out of memory on 2026-09-23 and left
    /// every Geometry4326 null. Only rows still null are touched, so it's safe to re-run.
    /// Returns the number of rows filled in.
    /// </summary>
    public static async Task<int> Backfill4326Async(NeptuneDbContext dbContext, Action<int>? onBatchSaved = null)
    {
        var total = 0;
        var lastParcelGeometryID = 0;
        while (true)
        {
            var batch = await dbContext.ParcelGeometries
                .Where(x => x.Geometry4326 == null && x.ParcelGeometryID > lastParcelGeometryID)
                .OrderBy(x => x.ParcelGeometryID)
                .Take(ProjectionBatchSize)
                .ToListAsync();
            if (batch.Count == 0)
            {
                return total;
            }

            foreach (var parcelGeometry in batch)
            {
                parcelGeometry.Geometry4326 = parcelGeometry.GeometryNative.ProjectTo4326().MakeValid();
            }

            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
            total += batch.Count;
            lastParcelGeometryID = batch[^1].ParcelGeometryID;
            onBatchSaved?.Invoke(total);
        }
    }

    public static async Task<int> CountMissing4326Async(NeptuneDbContext dbContext)
    {
        return await dbContext.ParcelGeometries.AsNoTracking().CountAsync(x => x.Geometry4326 == null);
    }

    public static Geometry UnionAggregate4326ByParcelIDs(NeptuneDbContext dbContext, IEnumerable<int> parcelIDs)
    {
        return dbContext.ParcelGeometries.AsNoTracking()
            .Where(x => parcelIDs.Contains(x.ParcelID) && x.Geometry4326 != null).Select(x => x.Geometry4326).ToList()
            .UnionListGeometries();
    }

    public static Geometry UnionAggregateByParcelIDs(NeptuneDbContext dbContext, IEnumerable<int> parcelIDs)
    {
        return dbContext.ParcelGeometries.AsNoTracking()
            .Where(x => parcelIDs.Contains(x.ParcelID) && x.GeometryNative != null).Select(x => x.GeometryNative).ToList()
            .UnionListGeometries();
    }

    public static IQueryable<ParcelGeometry> GetIntersected(NeptuneDbContext dbContext, Geometry geometryToIntersect)
    {
        return dbContext.ParcelGeometries.AsNoTracking().Where(x => x.GeometryNative.Intersects(geometryToIntersect));
    }
}