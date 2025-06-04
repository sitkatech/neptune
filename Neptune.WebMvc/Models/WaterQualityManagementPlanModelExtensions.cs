using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models;

public static class WaterQualityManagementPlanModelExtensions
{
    public static FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<WaterQualityManagementPlan> wqmpBoundaries,
        Action<Feature, WaterQualityManagementPlan> onEachFeature)
    {
        var featureCollection = new FeatureCollection();
        foreach (var wqmpBoundary in wqmpBoundaries)
        {
            var attributesTable = new AttributesTable
            {
                { "Name", wqmpBoundary.WaterQualityManagementPlanName },
                { "FeatureColor", "#935F59" },
                { "WaterQualityManagementPlanID", wqmpBoundary.WaterQualityManagementPlanID },
                { "StormwaterJurisdictionID", wqmpBoundary.StormwaterJurisdictionID }
            };
            var feature = new Feature(wqmpBoundary.WaterQualityManagementPlanBoundary?.Geometry4326, attributesTable);
            onEachFeature?.Invoke(feature, wqmpBoundary);
            featureCollection.Add(feature);
        }
        return featureCollection;
    }

    public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<WaterQualityManagementPlan> wqmpBoundaries,
        LinkGenerator linkGenerator)
    {
        var mapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, t => t.SummaryForMap(UrlTemplate.Parameter1Int)));

        var featureCollection = new FeatureCollection();
        foreach (var wqmpBoundary in wqmpBoundaries)
        {
            var attributesTable = new AttributesTable
            {
                { "Name", wqmpBoundary.WaterQualityManagementPlanName },
                { "FeatureColor", "#935F59" },
                { "WaterQualityManagementPlanID", wqmpBoundary.WaterQualityManagementPlanID },
                { "StormwaterJurisdictionID", wqmpBoundary.StormwaterJurisdictionID },
                { "MapSummaryUrl", mapSummaryUrlTemplate.ParameterReplace(wqmpBoundary.WaterQualityManagementPlanID) },
            };
            var feature = new Feature(wqmpBoundary.WaterQualityManagementPlanBoundary?.Geometry4326, attributesTable);
            featureCollection.Add(feature);
        }
        return featureCollection;
    }
}