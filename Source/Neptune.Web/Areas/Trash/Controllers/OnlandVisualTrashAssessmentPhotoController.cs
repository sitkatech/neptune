using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common;
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
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ContentResult StageObservationPhoto(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            return Content("");
        }

        public const string ErrorPageReportPath = "~/Views/Shared/PerformanceMeasureControls/PerformanceMeasureReportedValuesSummary.cshtml";

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ActionResult StageObservationPhoto(
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ObservationPhotoStagingSimple observationPhotoStagingSimple)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;

                // This endpoint can error out in a completely client-dependent way where the user's only recourse
                // is to try again until it works. We've never seen a server-side error from this endpoint that we
                // would be able to fix per-se, so just cancel the logging so we don't get bothered by it.
                SitkaGlobalBase.CancelErrorLoggingFromApplicationEnd();

                return Json(new
                {
                    Error =
                        "There was an error uploading the image. Please try again."
                });
            }

            // for now, setting arbitrary-ish (750) max height and width that roughly corresponds with the largest rendered size on the detail page
            var resizedImage =
                ImageHelper.ScaleImage(
                    FileResource.ConvertHttpPostedFileToByteArray(observationPhotoStagingSimple.Photo), 750, 750);
            
            var resizedImageBytes = ImageHelper.ImageToByteArrayAndCompress(resizedImage);

            var fileResource = FileResource.CreateNewResizedImageFileResource(observationPhotoStagingSimple.Photo, resizedImageBytes, CurrentPerson);

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
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ActionResult DeleteObservationPhoto()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
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

        public class ObservationPhotoStagingViewData
        {

        }

        public abstract class ObservationPhotoStaging : TypedWebPartialViewPage<ObservationPhotoStagingViewData,
            ObservationPhotoStagingSimple>
        {

        }
    }
}