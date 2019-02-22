using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public static class OnlandVisualTrashAssessmentAreaModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Models.OnlandVisualTrashAssessmentArea ovtaa)
        {
            return DetailUrlTemplate.ParameterReplace(ovtaa.OnlandVisualTrashAssessmentAreaID);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Models.OnlandVisualTrashAssessmentArea> areas)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(areas.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.OnlandVisualTrashAssessmentAreaGeometry);
                feature.Properties.Add("OnlandVisualTrashAssessmentAreaID", x.OnlandVisualTrashAssessmentAreaID);
                feature.Properties.Add("OnlandVisualTrashAssessmentAreaName", x.OnlandVisualTrashAssessmentAreaName);
                feature.Properties.Add("StormwaterJurisdictionID", x.StormwaterJurisdictionID);
                return feature;
            }));
            return featureCollection;
        }

        public static LayerGeoJson MakeAssessmentAreasLayerGeoJson(this IEnumerable<Models.OnlandVisualTrashAssessmentArea> assessmentArea)
        {
            var featureCollection = assessmentArea.ToGeoJsonFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }
    }
}