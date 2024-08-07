//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeDataType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeDataTypeExtensionMethods
    {
        public static CustomAttributeDataTypeSimpleDto AsSimpleDto(this CustomAttributeDataType customAttributeDataType)
        {
            var dto = new CustomAttributeDataTypeSimpleDto()
            {
                CustomAttributeDataTypeID = customAttributeDataType.CustomAttributeDataTypeID,
                CustomAttributeDataTypeName = customAttributeDataType.CustomAttributeDataTypeName,
                CustomAttributeDataTypeDisplayName = customAttributeDataType.CustomAttributeDataTypeDisplayName,
                HasOptions = customAttributeDataType.HasOptions,
                HasMeasurementUnit = customAttributeDataType.HasMeasurementUnit
            };
            return dto;
        }
    }
}