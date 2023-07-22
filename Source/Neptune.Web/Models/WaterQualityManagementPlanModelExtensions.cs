using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;

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

        public static LayerGeoJson GetBoundaryLayerGeoJson(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var featureCollection = new FeatureCollection();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(waterQualityManagementPlan.WaterQualityManagementPlanBoundary?.Geometry4326);
            featureCollection.Features.AddRange(new List<Feature> { feature });

            var boundaryLayerGeoJson = new LayerGeoJson("wqmpBoundary", featureCollection, "#4782ff",
                1,
                LayerInitialVisibility.Show);

            return boundaryLayerGeoJson;
        }
    }
}