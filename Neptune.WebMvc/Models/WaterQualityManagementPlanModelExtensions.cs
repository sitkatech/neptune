using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models
{
    public static class WaterQualityManagementPlanModelExtensions
    {
        // technically this isn't "fully" parameteried, it's just "parameterized enough to have results", which is basically the same damn thing.
        public static bool IsFullyParameterized(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
            {
                return waterQualityManagementPlan.TreatmentBMPs.Any(x => x.IsFullyParameterized(x.Delineation));
            }

            return waterQualityManagementPlan.QuickBMPs.Any(x => x.IsFullyParameterized());
        }
    }
}