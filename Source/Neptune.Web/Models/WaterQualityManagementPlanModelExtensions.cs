using System.Linq;

namespace Neptune.Web.Models
{
    public static class WaterQualityManagementPlanModelExtensions
    {
        // technically this isn't "fully" parameteried, it's just "parameterized enough to have results", which is basically the same damn thing.
        public static bool IsFullyParameterized(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
            {
                return waterQualityManagementPlan.TreatmentBMPs.Any() &&
                       waterQualityManagementPlan.TreatmentBMPs.Any(x => x.IsFullyParameterized());
            }
            else
            {
                return waterQualityManagementPlan.QuickBMPs.Any() &&
                       waterQualityManagementPlan.QuickBMPs.Any(x => x.IsFullyParameterized());
            }
        }
    }
}