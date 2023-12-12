//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SourceControlBMPExtensionMethods
    {

        public static SourceControlBMPSimpleDto AsSimpleDto(this SourceControlBMP sourceControlBMP)
        {
            var sourceControlBMPSimpleDto = new SourceControlBMPSimpleDto()
            {
                SourceControlBMPID = sourceControlBMP.SourceControlBMPID,
                WaterQualityManagementPlanID = sourceControlBMP.WaterQualityManagementPlanID,
                SourceControlBMPAttributeID = sourceControlBMP.SourceControlBMPAttributeID,
                IsPresent = sourceControlBMP.IsPresent,
                SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote
            };
            DoCustomSimpleDtoMappings(sourceControlBMP, sourceControlBMPSimpleDto);
            return sourceControlBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SourceControlBMP sourceControlBMP, SourceControlBMPSimpleDto sourceControlBMPSimpleDto);
    }
}