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
            var waterQualityManagementPlanVerifySourceControlBMPSimpleDto = new WaterQualityManagementPlanVerifySourceControlBMPSimpleDto()
            {
                WaterQualityManagementPlanVerifySourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerifySourceControlBMPID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerifyID,
                SourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMP.SourceControlBMPID,
                WaterQualityManagementPlanSourceControlCondition = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanSourceControlCondition
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifySourceControlBMP, waterQualityManagementPlanVerifySourceControlBMPSimpleDto);
            return waterQualityManagementPlanVerifySourceControlBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP, WaterQualityManagementPlanVerifySourceControlBMPSimpleDto waterQualityManagementPlanVerifySourceControlBMPSimpleDto);
    }
}