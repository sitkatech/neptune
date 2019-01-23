using System.Data.Entity.Spatial;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using static System.String;

namespace Neptune.Web.Models
{

    public static class OnlandVisualTrashAssessmentModelExtensions
    {
        public static DbGeometry GetTransect(this OnlandVisualTrashAssessment ovta)
        {
            var points = Join(",",
                ovta.OnlandVisualTrashAssessmentObservations.OrderBy(x => x.ObservationDatetime)
                    .Select(x => x.LocationPoint).ToList().Select(x => $"{x.XCoordinate} {x.YCoordinate}").ToList());

            var linestring = $"LINESTRING ({points})";

            return DbGeometry.LineFromText(linestring, 4326);
        }

        public static IQueryable<Parcel> GetParcelsViaTransect(this OnlandVisualTrashAssessment ovta)
        {
            var transect = ovta.GetTransect();

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
    }
}
