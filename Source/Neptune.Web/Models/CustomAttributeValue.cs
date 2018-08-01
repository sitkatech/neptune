namespace Neptune.Web.Models
{
    public partial class CustomAttributeValue : IAuditableEntity
    {
        public string GetAuditDescriptionString() => "Custom Attribute Value deleted";
    }
}