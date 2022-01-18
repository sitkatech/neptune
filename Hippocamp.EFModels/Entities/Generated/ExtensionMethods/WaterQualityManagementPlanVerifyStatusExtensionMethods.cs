//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyStatusExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyStatusDto AsDto(this WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus)
        {
            var waterQualityManagementPlanVerifyStatusDto = new WaterQualityManagementPlanVerifyStatusDto()
            {
                WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusID,
                WaterQualityManagementPlanVerifyStatusName = waterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusName
            };
            DoCustomMappings(waterQualityManagementPlanVerifyStatus, waterQualityManagementPlanVerifyStatusDto);
            return waterQualityManagementPlanVerifyStatusDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus, WaterQualityManagementPlanVerifyStatusDto waterQualityManagementPlanVerifyStatusDto);

        public static WaterQualityManagementPlanVerifyStatusSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus)
        {
            var waterQualityManagementPlanVerifyStatusSimpleDto = new WaterQualityManagementPlanVerifyStatusSimpleDto()
            {
                WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusID,
                WaterQualityManagementPlanVerifyStatusName = waterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusName
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifyStatus, waterQualityManagementPlanVerifyStatusSimpleDto);
            return waterQualityManagementPlanVerifyStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus, WaterQualityManagementPlanVerifyStatusSimpleDto waterQualityManagementPlanVerifyStatusSimpleDto);
    }
}