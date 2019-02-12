using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using static System.String;

namespace Neptune.Web.Models
{

    public static class OnlandVisualTrashAssessmentModelExtensions
    {
        private static DbGeometry GetTransect(this OnlandVisualTrashAssessment ovta)
        {
            var points = Join(",",
                ovta.OnlandVisualTrashAssessmentObservations.OrderBy(x => x.ObservationDatetime)
                    .Select(x => x.LocationPoint).ToList().Select(x => $"{x.XCoordinate} {x.YCoordinate}").ToList());

            var linestring = $"LINESTRING ({points})";

            return DbGeometry.LineFromText(linestring, 4326);
        }

        public static IQueryable<Parcel> GetParcelsViaTransect(this OnlandVisualTrashAssessment ovta)
        {
            var transect = ovta.OnlandVisualTrashAssessmentObservations.Count == 1
                ? ovta.OnlandVisualTrashAssessmentObservations.Single().LocationPoint // don't attempt to calculate the transect
                : ovta.GetTransect();

            return HttpRequestStorage.DatabaseEntities.Parcels.Where(x=>x.ParcelGeometry.Intersects(transect));
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

        public static string GetEditUrl(this OnlandVisualTrashAssessment ovta)
        {
            return SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                x.Instructions(ovta.OnlandVisualTrashAssessmentID));
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
    }
}
