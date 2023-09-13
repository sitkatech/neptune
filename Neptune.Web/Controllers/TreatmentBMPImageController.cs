using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Services;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
using Neptune.Web.Views.TreatmentBMPImage;

namespace Neptune.Web.Controllers
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
            var viewModel = new ManageTreatmentBMPImagesViewModel(treatmentBMP);
            return ViewManageTreatmentBMPImages(viewModel, treatmentBMP);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> ManageTreatmentBMPImages([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ManageTreatmentBMPImagesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewManageTreatmentBMPImages(viewModel, treatmentBMP);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(CurrentPerson, treatmentBMP, _dbContext, _fileResourceService);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");

            return Redirect(SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(_linkGenerator, c => c.ManageTreatmentBMPImages(treatmentBMP)));
        }

        private ViewResult ViewManageTreatmentBMPImages(ManageTreatmentBMPImagesViewModel viewModel,
            TreatmentBMP treatmentBMP)
        {
            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP);
            var viewData = new ManageTreatmentBMPImagesViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, managePhotosWithPreviewViewData);
            return RazorView<ManageTreatmentBMPImages, ManageTreatmentBMPImagesViewData, ManageTreatmentBMPImagesViewModel>(viewData, viewModel);
        }
    }
}
