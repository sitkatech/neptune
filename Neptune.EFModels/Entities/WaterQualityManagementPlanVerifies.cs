using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanVerifies
{
    public static IQueryable<WaterQualityManagementPlanVerify> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlanVerifies
            .Include(x => x.WaterQualityManagementPlan)
            .ThenInclude(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.LastEditedByPerson);
    }

    public static WaterQualityManagementPlanVerify GetByIDWithChangeTracking(NeptuneDbContext dbContext, int waterQualityManagementPlanVerifyID)
    {
        var waterQualityManagementPlanVerify = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerifyID);
        Check.RequireNotNull(waterQualityManagementPlanVerify, $"WaterQualityManagementPlanVerify with ID {waterQualityManagementPlanVerifyID} not found!");
        return waterQualityManagementPlanVerify;
    }

    public static WaterQualityManagementPlanVerify GetByIDWithChangeTracking(NeptuneDbContext dbContext, WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanVerifyPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlanVerify GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanVerifyID)
    {
        var waterQualityManagementPlanVerify = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerifyID);
        Check.RequireNotNull(waterQualityManagementPlanVerify, $"WaterQualityManagementPlanVerify with ID {waterQualityManagementPlanVerifyID} not found!");
        return waterQualityManagementPlanVerify;
    }

    public static WaterQualityManagementPlanVerify GetByID(NeptuneDbContext dbContext, WaterQualityManagementPlanVerifyPrimaryKey waterQualityManagementPlanVerifyPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanVerifyPrimaryKey.PrimaryKeyValue);
    }

    public static List<WaterQualityManagementPlanVerify> ListViewable(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDsPersonCanView)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.WaterQualityManagementPlan.StormwaterJurisdictionID))
            .OrderBy(x => x.WaterQualityManagementPlan.StormwaterJurisdiction.Organization.OrganizationName)
            .ThenBy(x => x.WaterQualityManagementPlan.WaterQualityManagementPlanName)
            .ThenByDescending(x => x.LastEditedDate).ToList();
    }

    public static List<WaterQualityManagementPlanVerify> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).OrderByDescending(x => x.VerificationDate).ToList();
    }
}