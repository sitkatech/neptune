using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
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

        public string GetDisplayNameWithUnits()
        {
            if (!CustomAttributeType.MeasurementUnitTypeID.HasValue)
            {
                return CustomAttributeType.CustomAttributeTypeName;
            }

            return
                $"{CustomAttributeType.CustomAttributeTypeName} ({CustomAttributeType.GetMeasurementUnitDisplayName()})";
        }

        public int GetID()
        {
            return TreatmentBMPTypeCustomAttributeTypeID;
        }
    }
}