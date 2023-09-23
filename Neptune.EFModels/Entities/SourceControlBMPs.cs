using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class SourceControlBMPs
{
    public static IQueryable<SourceControlBMP> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.SourceControlBMPs.Include(x => x.SourceControlBMPAttribute);
    }

    public static SourceControlBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int sourceControlBMPID)
    {
        var sourceControlBMP = GetImpl(dbContext)
            .SingleOrDefault(x => x.SourceControlBMPID == sourceControlBMPID);
        Check.RequireNotNull(sourceControlBMP,
            $"SourceControlBMP with ID {sourceControlBMPID} not found!");
        return sourceControlBMP;
    }

    public static SourceControlBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        SourceControlBMPPrimaryKey sourceControlBMPPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, sourceControlBMPPrimaryKey.PrimaryKeyValue);
    }

    public static SourceControlBMP GetByID(NeptuneDbContext dbContext, int sourceControlBMPID)
    {
        var sourceControlBMP = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.SourceControlBMPID == sourceControlBMPID);
        Check.RequireNotNull(sourceControlBMP,
            $"SourceControlBMP with ID {sourceControlBMPID} not found!");
        return sourceControlBMP;
    }

    public static SourceControlBMP GetByID(NeptuneDbContext dbContext,
        SourceControlBMPPrimaryKey sourceControlBMPPrimaryKey)
    {
        return GetByID(dbContext, sourceControlBMPPrimaryKey.PrimaryKeyValue);
    }

    public static List<SourceControlBMP> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
    }

    public static List<SourceControlBMP> ListByWaterQualityManagementPlanIDWithChangeTracking(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return GetImpl(dbContext).Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
    }
}