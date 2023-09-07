using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanParcels
{
    public static IQueryable<WaterQualityManagementPlanParcel> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanParcels
            .Include(x => x.Parcel);
    }

    public static WaterQualityManagementPlanParcel GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int waterQualityManagementPlanParcelID)
    {
        var waterQualityManagementPlanParcel = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanParcelID == waterQualityManagementPlanParcelID);
        Check.RequireNotNull(waterQualityManagementPlanParcel,
            $"WaterQualityManagementPlanParcel with ID {waterQualityManagementPlanParcelID} not found!");
        return waterQualityManagementPlanParcel;
    }

    public static WaterQualityManagementPlanParcel GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        WaterQualityManagementPlanParcelPrimaryKey waterQualityManagementPlanParcelPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanParcelPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlanParcel GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanParcelID)
    {
        var waterQualityManagementPlanParcel = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanParcelID == waterQualityManagementPlanParcelID);
        Check.RequireNotNull(waterQualityManagementPlanParcel,
            $"WaterQualityManagementPlanParcel with ID {waterQualityManagementPlanParcelID} not found!");
        return waterQualityManagementPlanParcel;
    }

    public static WaterQualityManagementPlanParcel GetByID(NeptuneDbContext dbContext,
        WaterQualityManagementPlanParcelPrimaryKey waterQualityManagementPlanParcelPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanParcelPrimaryKey.PrimaryKeyValue);
    }

    public static List<WaterQualityManagementPlanParcel> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
    }
}