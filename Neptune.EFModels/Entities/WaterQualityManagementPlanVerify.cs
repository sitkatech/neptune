using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanVerify : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return LastEditedDate.ToLongDateString();
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}