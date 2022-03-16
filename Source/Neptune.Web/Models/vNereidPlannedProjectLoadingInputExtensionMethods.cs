namespace Neptune.Web.Models
{
    public static class vNereidPlannedProjectLoadingInputExtensionMethods 
    { 
        public static vNereidLoadingInput tovNereidLoadingInput (this vNereidPlannedProjectLoadingInput vNereidPlannedProjectLoadingInput)
        {
            return new vNereidLoadingInput()
            {
                Area = vNereidPlannedProjectLoadingInput.Area,
                BaselineImperviousAcres = vNereidPlannedProjectLoadingInput.BaselineImperviousAcres,
                BaselineLandUseCode = vNereidPlannedProjectLoadingInput.BaselineLandUseCode,
                DelineationID = vNereidPlannedProjectLoadingInput.DelineationID,
                DelineationIsVerified = vNereidPlannedProjectLoadingInput.DelineationIsVerified,
                HydrologicSoilGroup = vNereidPlannedProjectLoadingInput.HydrologicSoilGroup,
                ImperviousAcres = vNereidPlannedProjectLoadingInput.ImperviousAcres,
                LandUseCode = vNereidPlannedProjectLoadingInput.LandUseCode,
                ModelBasinKey = vNereidPlannedProjectLoadingInput.ModelBasinKey,
                OCSurveyCatchmentID = vNereidPlannedProjectLoadingInput.OCSurveyCatchmentID,
                RegionalSubbasinID = vNereidPlannedProjectLoadingInput.RegionalSubbasinID,
                SlopePercentage = vNereidPlannedProjectLoadingInput.SlopePercentage,
                RelationallyAssociatedModelingApproach = vNereidPlannedProjectLoadingInput.RelationallyAssociatedModelingApproach,
                SpatiallyAssociatedModelingApproach = vNereidPlannedProjectLoadingInput.SpatiallyAssociatedModelingApproach,
                WaterQualityManagementPlanID = vNereidPlannedProjectLoadingInput.WaterQualityManagementPlanID
            };
        }
    }
}
