using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentPhotoController : NeptuneBaseController
    {
        // photo handling
        // these endpoints are more API-like than we usually do--they support AJAX manipulation of OVTA Photos from the OVTA workflow.

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult StageObservationPhoto(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewFeature]
        public ActionResult StageObservationPhoto(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ObservationPhotoStagingSimple opss)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Error = "There was an error uploading the image. Please try again."
                });
            }

            var fileResource = FileResource.CreateNewFromHttpPostedFile(opss.Photo, CurrentPerson);

            var staging = new OnlandVisualTrashAssessmentObservationPhotoStaging(fileResource,
                onlandVisualTrashAssessmentPrimaryKey.EntityObject);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return Json(new
            {
                PhotoStagingID = staging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                PhotoStagingUrl = staging.FileResource.GetFileResourceUrl()
            });
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ActionResult DeleteObservationPhoto()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewFeature]
        public ActionResult DeleteObservationPhoto(DeleteObservationPhotoSimple dopss)
        {
            if (dopss.IsStagedPhoto)
            {
                var onlandVisualTrashAssessmentObservationPhotoStaging =
                    ((OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey) dopss.ID).EntityObject;
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotoStagings
                    .DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(
                        onlandVisualTrashAssessmentObservationPhotoStaging);
            }
            else
            {
                var onlandVisualTrashAssessmentObservationPhoto =
                    ((OnlandVisualTrashAssessmentObservationPhotoPrimaryKey) dopss.ID).EntityObject;
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotos
                    .DeleteOnlandVisualTrashAssessmentObservationPhoto(
                        onlandVisualTrashAssessmentObservationPhoto);
            }

            return Content("");
        }

        public class DeleteObservationPhotoSimple
        {
            public int ID { get; set; }

            public bool IsStagedPhoto { get; set; }
        }

        public class ObservationPhotoStagingSimple
        {
            [SitkaFileExtensions("jpg|jpeg|gif|png")]
            [Required]
            public HttpPostedFileBase Photo { get; set; }
        }
    }
}