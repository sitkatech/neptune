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

    public static QuickBMPUpsertDto AsUpsertDto(
        this QuickBMP quickBMP)
    {
        var quickBMPUpsertDto = new QuickBMPUpsertDto()
        {
            TreatmentBMPTypeID = quickBMP.TreatmentBMPTypeID,
            QuickBMPName = quickBMP.QuickBMPName,
            QuickBMPNote = quickBMP.QuickBMPNote,
            PercentOfSiteTreated = quickBMP.PercentOfSiteTreated,
            PercentCaptured = quickBMP.PercentCaptured,
            PercentRetained = quickBMP.PercentRetained,
            DryWeatherFlowOverrideID = quickBMP.DryWeatherFlowOverrideID
        };
        return quickBMPUpsertDto;
    }
}