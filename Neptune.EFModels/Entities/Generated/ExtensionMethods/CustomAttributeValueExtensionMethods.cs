//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeValueExtensionMethods
    {

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