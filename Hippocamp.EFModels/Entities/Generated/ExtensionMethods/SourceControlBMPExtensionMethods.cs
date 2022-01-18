//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class SourceControlBMPExtensionMethods
    {
        public static SourceControlBMPDto AsDto(this SourceControlBMP sourceControlBMP)
        {
            var sourceControlBMPDto = new SourceControlBMPDto()
            {
                SourceControlBMPID = sourceControlBMP.SourceControlBMPID,
                WaterQualityManagementPlan = sourceControlBMP.WaterQualityManagementPlan.AsDto(),
                SourceControlBMPAttribute = sourceControlBMP.SourceControlBMPAttribute.AsDto(),
                IsPresent = sourceControlBMP.IsPresent,
                SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote
            };
            DoCustomMappings(sourceControlBMP, sourceControlBMPDto);
            return sourceControlBMPDto;
        }

        static partial void DoCustomMappings(SourceControlBMP sourceControlBMP, SourceControlBMPDto sourceControlBMPDto);

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