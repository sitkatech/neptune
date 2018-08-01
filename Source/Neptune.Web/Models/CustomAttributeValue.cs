namespace Neptune.Web.Models
{
    public partial class CustomAttributeValue : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return "Custom Attribute Value deleted";
        }
    }
}