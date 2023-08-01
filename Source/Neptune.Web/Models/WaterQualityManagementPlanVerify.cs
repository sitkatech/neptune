namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerify : IAuditableEntity
    {


        public string GetAuditDescriptionString()
        {
            return LastEditedDate.ToLongDateString();
        }
    }
}