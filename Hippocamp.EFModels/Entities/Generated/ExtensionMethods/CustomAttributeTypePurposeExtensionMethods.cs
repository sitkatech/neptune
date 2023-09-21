//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeTypePurpose]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class CustomAttributeTypePurposeExtensionMethods
    {
        public static CustomAttributeTypePurposeDto AsDto(this CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            var customAttributeTypePurposeDto = new CustomAttributeTypePurposeDto()
            {
                CustomAttributeTypePurposeID = customAttributeTypePurpose.CustomAttributeTypePurposeID,
                CustomAttributeTypePurposeName = customAttributeTypePurpose.CustomAttributeTypePurposeName,
                CustomAttributeTypePurposeDisplayName = customAttributeTypePurpose.CustomAttributeTypePurposeDisplayName
            };
            DoCustomMappings(customAttributeTypePurpose, customAttributeTypePurposeDto);
            return customAttributeTypePurposeDto;
        }

        static partial void DoCustomMappings(CustomAttributeTypePurpose customAttributeTypePurpose, CustomAttributeTypePurposeDto customAttributeTypePurposeDto);

        public static CustomAttributeTypePurposeSimpleDto AsSimpleDto(this CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            var customAttributeTypePurposeSimpleDto = new CustomAttributeTypePurposeSimpleDto()
            {
                CustomAttributeTypePurposeID = customAttributeTypePurpose.CustomAttributeTypePurposeID,
                CustomAttributeTypePurposeName = customAttributeTypePurpose.CustomAttributeTypePurposeName,
                CustomAttributeTypePurposeDisplayName = customAttributeTypePurpose.CustomAttributeTypePurposeDisplayName
            };
            DoCustomSimpleDtoMappings(customAttributeTypePurpose, customAttributeTypePurposeSimpleDto);
            return customAttributeTypePurposeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomAttributeTypePurpose customAttributeTypePurpose, CustomAttributeTypePurposeSimpleDto customAttributeTypePurposeSimpleDto);
    }
}