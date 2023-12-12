//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyQuickBMPExtensionMethods
    {

        public static WaterQualityManagementPlanVerifyQuickBMPSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
        {
            var waterQualityManagementPlanVerifyQuickBMPSimpleDto = new WaterQualityManagementPlanVerifyQuickBMPSimpleDto()
            {
                WaterQualityManagementPlanVerifyQuickBMPID = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyID,
                QuickBMPID = waterQualityManagementPlanVerifyQuickBMP.QuickBMPID,
                IsAdequate = waterQualityManagementPlanVerifyQuickBMP.IsAdequate,
                WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPNote
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifyQuickBMP, waterQualityManagementPlanVerifyQuickBMPSimpleDto);
            return waterQualityManagementPlanVerifyQuickBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP, WaterQualityManagementPlanVerifyQuickBMPSimpleDto waterQualityManagementPlanVerifyQuickBMPSimpleDto);
    }
}