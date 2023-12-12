//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanModelingApproach]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanModelingApproachExtensionMethods
    {
        public static WaterQualityManagementPlanModelingApproachSimpleDto AsSimpleDto(this WaterQualityManagementPlanModelingApproach waterQualityManagementPlanModelingApproach)
        {
            var dto = new WaterQualityManagementPlanModelingApproachSimpleDto()
            {
                WaterQualityManagementPlanModelingApproachID = waterQualityManagementPlanModelingApproach.WaterQualityManagementPlanModelingApproachID,
                WaterQualityManagementPlanModelingApproachName = waterQualityManagementPlanModelingApproach.WaterQualityManagementPlanModelingApproachName,
                WaterQualityManagementPlanModelingApproachDisplayName = waterQualityManagementPlanModelingApproach.WaterQualityManagementPlanModelingApproachDisplayName,
                WaterQualityManagementPlanModelingApproachDescription = waterQualityManagementPlanModelingApproach.WaterQualityManagementPlanModelingApproachDescription
            };
            return dto;
        }
    }
}