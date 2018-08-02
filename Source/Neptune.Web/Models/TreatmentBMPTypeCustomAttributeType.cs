namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeCustomAttributeType : IAuditableEntity, IHaveASortOrder
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; AttributeType: {CustomAttributeType?.CustomAttributeTypeName ?? "Unknown"}";
        }

        public string GetDisplayName()
        {
            return CustomAttributeType.CustomAttributeTypeName;
        }

        public int GetID()
        {
            return TreatmentBMPTypeCustomAttributeTypeID;
        }
    }
}