using System.Collections.Generic;
using LtInfo.Common.Views;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAttributeType : IAuditableEntity
    {
        public string AuditDescriptionString => $"BMP Attribute: {TreatmentBMPAttributeTypeName}";

        public string GetMeasurementUnitDisplayName()
        {
            return MeasurementUnitType == null ? ViewUtilities.NoneString : MeasurementUnitType.LegendDisplayName;
        }

        public List<string> GetOptionsSchemaAsListOfString()
        {
            return TreatmentBMPAttributeTypeOptionsSchema != null ? JsonConvert.DeserializeObject<List<string>>(TreatmentBMPAttributeTypeOptionsSchema) : new List<string>();
        }
    }
}