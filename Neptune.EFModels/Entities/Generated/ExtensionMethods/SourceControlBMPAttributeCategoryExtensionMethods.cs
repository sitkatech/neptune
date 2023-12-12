//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SourceControlBMPAttributeCategoryExtensionMethods
    {

        public static SourceControlBMPAttributeCategorySimpleDto AsSimpleDto(this SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory)
        {
            var sourceControlBMPAttributeCategorySimpleDto = new SourceControlBMPAttributeCategorySimpleDto()
            {
                SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryID,
                SourceControlBMPAttributeCategoryShortName = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryShortName,
                SourceControlBMPAttributeCategoryName = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName
            };
            DoCustomSimpleDtoMappings(sourceControlBMPAttributeCategory, sourceControlBMPAttributeCategorySimpleDto);
            return sourceControlBMPAttributeCategorySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory, SourceControlBMPAttributeCategorySimpleDto sourceControlBMPAttributeCategorySimpleDto);
    }
}