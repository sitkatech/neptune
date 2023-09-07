using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanDocument : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Water Quality Management Plan \"{WaterQualityManagementPlan?.WaterQualityManagementPlanName ?? "<Plan Not Found>"}\" Document \"{DisplayName}\"";
        }
    }
}
