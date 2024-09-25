using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlanNereidLogs
{
    public static WaterQualityManagementPlanNereidLog? GetByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        return dbContext.WaterQualityManagementPlanNereidLogs.AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID && x.LastRequestDate != null);
    }
}