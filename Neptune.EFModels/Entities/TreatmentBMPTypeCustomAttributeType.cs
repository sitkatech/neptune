namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPTypeCustomAttributeType : IHaveASortOrder
    {
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