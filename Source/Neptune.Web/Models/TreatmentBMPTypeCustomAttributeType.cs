namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeCustomAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; AttributeType: {CustomAttributeType?.CustomAttributeTypeName ?? "Unknown"}";
    }
}