using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Views.ParcelLayerUpload;

namespace Neptune.WebMvc.Controllers
{
    public class ParcelLayerUploadController : NeptuneBaseController<ParcelLayerUploadController>
    {
        public ParcelLayerUploadController(NeptuneDbContext dbContext, ILogger<ParcelLayerUploadController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UpdateParcelLayerGeometry()
        {
            var viewModel = new UploadParcelLayerViewModel { PersonID = CurrentPerson.PersonID };
            return ViewUpdateParcelLayerGeometry(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<IActionResult> UpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateParcelLayerGeometry(viewModel);
            }

            viewModel.UpdateModel(CurrentPerson);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Parcels were successfully added to the staging area. The staged Parcels will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

            return Redirect(SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, c => c.Index()));
        }

        private ViewResult ViewUpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<ParcelLayerUploadController>.BuildUrlFromExpression(_linkGenerator, c => c.UpdateParcelLayerGeometry());

            var viewData = new UploadParcelLayerViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, newGisUploadUrl);
            return RazorView<UploadParcelLayer, UploadParcelLayerViewData, UploadParcelLayerViewModel>(viewData, viewModel);
        }
    }
}
