using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class QuickBMPs
{
    public static IQueryable<QuickBMP> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.QuickBMPs
            .Include(x => x.TreatmentBMPType);
    }

    public static QuickBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int quickBMPID)
    {
        var quickBMP = GetImpl(dbContext)
            .SingleOrDefault(x => x.QuickBMPID == quickBMPID);
        Check.RequireNotNull(quickBMP,
            $"QuickBMP with ID {quickBMPID} not found!");
        return quickBMP;
    }

    public static QuickBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        QuickBMPPrimaryKey quickBMPPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, quickBMPPrimaryKey.PrimaryKeyValue);
    }

    public static QuickBMP GetByID(NeptuneDbContext dbContext, int quickBMPID)
    {
        var quickBMP = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.QuickBMPID == quickBMPID);
        Check.RequireNotNull(quickBMP,
            $"QuickBMP with ID {quickBMPID} not found!");
        return quickBMP;
    }

    public static QuickBMP GetByID(NeptuneDbContext dbContext,
        QuickBMPPrimaryKey quickBMPPrimaryKey)
    {
        return GetByID(dbContext, quickBMPPrimaryKey.PrimaryKeyValue);
    }

    public static List<QuickBMP> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).OrderBy(x => x.QuickBMPName).ToList();
    }

    public static List<QuickBMP> GetFullyParameterized(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x =>
                x.PercentOfSiteTreated != null && x.PercentCaptured != null && x.PercentRetained != null &&
                x.TreatmentBMPType.IsAnalyzedInModelingModule)
            .ToList();
    }
}