using System.Data.Entity.Spatial;
using System.Linq;
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
    }
}
