using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

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

        public static readonly UrlTemplate<int> BeginOVTAUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(t => t.NewAssessment(UrlTemplate.Parameter1Int)));
        public static string GetBeginOVTAUrl(this Models.OnlandVisualTrashAssessmentArea ovtaa)
        {
            return BeginOVTAUrlTemplate.ParameterReplace(ovtaa.OnlandVisualTrashAssessmentAreaID);
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

        public static HtmlString GetDisplayNameAsDetailUrl(this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, Person currentPerson)
        {
            if (!new OnlandVisualTrashAssessmentAreaViewFeature()
                .HasPermission(currentPerson, onlandVisualTrashAssessmentArea).HasPermission)
            {
                return new HtmlString(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName);
            }

            return new HtmlString(
                $"<a href=\"{onlandVisualTrashAssessmentArea.GetDetailUrl()}\" alt=\"{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}\" title=\"{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}\" >{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}</a>");
        }

        public static LayerGeoJson GetTransectLineLayerGeoJson(this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            if (onlandVisualTrashAssessmentArea.TransectLine != null)
            {
                var featureCollection = new FeatureCollection();
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(onlandVisualTrashAssessmentArea.TransectLine);
                featureCollection.Features.AddRange(new List<Feature> {feature});

                LayerGeoJson transectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000",
                    1,
                    LayerInitialVisibility.Show);
                return transectLineLayerGeoJson;
            }

            return null;
        }
    }
}