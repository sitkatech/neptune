using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using NetTopologySuite.Features;

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
                return waterQualityManagementPlan.TreatmentBMPs.Any(x => x.IsFullyParameterized());
            }

            return waterQualityManagementPlan.QuickBMPs.Any(x => x.IsFullyParameterized());
        }

        public static LayerGeoJson GetBoundaryLayerGeoJson(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var featureCollection = new FeatureCollection();
            var feature = new Feature(waterQualityManagementPlan.WaterQualityManagementPlanBoundary?.Geometry4326,
                new AttributesTable());
            featureCollection.Add(feature);

            var boundaryLayerGeoJson = new LayerGeoJson("wqmpBoundary", featureCollection, "#4782ff",
                1,
                LayerInitialVisibility.Show);

            return boundaryLayerGeoJson;
        }
    }
}