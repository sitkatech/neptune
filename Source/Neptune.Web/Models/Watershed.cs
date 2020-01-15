namespace Neptune.Web.Models
{
    public partial class Watershed : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return WatershedName;
        }
    }
}