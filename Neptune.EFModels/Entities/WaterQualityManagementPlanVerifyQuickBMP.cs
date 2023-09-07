using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanVerifyQuickBMP : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVerifyQuickBMPID.ToString();
        }
    }

}
