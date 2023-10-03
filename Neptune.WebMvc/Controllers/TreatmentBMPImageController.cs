using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview;
using Neptune.WebMvc.Views.TreatmentBMPImage;

namespace Neptune.WebMvc.Controllers
{
    public class TreatmentBMPImageController : NeptuneBaseController<TreatmentBMPImageController>
    {
        private readonly FileResourceService _fileResourceService;

        public TreatmentBMPImageController(NeptuneDbContext dbContext, ILogger<TreatmentBMPImageController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult ManageTreatmentBMPImages([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPImages = TreatmentBMPImages.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID);
            var viewModel = new ManageTreatmentBMPImagesViewModel(treatmentBMPImages);
            return ViewManageTreatmentBMPImages(viewModel, treatmentBMP, treatmentBMPImages);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> ManageTreatmentBMPImages([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ManageTreatmentBMPImagesViewModel viewModel)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey);
            var treatmentBMPImages = TreatmentBMPImages.ListByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPID);
            if (!ModelState.IsValid)
            {
                return ViewManageTreatmentBMPImages(viewModel, treatmentBMP, treatmentBMPImages);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            await viewModel.UpdateModel(CurrentPerson, treatmentBMP, _dbContext, _fileResourceService, treatmentBMPImages);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");

            return Redirect(SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(_linkGenerator, c => c.ManageTreatmentBMPImages(treatmentBMP)));
        }

        private ViewResult ViewManageTreatmentBMPImages(ManageTreatmentBMPImagesViewModel viewModel,
            TreatmentBMP treatmentBMP, IEnumerable<TreatmentBMPImage> treatmentBMPImages)
        {
            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMPImages);
            var viewData = new ManageTreatmentBMPImagesViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, managePhotosWithPreviewViewData);
            return RazorView<ManageTreatmentBMPImages, ManageTreatmentBMPImagesViewData, ManageTreatmentBMPImagesViewModel>(viewData, viewModel);
        }
    }
}
