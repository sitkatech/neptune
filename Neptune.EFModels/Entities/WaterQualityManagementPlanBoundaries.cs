using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanBoundaries
{
    public static IQueryable<WaterQualityManagementPlanBoundary> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanBoundaries
            .Include(x => x.WaterQualityManagementPlan);
    }

    public static WaterQualityManagementPlanBoundary GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int waterQualityManagementPlanGeometryID)
    {
        var waterQualityManagementPlanBoundary = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanGeometryID == waterQualityManagementPlanGeometryID);
        Check.RequireNotNull(waterQualityManagementPlanBoundary,
            $"WaterQualityManagementPlanBoundary with ID {waterQualityManagementPlanGeometryID} not found!");
        return waterQualityManagementPlanBoundary;
    }

    public static WaterQualityManagementPlanBoundary GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        WaterQualityManagementPlanBoundaryPrimaryKey waterQualityManagementPlanBoundaryPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanBoundaryPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlanBoundary GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanGeometryID)
    {
        var waterQualityManagementPlanBoundary = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanGeometryID == waterQualityManagementPlanGeometryID);
        Check.RequireNotNull(waterQualityManagementPlanBoundary,
            $"WaterQualityManagementPlanBoundary with ID {waterQualityManagementPlanGeometryID} not found!");
        return waterQualityManagementPlanBoundary;
    }

    public static WaterQualityManagementPlanBoundary GetByID(NeptuneDbContext dbContext,
        WaterQualityManagementPlanBoundaryPrimaryKey waterQualityManagementPlanBoundaryPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanBoundaryPrimaryKey.PrimaryKeyValue);
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
}