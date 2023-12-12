//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitSection]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldVisitSectionExtensionMethods
    {

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