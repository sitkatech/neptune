//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanStatusExtensionMethods
    {
        public static WaterQualityManagementPlanStatusDto AsDto(this WaterQualityManagementPlanStatus waterQualityManagementPlanStatus)
        {
            var waterQualityManagementPlanStatusDto = new WaterQualityManagementPlanStatusDto()
            {
                WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusID,
                WaterQualityManagementPlanStatusName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusName,
                WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusDisplayName,
                SortOrder = waterQualityManagementPlanStatus.SortOrder
            };
            DoCustomMappings(waterQualityManagementPlanStatus, waterQualityManagementPlanStatusDto);
            return waterQualityManagementPlanStatusDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanStatus waterQualityManagementPlanStatus, WaterQualityManagementPlanStatusDto waterQualityManagementPlanStatusDto);

        public static WaterQualityManagementPlanStatusSimpleDto AsSimpleDto(this WaterQualityManagementPlanStatus waterQualityManagementPlanStatus)
        {
            var waterQualityManagementPlanStatusSimpleDto = new WaterQualityManagementPlanStatusSimpleDto()
            {
                WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusID,
                WaterQualityManagementPlanStatusName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusName,
                WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusDisplayName,
                SortOrder = waterQualityManagementPlanStatus.SortOrder
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanStatus, waterQualityManagementPlanStatusSimpleDto);
            return waterQualityManagementPlanStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanStatus waterQualityManagementPlanStatus, WaterQualityManagementPlanStatusSimpleDto waterQualityManagementPlanStatusSimpleDto);
    }
}