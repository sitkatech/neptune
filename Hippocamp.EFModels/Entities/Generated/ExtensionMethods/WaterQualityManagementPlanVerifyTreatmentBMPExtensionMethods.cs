//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyTreatmentBMPExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyTreatmentBMPDto AsDto(this WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            var waterQualityManagementPlanVerifyTreatmentBMPDto = new WaterQualityManagementPlanVerifyTreatmentBMPDto()
            {
                WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPID,
                WaterQualityManagementPlanVerify = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerify.AsDto(),
                TreatmentBMP = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.AsDto(),
                IsAdequate = waterQualityManagementPlanVerifyTreatmentBMP.IsAdequate,
                WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPNote
            };
            DoCustomMappings(waterQualityManagementPlanVerifyTreatmentBMP, waterQualityManagementPlanVerifyTreatmentBMPDto);
            return waterQualityManagementPlanVerifyTreatmentBMPDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP, WaterQualityManagementPlanVerifyTreatmentBMPDto waterQualityManagementPlanVerifyTreatmentBMPDto);

        public static WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            var waterQualityManagementPlanVerifyTreatmentBMPSimpleDto = new WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()
            {
                WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyID,
                TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMPID,
                IsAdequate = waterQualityManagementPlanVerifyTreatmentBMP.IsAdequate,
                WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPNote
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifyTreatmentBMP, waterQualityManagementPlanVerifyTreatmentBMPSimpleDto);
            return waterQualityManagementPlanVerifyTreatmentBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP, WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto waterQualityManagementPlanVerifyTreatmentBMPSimpleDto);
    }
}