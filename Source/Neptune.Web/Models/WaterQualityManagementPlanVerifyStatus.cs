namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyStatus : IAuditableEntity
    {



        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVerifyStatusName;
        }
    }
}