namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"BMP Attribute: {TreatmentBMPAttributeTypeName}";
    }
}