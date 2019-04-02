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
                    $"<a href='{DetailUrlTemplate.ParameterReplace(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID)}'>View</a>");
            }

            return new HtmlString("");
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

                return DbGeometry.LineFromText(linestring, 4326);
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

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(ovta.GetAreaViaTransect());

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

                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(dbGeometry.ToSqlGeometry().MakeValid().ToDbGeometry());
                featureCollection.Features.AddRange(new List<Feature> { feature });

                transsectLineLayerGeoJson = new LayerGeoJson("transectLine", featureCollection, "#000000",
                    1,
                    LayerInitialVisibility.Show);
            }

            return transsectLineLayerGeoJson;
        }
    }
}
