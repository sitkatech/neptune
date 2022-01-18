//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class SourceControlBMPAttributeCategoryExtensionMethods
    {
        public static SourceControlBMPAttributeCategoryDto AsDto(this SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory)
        {
            var sourceControlBMPAttributeCategoryDto = new SourceControlBMPAttributeCategoryDto()
            {
                SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryID,
                SourceControlBMPAttributeCategoryShortName = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryShortName,
                SourceControlBMPAttributeCategoryName = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName
            };
            DoCustomMappings(sourceControlBMPAttributeCategory, sourceControlBMPAttributeCategoryDto);
            return sourceControlBMPAttributeCategoryDto;
        }

        static partial void DoCustomMappings(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory, SourceControlBMPAttributeCategoryDto sourceControlBMPAttributeCategoryDto);

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