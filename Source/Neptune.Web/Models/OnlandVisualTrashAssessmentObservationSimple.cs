using System;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationSimple
    {
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public string Note { get; set; }
        public DateTime ObservationDatetime { get; set; }
        public decimal? LocationX { get; set; }
        public decimal? LocationY { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public OnlandVisualTrashAssessmentObservationSimple()
        {

        }

        public OnlandVisualTrashAssessmentObservationSimple(OnlandVisualTrashAssessmentObservation o)
        {
            OnlandVisualTrashAssessmentObservationID = o.OnlandVisualTrashAssessmentObservationID;
            OnlandVisualTrashAssessmentID = o.OnlandVisualTrashAssessmentID;
            Note = o.Note;
            ObservationDatetime = o.ObservationDatetime;
            LocationX = o.LocationPoint.XCoordinate.HasValue ? (decimal?) o.LocationPoint.XCoordinate : null;
            LocationY = o.LocationPoint.YCoordinate.HasValue ? (decimal?) o.LocationPoint.YCoordinate : null;
        }
    }
}