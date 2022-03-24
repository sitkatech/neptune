namespace Neptune.Web.Models
{
    public static class vNereidProjectLoadingInputExtensionMethods 
    { 
        public static vNereidLoadingInput tovNereidLoadingInput (this vNereidProjectLoadingInput vNereidProjectLoadingInput)
        {
            return new vNereidLoadingInput()
            {
                Area = vNereidProjectLoadingInput.Area,
                BaselineImperviousAcres = vNereidProjectLoadingInput.BaselineImperviousAcres,
                BaselineLandUseCode = vNereidProjectLoadingInput.BaselineLandUseCode,
                DelineationID = vNereidProjectLoadingInput.DelineationID,
                DelineationIsVerified = vNereidProjectLoadingInput.DelineationIsVerified,
                HydrologicSoilGroup = vNereidProjectLoadingInput.HydrologicSoilGroup,
                ImperviousAcres = vNereidProjectLoadingInput.ImperviousAcres,
                LandUseCode = vNereidProjectLoadingInput.LandUseCode,
                ModelBasinKey = vNereidProjectLoadingInput.ModelBasinKey,
                OCSurveyCatchmentID = vNereidProjectLoadingInput.OCSurveyCatchmentID,
                RegionalSubbasinID = vNereidProjectLoadingInput.RegionalSubbasinID,
                SlopePercentage = vNereidProjectLoadingInput.SlopePercentage,
                RelationallyAssociatedModelingApproach = vNereidProjectLoadingInput.RelationallyAssociatedModelingApproach,
                SpatiallyAssociatedModelingApproach = vNereidProjectLoadingInput.SpatiallyAssociatedModelingApproach,
                WaterQualityManagementPlanID = vNereidProjectLoadingInput.WaterQualityManagementPlanID
            };
        }
    }
}
