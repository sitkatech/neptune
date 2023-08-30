using System.Text.Json;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class CustomAttributeType : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"BMP Attribute: {CustomAttributeTypeName}";
        }

        public string GetMeasurementUnitDisplayName()
        {
            return MeasurementUnitType == null ? ViewUtilities.NoneString : MeasurementUnitType.LegendDisplayName;
        }

        public List<string> GetOptionsSchemaAsListOfString()
        {
            return CustomAttributeTypeOptionsSchema != null ? GeoJsonSerializer.Deserialize<List<string>>(CustomAttributeTypeOptionsSchema) : new List<string>();
        }

        public string DisplayNameWithUnits()
        {
            return
                $"{CustomAttributeTypeName} {(MeasurementUnitType != null ? $"({MeasurementUnitType.MeasurementUnitTypeDisplayName})" : string.Empty)}";
        }

        public bool IsCompleteForTreatmentBMP(TreatmentBMP treatmentBMP)
        {
            Check.Assert(
                treatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Select(x => x.CustomAttributeTypeID).Contains(CustomAttributeTypeID),
                "The Custom Attribute Type is not valid for this Treatment BMP");

            var customAttribute = treatmentBMP.CustomAttributes.SingleOrDefault(x => x.CustomAttributeTypeID == CustomAttributeTypeID);
            if (customAttribute == null)
            {
                return false;
            }
            return customAttribute.CustomAttributeValues.Any(x => !string.IsNullOrWhiteSpace(x.AttributeValue));
        }
    }
}