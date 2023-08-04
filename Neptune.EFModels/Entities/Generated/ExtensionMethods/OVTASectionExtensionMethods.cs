//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OVTASection]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OVTASectionExtensionMethods
    {
        public static OVTASectionDto AsDto(this OVTASection oVTASection)
        {
            var oVTASectionDto = new OVTASectionDto()
            {
                OVTASectionID = oVTASection.OVTASectionID,
                OVTASectionName = oVTASection.OVTASectionName,
                OVTASectionDisplayName = oVTASection.OVTASectionDisplayName,
                SectionHeader = oVTASection.SectionHeader,
                SortOrder = oVTASection.SortOrder,
                HasCompletionStatus = oVTASection.HasCompletionStatus
            };
            DoCustomMappings(oVTASection, oVTASectionDto);
            return oVTASectionDto;
        }

        static partial void DoCustomMappings(OVTASection oVTASection, OVTASectionDto oVTASectionDto);

        public static OVTASectionSimpleDto AsSimpleDto(this OVTASection oVTASection)
        {
            var oVTASectionSimpleDto = new OVTASectionSimpleDto()
            {
                OVTASectionID = oVTASection.OVTASectionID,
                OVTASectionName = oVTASection.OVTASectionName,
                OVTASectionDisplayName = oVTASection.OVTASectionDisplayName,
                SectionHeader = oVTASection.SectionHeader,
                SortOrder = oVTASection.SortOrder,
                HasCompletionStatus = oVTASection.HasCompletionStatus
            };
            DoCustomSimpleDtoMappings(oVTASection, oVTASectionSimpleDto);
            return oVTASectionSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OVTASection oVTASection, OVTASectionSimpleDto oVTASectionSimpleDto);
    }
}