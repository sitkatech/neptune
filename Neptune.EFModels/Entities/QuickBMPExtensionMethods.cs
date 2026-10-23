using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class QuickBMPExtensionMethods
{
    public static QuickBMPGridDto AsGridDto(this QuickBMP quickBMP, string? treatmentBMPTypeName, vWaterQualityManagementPlanDetailed waterQualityManagementPlan)
    {
        var calculatedBoundaryAcreage = GetCalculatedBoundaryAcreage(waterQualityManagementPlan.RecordedWQMPAreaInAcres, waterQualityManagementPlan.CalculatedWQMPAcreage);
        return new QuickBMPGridDto
        {
            QuickBMPID = quickBMP.QuickBMPID,
            QuickBMPName = quickBMP.QuickBMPName,
            TreatmentBMPTypeID = quickBMP.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPTypeName,
            NumberOfIndividualBMPs = quickBMP.NumberOfIndividualBMPs,
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
            WaterQualityManagementPlanName = waterQualityManagementPlan.WaterQualityManagementPlanName,
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID,
            StormwaterJurisdictionName = waterQualityManagementPlan.StormwaterJurisdictionName,
            WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlan.WaterQualityManagementPlanStatusDisplayName,
            HydrologicSubareaName = waterQualityManagementPlan.HydrologicSubareaName,
            PercentOfSiteTreated = quickBMP.PercentOfSiteTreated,
            CalculatedBoundaryAcreage = calculatedBoundaryAcreage,
            DrainageAreaTreatedAcres = GetDrainageAreaTreatedAcres(quickBMP.PercentOfSiteTreated, calculatedBoundaryAcreage),
            PercentCaptured = quickBMP.PercentCaptured,
            PercentRetained = quickBMP.PercentRetained,
            DryWeatherFlowOverrideID = quickBMP.DryWeatherFlowOverrideID,
            DryWeatherFlowOverrideDisplayName = quickBMP.DryWeatherFlowOverrideID.HasValue && DryWeatherFlowOverride.AllLookupDictionary.TryGetValue(quickBMP.DryWeatherFlowOverrideID.Value, out var dryWeatherFlowOverride)
                ? dryWeatherFlowOverride.DryWeatherFlowOverrideDisplayName
                : null,
            QuickBMPNote = quickBMP.QuickBMPNote,
        };
    }

    /// <summary>
    /// The recorded WQMP area, falling back to the boundary's area. The view reports a missing
    /// boundary as 0 acres, so 0 is treated as blank rather than a real area.
    /// </summary>
    public static decimal? GetCalculatedBoundaryAcreage(decimal? recordedWQMPAreaInAcres, double? calculatedWQMPAcreage)
    {
        if (recordedWQMPAreaInAcres.HasValue)
        {
            return recordedWQMPAreaInAcres;
        }

        return calculatedWQMPAcreage is > 0 ? (decimal)calculatedWQMPAcreage.Value : null;
    }

    public static decimal? GetDrainageAreaTreatedAcres(decimal? percentOfSiteTreated, decimal? calculatedBoundaryAcreage)
    {
        if (!percentOfSiteTreated.HasValue || !calculatedBoundaryAcreage.HasValue)
        {
            return null;
        }

        return Math.Round(percentOfSiteTreated.Value / 100m * calculatedBoundaryAcreage.Value, 2, MidpointRounding.AwayFromZero);
    }
}
