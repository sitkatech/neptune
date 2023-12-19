using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
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
            if (onlandVisualTrashAssessmentArea.TransectLine != null)
            {
                var featureCollection = new FeatureCollection();
                var feature = new Feature(onlandVisualTrashAssessmentArea.TransectLine, new AttributesTable());
                featureCollection.Add(feature);
                var transectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000", 1, LayerInitialVisibility.Show);
                return transectLineLayerGeoJson;
            }

            return null;
        }

        public static Geometry RecomputeTransectLine(List<EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments,
            out EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var completedOVTAs = onlandVisualTrashAssessments
                .Where(x => x.OnlandVisualTrashAssessmentStatusID == (int) OnlandVisualTrashAssessmentStatusEnum.Complete).ToList();

            // new transect should come from the earliest completed assessment
            if (completedOVTAs.Any())
            {
                onlandVisualTrashAssessment = completedOVTAs.MinBy(x => x.CompletedDate);
                return onlandVisualTrashAssessment.GetTransect();
            }

            onlandVisualTrashAssessment = null;
            return null;
        }

        public static OnlandVisualTrashAssessmentScore CalculateProgressScore(List<EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments)
        {
            var completedAndIsProgressAssessment = onlandVisualTrashAssessments.Where(x =>
                x.OnlandVisualTrashAssessmentStatusID == OnlandVisualTrashAssessmentStatus.Complete
                    .OnlandVisualTrashAssessmentStatusID && x.IsProgressAssessment).ToList();

            if (!completedAndIsProgressAssessment.Any())
            {
                return null;
            }

            var average = completedAndIsProgressAssessment.OrderByDescending(x=>x.CompletedDate).Take(3).Average(x=>x.OnlandVisualTrashAssessmentScore.NumericValue);

            var onlandVisualTrashAssessmentScore = OnlandVisualTrashAssessmentScore.All.Single(x => x.NumericValue == Math.Round(average));
            
            return onlandVisualTrashAssessmentScore;
        }

        public static LayerGeoJson GetAssessmentAreaLayerGeoJson(this EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var geoJsonFeatureCollection = new List<EFModels.Entities.OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }
                .ToGeoJsonFeatureCollection();

            var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", geoJsonFeatureCollection, "#ffff00", 0.5f, LayerInitialVisibility.Show);
            return assessmentAreaLayerGeoJson;
        }

        public static OnlandVisualTrashAssessmentScore? CalculateScoreFromBackingData(
            List<EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments, bool calculateProgressScore)
        {
            var completedAndIsProgressAssessment = onlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatusID == (int)
                    OnlandVisualTrashAssessmentStatusEnum.Complete && x.IsProgressAssessment == calculateProgressScore).ToList();

            if (!completedAndIsProgressAssessment.Any())
            {
                return null;
            }

            var average = completedAndIsProgressAssessment.Average(x => x.OnlandVisualTrashAssessmentScore.NumericValue);
            var round = (int)Math.Round(average);
            return OnlandVisualTrashAssessmentScore.All.SingleOrDefault(x => x.NumericValue == round);
        }
    }
}
