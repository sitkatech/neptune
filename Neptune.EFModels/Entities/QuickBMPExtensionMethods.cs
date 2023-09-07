using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class QuickBMPExtensionMethods
{
    public static WaterQualityManagementPlanVerifyQuickBMPSimpleDto AsWaterQualityManagementPlanVerifyQuickBMPSimpleDto(
        this QuickBMP quickBMP)
    {
        var waterQualityManagementPlanVerifyQuickBMPSimpleDto = new WaterQualityManagementPlanVerifyQuickBMPSimpleDto()
        {
            QuickBMPName = quickBMP.QuickBMPName,
            QuickBMPID = quickBMP.QuickBMPID,
            IsAdequate = null,
            WaterQualityManagementPlanVerifyQuickBMPNote = null,
            TreatmentBMPType = quickBMP.TreatmentBMPType.TreatmentBMPTypeName
        };
        return waterQualityManagementPlanVerifyQuickBMPSimpleDto;
    }
}