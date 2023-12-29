using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class QuickBMPExtensionMethods
{
    public static WaterQualityManagementPlanVerifyQuickBMPDto AsWaterQualityManagementPlanVerifyQuickBMPDto(
        this QuickBMP quickBMP)
    {
        var dto = new WaterQualityManagementPlanVerifyQuickBMPDto()
        {
            QuickBMPName = quickBMP.QuickBMPName,
            QuickBMPID = quickBMP.QuickBMPID,
            IsAdequate = null,
            WaterQualityManagementPlanVerifyQuickBMPNote = null,
            TreatmentBMPType = quickBMP.TreatmentBMPType.TreatmentBMPTypeName
        };
        return dto;
    }

    public static QuickBMPUpsertDto AsUpsertDto(
        this QuickBMP quickBMP)
    {
        var dto = new QuickBMPUpsertDto()
        {
            TreatmentBMPTypeID = quickBMP.TreatmentBMPTypeID,
            QuickBMPName = quickBMP.QuickBMPName,
            QuickBMPNote = quickBMP.QuickBMPNote,
            PercentOfSiteTreated = quickBMP.PercentOfSiteTreated,
            PercentCaptured = quickBMP.PercentCaptured,
            PercentRetained = quickBMP.PercentRetained,
            DryWeatherFlowOverrideID = quickBMP.DryWeatherFlowOverrideID
        };
        return dto;
    }
}