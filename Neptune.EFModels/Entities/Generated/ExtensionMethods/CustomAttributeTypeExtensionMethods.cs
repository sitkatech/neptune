//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeTypeExtensionMethods
    {
        public static CustomAttributeTypeDto AsDto(this CustomAttributeType customAttributeType)
        {
            var customAttributeTypeDto = new CustomAttributeTypeDto()
            {
                CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID,
                CustomAttributeTypeName = customAttributeType.CustomAttributeTypeName,
                CustomAttributeDataType = customAttributeType.CustomAttributeDataType.AsDto(),
                MeasurementUnitType = customAttributeType.MeasurementUnitType?.AsDto(),
                IsRequired = customAttributeType.IsRequired,
                CustomAttributeTypeDescription = customAttributeType.CustomAttributeTypeDescription,
                CustomAttributeTypePurpose = customAttributeType.CustomAttributeTypePurpose.AsDto(),
                CustomAttributeTypeOptionsSchema = customAttributeType.CustomAttributeTypeOptionsSchema
            };
            DoCustomMappings(customAttributeType, customAttributeTypeDto);
            return customAttributeTypeDto;
        }

        static partial void DoCustomMappings(CustomAttributeType customAttributeType, CustomAttributeTypeDto customAttributeTypeDto);

        public static CustomAttributeTypeSimpleDto AsSimpleDto(this CustomAttributeType customAttributeType)
        {
            var customAttributeTypeSimpleDto = new CustomAttributeTypeSimpleDto()
            {
                CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID,
                CustomAttributeTypeName = customAttributeType.CustomAttributeTypeName,
                CustomAttributeDataTypeID = customAttributeType.CustomAttributeDataTypeID,
                MeasurementUnitTypeID = customAttributeType.MeasurementUnitTypeID,
                IsRequired = customAttributeType.IsRequired,
                CustomAttributeTypeDescription = customAttributeType.CustomAttributeTypeDescription,
                CustomAttributeTypePurposeID = customAttributeType.CustomAttributeTypePurposeID,
                CustomAttributeTypeOptionsSchema = customAttributeType.CustomAttributeTypeOptionsSchema
            };
            DoCustomSimpleDtoMappings(customAttributeType, customAttributeTypeSimpleDto);
            return customAttributeTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomAttributeType customAttributeType, CustomAttributeTypeSimpleDto customAttributeTypeSimpleDto);
    }
}