using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class NereidLogs
{
    public static NereidLog? GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return dbContext.TreatmentBMPs.Include(x => x.LastNereidLog).AsNoTracking().SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID)?.LastNereidLog;
    }

    public static NereidLog? GetByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return dbContext.WaterQualityManagementPlans.Include(x => x.LastNereidLog).AsNoTracking().SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID)?.LastNereidLog;
    }
}