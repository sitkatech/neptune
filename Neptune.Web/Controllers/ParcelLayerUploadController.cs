using Neptune.Web.Common;
using Neptune.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Views.ParcelLayerUpload;

namespace Neptune.Web.Controllers
{
    public class ParcelLayerUploadController : NeptuneBaseController<ParcelLayerUploadController>
    {
        public ParcelLayerUploadController(NeptuneDbContext dbContext, ILogger<ParcelLayerUploadController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
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

            var viewData = new UploadParcelLayerViewData(HttpContext, _linkGenerator, CurrentPerson, newGisUploadUrl);
            return RazorView<UploadParcelLayer, UploadParcelLayerViewData, UploadParcelLayerViewModel>(viewData, viewModel);
        }
    }
}
