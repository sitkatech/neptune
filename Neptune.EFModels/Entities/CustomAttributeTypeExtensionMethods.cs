using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class CustomAttributeTypeExtensionMethods
{
    public static CustomAttributeTypeDto AsDto(this CustomAttributeType customAttributeType)
    {
        var dto = new CustomAttributeTypeDto
        {
            CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID,
            CustomAttributeTypeName = customAttributeType.CustomAttributeTypeName,
            CustomAttributeDataTypeID = customAttributeType.CustomAttributeDataTypeID,
            MeasurementUnitTypeID = customAttributeType.MeasurementUnitTypeID,
            IsRequired = customAttributeType.IsRequired,
            CustomAttributeTypeDescription = customAttributeType.CustomAttributeTypeDescription,
            CustomAttributeTypePurposeID = customAttributeType.CustomAttributeTypePurposeID,
            CustomAttributeTypeOptionsSchema = customAttributeType.CustomAttributeTypeOptionsSchema,
            DataTypeDisplayName = customAttributeType.CustomAttributeDataType.CustomAttributeDataTypeDisplayName,
            MeasurementUnitDisplayName = customAttributeType.GetMeasurementUnitDisplayName(),
            Purpose = customAttributeType.CustomAttributeTypePurpose
                .CustomAttributeTypePurposeDisplayName
        };
        return dto;
    }
}