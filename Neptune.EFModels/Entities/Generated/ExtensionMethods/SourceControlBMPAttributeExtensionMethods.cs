//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SourceControlBMPAttributeExtensionMethods
    {
        public static SourceControlBMPAttributeSimpleDto AsSimpleDto(this SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            var dto = new SourceControlBMPAttributeSimpleDto()
            {
                SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID,
                SourceControlBMPAttributeCategoryID = sourceControlBMPAttribute.SourceControlBMPAttributeCategoryID,
                SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName
            };
            return dto;
        }
    }
}