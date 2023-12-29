//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeTypeExtensionMethods
    {
        public static CustomAttributeTypeSimpleDto AsSimpleDto(this CustomAttributeType customAttributeType)
        {
            var dto = new CustomAttributeTypeSimpleDto()
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
            return dto;
        }
    }
}