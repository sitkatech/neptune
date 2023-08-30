using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class CustomAttributeTypeExtensionMethods
{
    static partial void DoCustomSimpleDtoMappings(CustomAttributeType customAttributeType,
        CustomAttributeTypeSimpleDto customAttributeTypeSimpleDto)
    {
        customAttributeTypeSimpleDto.DataTypeDisplayName = customAttributeType.CustomAttributeDataType.CustomAttributeDataTypeDisplayName;
        customAttributeTypeSimpleDto.MeasurementUnitDisplayName = customAttributeType.GetMeasurementUnitDisplayName();
        customAttributeTypeSimpleDto.Purpose = customAttributeType.CustomAttributeTypePurpose
            .CustomAttributeTypePurposeDisplayName;
    }
}