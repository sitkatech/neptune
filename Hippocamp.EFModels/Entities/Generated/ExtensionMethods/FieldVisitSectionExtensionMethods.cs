//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitSection]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldVisitSectionExtensionMethods
    {
        public static FieldVisitSectionDto AsDto(this FieldVisitSection fieldVisitSection)
        {
            var fieldVisitSectionDto = new FieldVisitSectionDto()
            {
                FieldVisitSectionID = fieldVisitSection.FieldVisitSectionID,
                FieldVisitSectionName = fieldVisitSection.FieldVisitSectionName,
                FieldVisitSectionDisplayName = fieldVisitSection.FieldVisitSectionDisplayName,
                SectionHeader = fieldVisitSection.SectionHeader,
                SortOrder = fieldVisitSection.SortOrder
            };
            DoCustomMappings(fieldVisitSection, fieldVisitSectionDto);
            return fieldVisitSectionDto;
        }

        static partial void DoCustomMappings(FieldVisitSection fieldVisitSection, FieldVisitSectionDto fieldVisitSectionDto);

        public static FieldVisitSectionSimpleDto AsSimpleDto(this FieldVisitSection fieldVisitSection)
        {
            var fieldVisitSectionSimpleDto = new FieldVisitSectionSimpleDto()
            {
                FieldVisitSectionID = fieldVisitSection.FieldVisitSectionID,
                FieldVisitSectionName = fieldVisitSection.FieldVisitSectionName,
                FieldVisitSectionDisplayName = fieldVisitSection.FieldVisitSectionDisplayName,
                SectionHeader = fieldVisitSection.SectionHeader,
                SortOrder = fieldVisitSection.SortOrder
            };
            DoCustomSimpleDtoMappings(fieldVisitSection, fieldVisitSectionSimpleDto);
            return fieldVisitSectionSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldVisitSection fieldVisitSection, FieldVisitSectionSimpleDto fieldVisitSectionSimpleDto);
    }
}