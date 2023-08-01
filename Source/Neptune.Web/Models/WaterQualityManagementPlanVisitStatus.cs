namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVisitStatus : IAuditableEntity
    {



        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVisitStatusName;
        }
    }
}