namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeCustomAttributeType : IAuditableEntity, IHaveASortOrder
    {
        public string AuditDescriptionString => $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; AttributeType: {CustomAttributeType?.CustomAttributeTypeName ?? "Unknown"}";
        public string DisplayName => CustomAttributeType.CustomAttributeTypeName;
        public int ID => TreatmentBMPTypeCustomAttributeTypeID;
    }
}