using Neptune.Models.DataTransferObjects;
using System.Reflection.Metadata;
using System.Linq;

namespace Neptune.EFModels.Entities;

public static partial class CustomAttributeExtensionMethods
{
    public static CustomAttributeUpsertDto AsUpsertDto(this CustomAttribute customAttribute)
    {
        var customAttributeUpsertDto = new CustomAttributeUpsertDto(
            customAttribute.TreatmentBMPTypeCustomAttributeTypeID, customAttribute.CustomAttributeTypeID,
            customAttribute.CustomAttributeValues.Select(x => x.AttributeValue).ToList(), 
            customAttribute.CustomAttributeID, customAttribute.TreatmentBMPID, customAttribute.TreatmentBMPTypeID);
        return customAttributeUpsertDto;
    }

    public static CustomAttributeDto AsDto(this CustomAttribute customAttribute)
    {
        var dto = new CustomAttributeDto
        {
            CustomAttributeID = customAttribute.CustomAttributeID,
            TreatmentBMPID = customAttribute.TreatmentBMPID,
            TreatmentBMPTypeCustomAttributeTypeID = customAttribute.TreatmentBMPTypeCustomAttributeTypeID,
            TreatmentBMPTypeID = customAttribute.TreatmentBMPTypeID,
            CustomAttributeTypeID = customAttribute.CustomAttributeTypeID,
            CustomAttributeValueWithUnits = GetCustomAttributeValueWithUnits(customAttribute)
        };

        return dto;
    }

    private static string GetCustomAttributeValueWithUnits(this CustomAttribute customAttribute)
    {
        var measurementUnit = "";
        var customAttributeType = customAttribute.CustomAttributeType;
        if (customAttributeType.MeasurementUnitTypeID.HasValue)
        {
            measurementUnit = $" {customAttributeType.MeasurementUnitType.LegendDisplayName}";
        }

        var value = string.Join(", ", customAttribute.CustomAttributeValues.OrderBy(x => x.AttributeValue).Select(x => x.AttributeValue));

        return $"{value}{measurementUnit}";
    }
}