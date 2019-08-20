using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using static System.String;

namespace Neptune.Web.Models
{

    public static class OnlandVisualTrashAssessmentModelExtensions
    {
        public static readonly UrlTemplate<int> EditUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(t => t.RecordObservations(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this OnlandVisualTrashAssessment ovta)
        {
            return EditUrlTemplate.ParameterReplace(ovta.OnlandVisualTrashAssessmentID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this OnlandVisualTrashAssessment ovta)
        {
            return DeleteUrlTemplate.ParameterReplace(ovta.OnlandVisualTrashAssessmentID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static HtmlString GetDetailUrlForGrid(this OnlandVisualTrashAssessment onlandVisualTrashAssessment, Person currentPerson)
        {
            if (new OnlandVisualTrashAssessmentViewFeature().HasPermission(currentPerson, onlandVisualTrashAssessment)
                    .HasPermission && onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus ==
                OnlandVisualTrashAssessmentStatus.Complete)
            {
                return new HtmlString(
                    $"<a class='gridButton' href='{DetailUrlTemplate.ParameterReplace(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)}'>View</a>");
            }

            return new HtmlString("");
        }


        public static readonly UrlTemplate<int> EditStatusToAllowEditURLTemplate =
           new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(t =>
                    t.EditStatusToAllowEdit(UrlTemplate.Parameter1Int)));

        public static string GetEditStatusToAllowEditUrl(this OnlandVisualTrashAssessment ovta)
        {
            return EditStatusToAllowEditURLTemplate.ParameterReplace(ovta.OnlandVisualTrashAssessmentID);
        }

        public static HtmlString GetEditUrlForGrid(this OnlandVisualTrashAssessment onlandVisualTrashAssessment, Person currentPerson)
        {
            var userCanEdit = new OnlandVisualTrashAssessmentEditStausFeature()
                .HasPermission(currentPerson, onlandVisualTrashAssessment)
                .HasPermission;
            if (!userCanEdit) return new HtmlString(Empty);

            var modalDialogForm = new ModalDialogForm(GetEditStatusToAllowEditUrl(onlandVisualTrashAssessment),
                ModalDialogFormHelper.DefaultDialogWidth, "Return to Edit");

            return onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus ==
                   OnlandVisualTrashAssessmentStatus.Complete
                ? ModalDialogFormHelper.ModalDialogFormLink("Return to Edit", GetEditStatusToAllowEditUrl(onlandVisualTrashAssessment),
                    Format("Return to Edit On-land Visual Trash Assessment for {0}", onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName),
                    500, "Continue", "Cancel", new List<string> { "gridButton" },
                    null, null) : DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(GetEditUrl(onlandVisualTrashAssessment));
        }

        public static string ToBaselineProgress(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
           return onlandVisualTrashAssessment.IsProgressAssessment ? "Progress" : "Baseline";
        }

        public static DbGeometry GetTransect(this OnlandVisualTrashAssessment ovta)
        {
            if (ovta.OnlandVisualTrashAssessmentObservations.Count > 1)
            {
                var points = Join(",",
                    ovta.OnlandVisualTrashAssessmentObservations.OrderBy(x => x.ObservationDatetime)
                        .Select(x => x.LocationPoint).ToList().Select(x => $"{x.XCoordinate} {x.YCoordinate}")
                        .ToList());

                var linestring = $"LINESTRING ({points})";

                // the transect is going to be in 2771 because it was generated from points in 2771
                return DbGeometry.LineFromText(linestring, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            }

            return null;
        }

        public static IQueryable<Parcel> GetParcelsViaTransect(this OnlandVisualTrashAssessment ovta)
        {
            if (!ovta.OnlandVisualTrashAssessmentObservations.Any())
            {
                return new List<Parcel>().AsQueryable();
            }

            var transect = ovta.OnlandVisualTrashAssessmentObservations.Count == 1
                ? ovta.OnlandVisualTrashAssessmentObservations.Single().LocationPoint // don't attempt to calculate the transect
                : ovta.GetTransect();

            return HttpRequestStorage.DatabaseEntities.Parcels.Where(x => x.ParcelGeometry.Intersects(transect));
        }

        public static DbGeometry GetAreaViaTransect(this OnlandVisualTrashAssessment ovta)
        {
            var parcelGeoms = ovta.GetParcelsViaTransect().Select(x => x.ParcelGeometry).ToList();
            return parcelGeoms.UnionListGeometries();
        }

        public static FeatureCollection GetAssessmentAreaFeatureCollection(this OnlandVisualTrashAssessment ovta)
        {
            var featureCollection = new FeatureCollection();

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChecc(ovta.GetAreaViaTransect());

            featureCollection.Features.Add(feature);
            return featureCollection;
        }

        public static List<int> GetParcelIDsForAddOrRemoveParcels(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            if (onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined.GetValueOrDefault())
            {
                return new List<int>();
            }

            var parcelIDs = onlandVisualTrashAssessment.DraftGeometry == null
                ? onlandVisualTrashAssessment.GetParcelsViaTransect().Select(x => x.ParcelID)
                : HttpRequestStorage.DatabaseEntities.Parcels
                    .Where(x => onlandVisualTrashAssessment.DraftGeometry.Contains(x.ParcelGeometry)).Select(x => x.ParcelID);
            return parcelIDs.ToList();
        }

        public static List<PreliminarySourceIdentificationSimple> GetPreliminarySourceIdentificationSimples(
            this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var presentGuys = onlandVisualTrashAssessment
                .OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.ToList()
                .Select(x => new PreliminarySourceIdentificationSimple(x)).ToList();

            var missingGuys = PreliminarySourceIdentificationType.All.Except(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Select(x => x.PreliminarySourceIdentificationType)).Select(x => new PreliminarySourceIdentificationSimple(x));
            presentGuys.AddRange(missingGuys);

            return presentGuys;
        }

        public static LayerGeoJson GetTransectLineLayerGeoJson(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            LayerGeoJson transsectLineLayerGeoJson;

            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.TransectLine != null)
            {
                transsectLineLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                    .GetTransectLineLayerGeoJson();
            }
            else
            {
                var featureCollection = new FeatureCollection();
                var dbGeometry = onlandVisualTrashAssessment.GetTransect();
                if (dbGeometry == null)
                {
                    return null;
                }

                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChecc(dbGeometry.ToSqlGeometry().MakeValid().ToDbGeometry());
                featureCollection.Features.AddRange(new List<Feature> { feature });

                transsectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000",
                    1,
                    LayerInitialVisibility.Show);
            }

            return transsectLineLayerGeoJson;
        }

        public static LayerGeoJson GetAssessmentAreaLayerGeoJson(this OnlandVisualTrashAssessment onlandVisualTrashAssessment, bool reduce)
        {
            FeatureCollection geoJsonFeatureCollection;
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null)
            {
                geoJsonFeatureCollection =
                    new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea }
                        .ToGeoJsonFeatureCollection();
            }
            else if (onlandVisualTrashAssessment.DraftGeometry != null)
            {
                var draftGeometry = onlandVisualTrashAssessment.DraftGeometry;
                geoJsonFeatureCollection = new FeatureCollection();

                // Leaflet.Draw does not support multipolgyon editing because its dev team decided it wasn't necessary.
                // Unless https://github.com/Leaflet/Leaflet.draw/issues/268 is resolved, we have to break into separate polys.
                // On an unrelated note, DbGeometry.ElementAt is 1-indexed instead of 0-indexed, which is terrible.
                for (var i = 1; i <= draftGeometry.ElementCount.GetValueOrDefault(); i++)
                {
                    var dbGeometry = draftGeometry.ElementAt(i);
                    if (reduce)
                    {
                        // Reduce is SQL Server's implementation of the Douglas–Peucker downsampling algorithm
                        dbGeometry = dbGeometry.ToSqlGeometry().Reduce(.0000025).ToDbGeometry().FixSrid(CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
                    }
                    var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChecc(dbGeometry);
                    geoJsonFeatureCollection.Features.Add(feature);
                }
            }
            else
            {
                geoJsonFeatureCollection = new FeatureCollection();
            }

            var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", geoJsonFeatureCollection,
                "#ffff00", .5m,
                LayerInitialVisibility.Show);
            return assessmentAreaLayerGeoJson;
        }
    }
}
