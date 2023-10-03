using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models
{
    public static class NereidResultExtensionMethods
    {
        public static ProjectNereidResult toProjectNereidResult (this NereidResult nereidResult,int projectID)
        {
            return new ProjectNereidResult()
            {
                ProjectID = projectID,
                IsBaselineCondition = nereidResult.IsBaselineCondition,
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
