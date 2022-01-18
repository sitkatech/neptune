//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class SourceControlBMPAttributeExtensionMethods
    {
        public static SourceControlBMPAttributeDto AsDto(this SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            var sourceControlBMPAttributeDto = new SourceControlBMPAttributeDto()
            {
                SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID,
                SourceControlBMPAttributeCategory = sourceControlBMPAttribute.SourceControlBMPAttributeCategory.AsDto(),
                SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName
            };
            DoCustomMappings(sourceControlBMPAttribute, sourceControlBMPAttributeDto);
            return sourceControlBMPAttributeDto;
        }

        static partial void DoCustomMappings(SourceControlBMPAttribute sourceControlBMPAttribute, SourceControlBMPAttributeDto sourceControlBMPAttributeDto);

        public static SourceControlBMPAttributeSimpleDto AsSimpleDto(this SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            var sourceControlBMPAttributeSimpleDto = new SourceControlBMPAttributeSimpleDto()
            {
                SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID,
                SourceControlBMPAttributeCategoryID = sourceControlBMPAttribute.SourceControlBMPAttributeCategoryID,
                SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName
            };
            DoCustomSimpleDtoMappings(sourceControlBMPAttribute, sourceControlBMPAttributeSimpleDto);
            return sourceControlBMPAttributeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SourceControlBMPAttribute sourceControlBMPAttribute, SourceControlBMPAttributeSimpleDto sourceControlBMPAttributeSimpleDto);
    }
}