using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanDocument
    {
        public void DeleteFull(NeptuneDbContext dbContext)
        {
            dbContext.WaterQualityManagementPlanDocuments
                .Where(x => x.WaterQualityManagementPlanDocumentID == WaterQualityManagementPlanDocumentID)
                .ExecuteDelete();
        }
    }
}
