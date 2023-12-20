using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class OnlandVisualTrashAssessmentPhotoController : NeptuneBaseController<OnlandVisualTrashAssessmentPhotoController>
    {
        private readonly FileResourceService _fileResourceService;

        public OnlandVisualTrashAssessmentPhotoController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentPhotoController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
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
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    Error =
                        "There was an error uploading the image. Please try again."
                });
            }

            var fileResource = await _fileResourceService.CreateNewFromIFormFile(observationPhotoStagingSimple.Photo, CurrentPerson);

            var onlandVisualTrashAssessmentObservationPhotoStaging = new OnlandVisualTrashAssessmentObservationPhotoStaging
            {
                FileResource = fileResource,
                OnlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject
            };
            await _dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.AddAsync(onlandVisualTrashAssessmentObservationPhotoStaging);
            await _dbContext.SaveChangesAsync();

            return Json(new
            {
                PhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                PhotoStagingUrl = onlandVisualTrashAssessmentObservationPhotoStaging.FileResource.GetFileResourceUrl()
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