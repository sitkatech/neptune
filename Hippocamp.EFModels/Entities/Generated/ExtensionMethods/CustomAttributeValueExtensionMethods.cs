//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class CustomAttributeValueExtensionMethods
    {
        public static CustomAttributeValueDto AsDto(this CustomAttributeValue customAttributeValue)
        {
            var customAttributeValueDto = new CustomAttributeValueDto()
            {
                CustomAttributeValueID = customAttributeValue.CustomAttributeValueID,
                CustomAttribute = customAttributeValue.CustomAttribute.AsDto(),
                AttributeValue = customAttributeValue.AttributeValue
            };
            DoCustomMappings(customAttributeValue, customAttributeValueDto);
            return customAttributeValueDto;
        }

        static partial void DoCustomMappings(CustomAttributeValue customAttributeValue, CustomAttributeValueDto customAttributeValueDto);

        public static CustomAttributeValueSimpleDto AsSimpleDto(this CustomAttributeValue customAttributeValue)
        {
            var customAttributeValueSimpleDto = new CustomAttributeValueSimpleDto()
            {
                CustomAttributeValueID = customAttributeValue.CustomAttributeValueID,
                CustomAttributeID = customAttributeValue.CustomAttributeID,
                AttributeValue = customAttributeValue.AttributeValue
            };
            DoCustomSimpleDtoMappings(customAttributeValue, customAttributeValueSimpleDto);
            return customAttributeValueSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomAttributeValue customAttributeValue, CustomAttributeValueSimpleDto customAttributeValueSimpleDto);
    }
}