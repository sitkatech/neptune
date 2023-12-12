//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanDevelopmentTypeExtensionMethods
    {
        public static WaterQualityManagementPlanDevelopmentTypeSimpleDto AsSimpleDto(this WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            var dto = new WaterQualityManagementPlanDevelopmentTypeSimpleDto()
            {
                WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeID,
                WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeName,
                WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeDisplayName
            };
            return dto;
        }
    }
}