using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlans
{
    public static IQueryable<WaterQualityManagementPlan> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlans.AsNoTracking()
            .Include(x => x.TreatmentBMPs)
            .Include(x => x.WaterQualityManagementPlanVerifies)
            .Include(x => x.WaterQualityManagementPlanParcels).ThenInclude(x => x.Parcel);
    }
}