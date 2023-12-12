//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPriorityExtensionMethods
    {
        public static WaterQualityManagementPlanPrioritySimpleDto AsSimpleDto(this WaterQualityManagementPlanPriority waterQualityManagementPlanPriority)
        {
            var dto = new WaterQualityManagementPlanPrioritySimpleDto()
            {
                WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityID,
                WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityName,
                WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityDisplayName
            };
            return dto;
        }
    }
}