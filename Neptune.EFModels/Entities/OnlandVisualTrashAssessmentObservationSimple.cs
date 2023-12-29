using System.ComponentModel.DataAnnotations;
using Neptune.Common.GeoSpatial;

namespace Neptune.EFModels.Entities
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

        public string PhotoUrl { get; set; }

        public int? PhotoStagingID { get; set; }
        public int? PhotoID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public OnlandVisualTrashAssessmentObservationSimple()
        {

        }

        public OnlandVisualTrashAssessmentObservationSimple(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID;
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID;
            Note = onlandVisualTrashAssessmentObservation.Note;
            ObservationDateTime = onlandVisualTrashAssessmentObservation.ObservationDatetime;
            
            LocationX = onlandVisualTrashAssessmentObservation.LocationPoint4326?.Coordinate.X;
            LocationY = onlandVisualTrashAssessmentObservation.LocationPoint4326?.Coordinate.Y;

            // todo: ensure there is a database constraint ensuring one photo per observation
            var photo = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationPhotos.SingleOrDefault();
            PhotoID = photo
                ?.OnlandVisualTrashAssessmentObservationPhotoID;
            PhotoUrl = photo?.FileResource.GetFileResourceUrl();
        }

        public OnlandVisualTrashAssessmentObservation ToOnlandVisualTrashAssessmentObservation()
        {
            var locationPoint4326 = GeometryHelper.CreateLocationPoint4326FromLatLong(LocationY.GetValueOrDefault(),
                LocationX.GetValueOrDefault());

            var locationPoint2771 = locationPoint4326.ProjectTo2771();

            var onlandVisualTrashAssessmentObservation = new OnlandVisualTrashAssessmentObservation
            {
                OnlandVisualTrashAssessmentID = OnlandVisualTrashAssessmentID, LocationPoint = locationPoint2771,
                Note = Note, ObservationDatetime = ObservationDateTime, LocationPoint4326 = locationPoint4326
            };
            return onlandVisualTrashAssessmentObservation;
        }
    }
}