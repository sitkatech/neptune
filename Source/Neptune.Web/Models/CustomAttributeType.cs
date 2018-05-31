using System.Collections.Generic;
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
    }
}