using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;

namespace Neptune.WebMvc.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentPhotoController : NeptuneBaseController<OnlandVisualTrashAssessmentPhotoController>
    {
        public OnlandVisualTrashAssessmentPhotoController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentPhotoController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }
        // photo handling
        // these endpoints are more API-like than we usually do--they support AJAX manipulation of OVTA Photos from the OVTA workflow.

        [HttpGet("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public ContentResult StageObservationPhoto([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            return Content("");
        }

        [HttpPost("{onlandVisualTrashAssessmentPrimaryKey}")]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentPrimaryKey")]
        public async Task<ActionResult> StageObservationPhoto([FromRoute]
            OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey,
            ObservationPhotoStagingSimple observationPhotoStagingSimple)
        {
            // todo: ovta photos
            //if (!ModelState.IsValid)
            //{
            //    Response.StatusCode = (int) HttpStatusCode.BadRequest;

            //    // This endpoint can error out in a completely client-dependent way where the user's only recourse
            //    // is to try again until it works. We've never seen a server-side error from this endpoint that we
            //    // would be able to fix per-se, so just cancel the logging so we don't get bothered by it.
            //    SitkaGlobalBase.CancelErrorLoggingFromApplicationEnd();

            //    return Json(new
            //    {
            //        Error =
            //            "There was an error uploading the image. Please try again."
            //    });
            //}

            //// for now, setting arbitrary-ish (750) max height and width that roughly corresponds with the largest rendered size on the detail page
            //var resizedImage =
            //    ImageHelper.ScaleImage(
            //        FileResource.ConvertHttpPostedFileToByteArray(observationPhotoStagingSimple.Photo), 750, 750);
            
            //var resizedImageBytes = ImageHelper.ImageToByteArrayAndCompress(resizedImage);

            //var fileResource = FileResource.CreateNewResizedImageFileResource(observationPhotoStagingSimple.Photo, resizedImageBytes, CurrentPerson);

            //var staging = new OnlandVisualTrashAssessmentObservationPhotoStaging
            //{
            //    FileResource = fileResource,
            //    OnlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject
            //};
            //await _dbContext.SaveChangesAsync();

            //return Json(new
            //{
            //    PhotoStagingID = staging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
            //    PhotoStagingUrl = staging.FileResource.GetFileResourceUrl()
            //});
            return Ok();
        }

        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ActionResult DeleteObservationPhoto()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public async Task<ActionResult> DeleteObservationPhoto(DeleteObservationPhotoSimple deleteObservationPhotoSimple)
        {
            if (deleteObservationPhotoSimple.IsStagedPhoto)
            {
                await _dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Where(x => x.OnlandVisualTrashAssessmentObservationPhotoStagingID ==  deleteObservationPhotoSimple.ID).ExecuteDeleteAsync();
            }
            else
            {
                await _dbContext.OnlandVisualTrashAssessmentObservationPhotos.Where(x => x.OnlandVisualTrashAssessmentObservationPhotoID == deleteObservationPhotoSimple.ID).ExecuteDeleteAsync();
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
            public IFormFile Photo { get; set; }
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