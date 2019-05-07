using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using MoreLinq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public static class OnlandVisualTrashAssessmentAreaModelExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return DeleteUrlTemplate.ParameterReplace(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
        }

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

        public static DateTime? GetLastAssessmentDate(
            this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Max(x => x.CompletedDate);
        }

        public static HtmlString GetScoreAsHtmlString(
            this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return new HtmlString(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore != null
                ? onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
        }

        public static LayerGeoJson GetTransectLineLayerGeoJson(this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            if (onlandVisualTrashAssessmentArea.TransectLine != null)
            {
                var featureCollection = new FeatureCollection();
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(onlandVisualTrashAssessmentArea.TransectLine.ToSqlGeometry().MakeValid().ToDbGeometry());
                featureCollection.Features.AddRange(new List<Feature> {feature});

                LayerGeoJson transectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000",
                    1,
                    LayerInitialVisibility.Show);
                return transectLineLayerGeoJson;
            }

            return null;
        }

        public static Models.OnlandVisualTrashAssessment GetTransectBackingAssessment(this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.SingleOrDefault(x =>
                x.IsTransectBackingAssessment);
        }

        public static DbGeometry RecomputeTransectLine(
            this Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var onlandVisualTrashAssessments = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments
                .Where(x => x.OnlandVisualTrashAssessmentStatusID ==
                            OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID).ToList();

            if (onlandVisualTrashAssessments.Any())
            {
                return onlandVisualTrashAssessments.MaxBy(x => x.CompletedDate).GetTransect().FixSrid();
            }

            return null;
        }
    }
}
