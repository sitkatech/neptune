using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public static class OnlandVisualTrashAssessmentAreaModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<EFModels.Entities.OnlandVisualTrashAssessmentArea> areas)
        {
            var featureCollection = new FeatureCollection();
            foreach (var area in areas)
            {
                var attributesTable = new AttributesTable
                {
                    { "OnlandVisualTrashAssessmentAreaID", area.OnlandVisualTrashAssessmentAreaID },
                    { "OnlandVisualTrashAssessmentAreaName", area.OnlandVisualTrashAssessmentAreaName },
                    { "StormwaterJurisdictionID", area.StormwaterJurisdictionID }
                };
                var feature = new Feature(area.OnlandVisualTrashAssessmentAreaGeometry4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        public static HtmlString GetBaselineScoreAsHtmlString(
            this EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return new HtmlString(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore != null
                ? onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
        }

        public static HtmlString GetProgressScoreAsHtmlString(
            this EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return new HtmlString(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore != null
                ? onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
        }

        public static LayerGeoJson? GetTransectLineLayerGeoJson(this EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            if (onlandVisualTrashAssessmentArea.TransectLine4326 != null)
            {
                var featureCollection = new FeatureCollection();
                var feature = new Feature(onlandVisualTrashAssessmentArea.TransectLine4326, new AttributesTable());
                featureCollection.Add(feature);
                var transectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000", 1, LayerInitialVisibility.Show);
                return transectLineLayerGeoJson;
            }

            return null;
        }

        public static LayerGeoJson GetAssessmentAreaLayerGeoJson(this EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var geoJsonFeatureCollection = new List<EFModels.Entities.OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }
                .ToGeoJsonFeatureCollection();

            var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", geoJsonFeatureCollection, "#ffff00", 0.5f, LayerInitialVisibility.Show);
            return assessmentAreaLayerGeoJson;
        }
    }
}
