using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models
{
    public static class WaterQualityManagementPlanModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<WaterQualityManagementPlan> waterQualityManagementPlans)
        {
            var featureCollection = new FeatureCollection();
            foreach (var waterQualityManagementPlan in waterQualityManagementPlans.Where(x => x.WaterQualityManagementPlanBoundary != null))
            {
                var attributesTable = new AttributesTable
                {
                    { "FeatureColor", waterQualityManagementPlan.TrashCaptureStatusType.FeatureColorOnTrashModuleMap() },
                    { "TrashCaptureStatusTypeID", waterQualityManagementPlan.TrashCaptureStatusTypeID },
                    { "WaterQualityManagementPlanID", waterQualityManagementPlan.WaterQualityManagementPlanID },
                };
                var feature = new Feature(waterQualityManagementPlan.WaterQualityManagementPlanBoundary.Geometry4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

    }
}