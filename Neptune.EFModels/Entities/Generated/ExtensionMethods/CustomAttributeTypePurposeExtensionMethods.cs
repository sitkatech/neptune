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
            var dto = new CustomAttributeTypePurposeSimpleDto()
            {
                CustomAttributeTypePurposeID = customAttributeTypePurpose.CustomAttributeTypePurposeID,
                CustomAttributeTypePurposeName = customAttributeTypePurpose.CustomAttributeTypePurposeName,
                CustomAttributeTypePurposeDisplayName = customAttributeTypePurpose.CustomAttributeTypePurposeDisplayName
            };
            return dto;
        }
    }
}