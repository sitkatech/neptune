using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanVerifyQuickBMPs
{
    public static IQueryable<WaterQualityManagementPlanVerifyQuickBMP> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanVerifyQuickBMPs.Include(x => x.WaterQualityManagementPlanVerify).ThenInclude(x => x.WaterQualityManagementPlan);
    }

    public static WaterQualityManagementPlanVerifyQuickBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int waterQualityManagementPlanVerifyQuickBMPID)
    {
        var waterQualityManagementPlanVerifyQuickBMP = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanVerifyQuickBMPID == waterQualityManagementPlanVerifyQuickBMPID);
        Check.RequireNotNull(waterQualityManagementPlanVerifyQuickBMP,
            $"WaterQualityManagementPlanVerifyQuickBMP with ID {waterQualityManagementPlanVerifyQuickBMPID} not found!");
        return waterQualityManagementPlanVerifyQuickBMP;
    }

    public static WaterQualityManagementPlanVerifyQuickBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        WaterQualityManagementPlanVerifyQuickBMPPrimaryKey waterQualityManagementPlanVerifyQuickBMPPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanVerifyQuickBMPPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlanVerifyQuickBMP GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanVerifyQuickBMPID)
    {
        var waterQualityManagementPlanVerifyQuickBMP = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanVerifyQuickBMPID == waterQualityManagementPlanVerifyQuickBMPID);
        Check.RequireNotNull(waterQualityManagementPlanVerifyQuickBMP,
            $"WaterQualityManagementPlanVerifyQuickBMP with ID {waterQualityManagementPlanVerifyQuickBMPID} not found!");
        return waterQualityManagementPlanVerifyQuickBMP;
    }

    public static WaterQualityManagementPlanVerifyQuickBMP GetByID(NeptuneDbContext dbContext,
        WaterQualityManagementPlanVerifyQuickBMPPrimaryKey waterQualityManagementPlanVerifyQuickBMPPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanVerifyQuickBMPPrimaryKey.PrimaryKeyValue);
    }

    public static List<WaterQualityManagementPlanVerifyQuickBMP> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanVerify.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
    }

    public static List<WaterQualityManagementPlanVerifyQuickBMP> ListByWaterQualityManagementPlanVerifyID(NeptuneDbContext dbContext, int waterQualityManagementPlanVerifyID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerifyID).ToList();
    }
}