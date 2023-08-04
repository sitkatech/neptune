//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVisitStatusExtensionMethods
    {
        public static WaterQualityManagementPlanVisitStatusDto AsDto(this WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus)
        {
            var waterQualityManagementPlanVisitStatusDto = new WaterQualityManagementPlanVisitStatusDto()
            {
                WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusID,
                WaterQualityManagementPlanVisitStatusName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusName,
                WaterQualityManagementPlanVisitStatusDisplayName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusDisplayName
            };
            DoCustomMappings(waterQualityManagementPlanVisitStatus, waterQualityManagementPlanVisitStatusDto);
            return waterQualityManagementPlanVisitStatusDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus, WaterQualityManagementPlanVisitStatusDto waterQualityManagementPlanVisitStatusDto);

        public static WaterQualityManagementPlanVisitStatusSimpleDto AsSimpleDto(this WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus)
        {
            var waterQualityManagementPlanVisitStatusSimpleDto = new WaterQualityManagementPlanVisitStatusSimpleDto()
            {
                WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusID,
                WaterQualityManagementPlanVisitStatusName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusName,
                WaterQualityManagementPlanVisitStatusDisplayName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusDisplayName
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVisitStatus, waterQualityManagementPlanVisitStatusSimpleDto);
            return waterQualityManagementPlanVisitStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus, WaterQualityManagementPlanVisitStatusSimpleDto waterQualityManagementPlanVisitStatusSimpleDto);
    }
}