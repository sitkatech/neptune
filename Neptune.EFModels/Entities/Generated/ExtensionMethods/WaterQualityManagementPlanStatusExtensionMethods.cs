//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanStatusExtensionMethods
    {
        public static WaterQualityManagementPlanStatusSimpleDto AsSimpleDto(this WaterQualityManagementPlanStatus waterQualityManagementPlanStatus)
        {
            var dto = new WaterQualityManagementPlanStatusSimpleDto()
            {
                WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusID,
                WaterQualityManagementPlanStatusName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusName,
                WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusDisplayName
            };
            return dto;
        }
    }
}