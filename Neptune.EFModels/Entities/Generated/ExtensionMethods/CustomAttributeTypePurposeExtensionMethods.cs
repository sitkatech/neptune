//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeTypePurpose]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeTypePurposeExtensionMethods
    {

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