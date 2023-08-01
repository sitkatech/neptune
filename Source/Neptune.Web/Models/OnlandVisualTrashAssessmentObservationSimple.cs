using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
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

        public string PhotoUrl { get; set; }

        public int? PhotoStagingID { get; set; }
        public int? PhotoID { get; set; }

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
            
            LocationX = o.LocationPoint4326.XCoordinate.GetValueOrDefault();
            LocationY = o.LocationPoint4326.YCoordinate.GetValueOrDefault();

            // todo: ensure there is a database constraint ensuring one photo per observo
            var photo = o.OnlandVisualTrashAssessmentObservationPhotos.SingleOrDefault();
            PhotoID = photo
                ?.OnlandVisualTrashAssessmentObservationPhotoID;
            PhotoUrl = photo?.FileResource.GetFileResourceUrl();
        }

        public OnlandVisualTrashAssessmentObservation ToOnlandVisualTrashAssessmentObservation()
        {
            DbGeometry locationPoint4326 = DbSpatialHelper.MakeDbGeometryFromCoordinates(LocationX.GetValueOrDefault(),
                LocationY.GetValueOrDefault(), CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

            var locationPoint2771 =
                CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(locationPoint4326);

            return new OnlandVisualTrashAssessmentObservation(OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID, locationPoint2771, Note, ObservationDateTime, locationPoint4326);
        }
    }
}