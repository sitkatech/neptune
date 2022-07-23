using Neptune.Web.Common;
using Neptune.Web.Security;
using System.Web.Mvc;
using Neptune.Web.Views.ParcelLayerUpload;

namespace Neptune.Web.Controllers
{
    public class ParcelLayerUploadController : NeptuneBaseController
    {

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UpdateParcelLayerGeometry()
        {
            var viewModel = new UploadParcelLayerViewModel { PersonID = CurrentPerson.PersonID };
            return ViewUpdateParcelLayerGeometry(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateParcelLayerGeometry(viewModel);
            }

            viewModel.UpdateModel(CurrentPerson);

            SetMessageForDisplay("Parcels were successfully added to the staging area. The staged Parcels will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

            return Redirect(SitkaRoute<ParcelController>.BuildUrlFromExpression(c => c.Index()));
        }

        private ViewResult ViewUpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<ParcelLayerUploadController>.BuildUrlFromExpression(c => c.UpdateParcelLayerGeometry());

            var viewData = new UploadParcelLayerViewData(CurrentPerson, newGisUploadUrl);
            return RazorView<UploadParcelLayer, UploadParcelLayerViewData, UploadParcelLayerViewModel>(viewData, viewModel);
        }
    }
}
