using Neptune.Common;
using Neptune.Common.GeoSpatial;

namespace Neptune.EFModels.Entities
{
    public partial class CustomAttributeType
    {
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

        public bool IsCompleteForTreatmentBMP(List<CustomAttribute> customAttributes)
        {
            var customAttribute = customAttributes.SingleOrDefault(x => x.CustomAttributeTypeID == CustomAttributeTypeID);
            if (customAttribute == null)
            {
                return false;
            }
            return customAttribute.CustomAttributeValues.Any(x => !string.IsNullOrWhiteSpace(x.AttributeValue));
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            //todo: deletefull
            throw new NotImplementedException("Deleting of Custom Attribute Type not implemented yet!");
        }
    }
}