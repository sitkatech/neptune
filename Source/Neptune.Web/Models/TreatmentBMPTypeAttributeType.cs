namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; AttributeType: {TreatmentBMPAttributeType?.TreatmentBMPAttributeTypeName ?? "Unknown"}";
    }
}