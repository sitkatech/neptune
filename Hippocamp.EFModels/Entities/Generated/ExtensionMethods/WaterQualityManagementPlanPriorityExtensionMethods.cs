//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPriorityExtensionMethods
    {
        public static WaterQualityManagementPlanPriorityDto AsDto(this WaterQualityManagementPlanPriority waterQualityManagementPlanPriority)
        {
            var waterQualityManagementPlanPriorityDto = new WaterQualityManagementPlanPriorityDto()
            {
                WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityID,
                WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityName,
                WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityDisplayName,
                SortOrder = waterQualityManagementPlanPriority.SortOrder
            };
            DoCustomMappings(waterQualityManagementPlanPriority, waterQualityManagementPlanPriorityDto);
            return waterQualityManagementPlanPriorityDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanPriority waterQualityManagementPlanPriority, WaterQualityManagementPlanPriorityDto waterQualityManagementPlanPriorityDto);

        public static WaterQualityManagementPlanPrioritySimpleDto AsSimpleDto(this WaterQualityManagementPlanPriority waterQualityManagementPlanPriority)
        {
            var waterQualityManagementPlanPrioritySimpleDto = new WaterQualityManagementPlanPrioritySimpleDto()
            {
                WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityID,
                WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityName,
                WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityDisplayName,
                SortOrder = waterQualityManagementPlanPriority.SortOrder
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanPriority, waterQualityManagementPlanPrioritySimpleDto);
            return waterQualityManagementPlanPrioritySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanPriority waterQualityManagementPlanPriority, WaterQualityManagementPlanPrioritySimpleDto waterQualityManagementPlanPrioritySimpleDto);
    }
}