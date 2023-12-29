using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanDocument
    {
        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.WaterQualityManagementPlanDocuments
                .Where(x => x.WaterQualityManagementPlanDocumentID == WaterQualityManagementPlanDocumentID)
                .ExecuteDeleteAsync();
        }
    }
}
