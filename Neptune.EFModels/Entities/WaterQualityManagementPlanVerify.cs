using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanVerify
    {
        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.WaterQualityManagementPlanVerifyPhotos
                .Where(x => x.WaterQualityManagementPlanVerifyID == WaterQualityManagementPlanVerifyID).ExecuteDeleteAsync();
            await dbContext.WaterQualityManagementPlanVerifyQuickBMPs
                .Where(x => x.WaterQualityManagementPlanVerifyID == WaterQualityManagementPlanVerifyID).ExecuteDeleteAsync();
            await dbContext.WaterQualityManagementPlanVerifySourceControlBMPs
                .Where(x => x.WaterQualityManagementPlanVerifyID == WaterQualityManagementPlanVerifyID).ExecuteDeleteAsync();
            await dbContext.WaterQualityManagementPlanVerifyTreatmentBMPs
                .Where(x => x.WaterQualityManagementPlanVerifyID == WaterQualityManagementPlanVerifyID).ExecuteDeleteAsync();
            await dbContext.WaterQualityManagementPlanVerifies
                .Where(x => x.WaterQualityManagementPlanVerifyID == WaterQualityManagementPlanVerifyID).ExecuteDeleteAsync();
        }
    }
}