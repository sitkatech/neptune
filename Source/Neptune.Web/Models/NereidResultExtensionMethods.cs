namespace Neptune.Web.Models
{
    public static class NereidResultExtensionMethods
    {
        public static PlannedProjectNereidResult toPlannedProjectNereidResult (this NereidResult nereidResult,int plannedProjectID)
        {
            return new PlannedProjectNereidResult(plannedProjectID, nereidResult.IsBaselineCondition)
            {
                FullResponse =  nereidResult.FullResponse,
                LastUpdate = nereidResult.LastUpdate,
                NodeID = nereidResult.NodeID,
                RegionalSubbasinID = nereidResult.RegionalSubbasinID,
                TreatmentBMPID = nereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = nereidResult.WaterQualityManagementPlanID
            };
        }
    }
}
