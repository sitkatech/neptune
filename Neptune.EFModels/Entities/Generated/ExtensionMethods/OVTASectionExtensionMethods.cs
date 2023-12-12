//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OVTASection]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OVTASectionExtensionMethods
    {
        public static OVTASectionSimpleDto AsSimpleDto(this OVTASection oVTASection)
        {
            var dto = new OVTASectionSimpleDto()
            {
                OVTASectionID = oVTASection.OVTASectionID,
                OVTASectionName = oVTASection.OVTASectionName,
                OVTASectionDisplayName = oVTASection.OVTASectionDisplayName,
                SectionHeader = oVTASection.SectionHeader,
                SortOrder = oVTASection.SortOrder,
                HasCompletionStatus = oVTASection.HasCompletionStatus
            };
            return dto;
        }
    }
}