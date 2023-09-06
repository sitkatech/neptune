using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanVerifyTreatmentBMP : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return  $"Treatment BMP Name: {TreatmentBMP?.TreatmentBMPName};  WaterQualityManagementPlanName: {WaterQualityManagementPlanVerify?.WaterQualityManagementPlan?.WaterQualityManagementPlanName}";
        }
    }

}