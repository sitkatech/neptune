//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifySourceControlBMPExtensionMethods
    {
        public static WaterQualityManagementPlanVerifySourceControlBMPSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP)
        {
            var dto = new WaterQualityManagementPlanVerifySourceControlBMPSimpleDto()
            {
                WaterQualityManagementPlanVerifySourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerifySourceControlBMPID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerifyID,
                SourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMP.SourceControlBMPID,
                WaterQualityManagementPlanSourceControlCondition = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanSourceControlCondition
            };
            return dto;
        }
    }
}