using LtInfo.Common.Views;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"BMP Attribute: {TreatmentBMPAttributeTypeName}";

        public string GetMeasurementUnitDisplayName()
        {
            return MeasurementUnitType == null ? ViewUtilities.NoneString : MeasurementUnitType.LegendDisplayName;
        }
    }
}