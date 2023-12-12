//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyTypeExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyTypeSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType)
        {
            var dto = new WaterQualityManagementPlanVerifyTypeSimpleDto()
            {
                WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVerifyTypeName = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeName,
                WaterQualityManagementPlanVerifyTypeDisplayName = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeDisplayName
            };
            return dto;
        }
    }
}