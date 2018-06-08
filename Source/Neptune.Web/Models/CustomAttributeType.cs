using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Views;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class CustomAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"BMP Attribute: {CustomAttributeTypeName}";

        public string GetMeasurementUnitDisplayName()
        {
            return MeasurementUnitType == null ? ViewUtilities.NoneString : MeasurementUnitType.LegendDisplayName;
        }

        public List<string> GetOptionsSchemaAsListOfString()
        {
            return CustomAttributeTypeOptionsSchema != null ? JsonConvert.DeserializeObject<List<string>>(CustomAttributeTypeOptionsSchema) : new List<string>();
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

            return customAttribute?.CustomAttributeValues?.Any(x => !string.IsNullOrWhiteSpace(x.AttributeValue)) ??
                   false;
        }
    }
}