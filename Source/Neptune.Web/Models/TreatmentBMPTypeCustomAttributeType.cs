namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeCustomAttributeType : IAuditableEntity, IHaveASortOrder
    {
        public string GetAuditDescriptionString() =>
            $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; AttributeType: {CustomAttributeType?.CustomAttributeTypeName ?? "Unknown"}";

        public string GetDisplayName() => CustomAttributeType.CustomAttributeTypeName;
        public int GetID() => TreatmentBMPTypeCustomAttributeTypeID;
    }
}