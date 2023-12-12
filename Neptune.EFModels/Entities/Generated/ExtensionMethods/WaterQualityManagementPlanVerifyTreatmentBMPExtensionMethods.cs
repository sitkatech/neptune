//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyTreatmentBMPExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            var dto = new WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()
            {
                WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyID,
                TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMPID,
                IsAdequate = waterQualityManagementPlanVerifyTreatmentBMP.IsAdequate,
                WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPNote
            };
            return dto;
        }
    }
}