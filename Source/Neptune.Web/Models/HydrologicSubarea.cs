namespace Neptune.Web.Models
{
    public partial class HydrologicSubarea : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return HydrologicSubareaName;
        }
    }
}