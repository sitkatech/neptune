using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Web;
using LtInfo.Common.DbSpatial;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationSimple
    {
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        [Required]
        public int OnlandVisualTrashAssessmentID { get; set; }
        public string Note { get; set; }
        [Required]
        public DateTime ObservationDateTime { get; set; }
        [Required]
        public double? LocationX { get; set; }
        [Required]
        public double? LocationY { get; set; }

        public bool? DeletePhoto { get; set; }

        public HttpPostedFileBase Photo { get; set; }

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
            ObservationDateTime = o.ObservationDatetime;
            LocationX = o.LocationPoint.XCoordinate.GetValueOrDefault();
            LocationY = o.LocationPoint.YCoordinate.GetValueOrDefault();
        }

        public OnlandVisualTrashAssessmentObservation ToOnlandVisualTrashAssessmentObservation()
        {
            DbGeometry locationPoint = DbSpatialHelper.MakeDbGeometryFromCoordinates(LocationX.GetValueOrDefault(),
                LocationY.GetValueOrDefault(), MapInitJson.CoordinateSystemId);

            return new OnlandVisualTrashAssessmentObservation(OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID, locationPoint, Note, ObservationDateTime);
        }
    }
}