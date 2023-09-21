//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifySourceControlBMPExtensionMethods
    {
        public static WaterQualityManagementPlanVerifySourceControlBMPDto AsDto(this WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP)
        {
            var waterQualityManagementPlanVerifySourceControlBMPDto = new WaterQualityManagementPlanVerifySourceControlBMPDto()
            {
                WaterQualityManagementPlanVerifySourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerifySourceControlBMPID,
                WaterQualityManagementPlanVerify = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerify.AsDto(),
                SourceControlBMP = waterQualityManagementPlanVerifySourceControlBMP.SourceControlBMP.AsDto(),
                WaterQualityManagementPlanSourceControlCondition = waterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanSourceControlCondition
            };
            DoCustomMappings(waterQualityManagementPlanVerifySourceControlBMP, waterQualityManagementPlanVerifySourceControlBMPDto);
            return waterQualityManagementPlanVerifySourceControlBMPDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP, WaterQualityManagementPlanVerifySourceControlBMPDto waterQualityManagementPlanVerifySourceControlBMPDto);

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