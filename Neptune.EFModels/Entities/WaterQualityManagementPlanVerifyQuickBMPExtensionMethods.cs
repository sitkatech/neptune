using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class WaterQualityManagementPlanVerifyQuickBMPExtensionMethods
{
    public static WaterQualityManagementPlanVerifyQuickBMPDto AsDto(this WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
    {
        var dto = new WaterQualityManagementPlanVerifyQuickBMPDto()
        {
            WaterQualityManagementPlanVerifyQuickBMPID = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPID,
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyID,
            QuickBMPID = waterQualityManagementPlanVerifyQuickBMP.QuickBMPID,
            IsAdequate = waterQualityManagementPlanVerifyQuickBMP.IsAdequate,
            WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPNote,
            QuickBMPName = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.QuickBMPName,
            TreatmentBMPType = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.TreatmentBMPType.TreatmentBMPTypeName
        };
        return dto;
    }
}