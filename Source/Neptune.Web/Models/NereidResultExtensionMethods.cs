namespace Neptune.Web.Models
{
    public static class NereidResultExtensionMethods
    {
        public static ProjectNereidResult toProjectNereidResult (this NereidResult nereidResult,int projectID)
        {
            return new ProjectNereidResult(projectID, nereidResult.IsBaselineCondition)
            {
                FullResponse =  nereidResult.FullResponse,
                LastUpdate = nereidResult.LastUpdate,
                NodeID = nereidResult.NodeID,
                RegionalSubbasinID = nereidResult.RegionalSubbasinID,
                TreatmentBMPID = nereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = nereidResult.WaterQualityManagementPlanID,
                DelineationID = nereidResult.DelineationID
            };
        }
    }
}
