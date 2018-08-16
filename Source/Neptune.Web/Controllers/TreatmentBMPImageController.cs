using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
using Neptune.Web.Views.TreatmentBMPImage;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPImageController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public ViewResult ManageTreatmentBMPImages(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ManageTreatmentBMPImagesViewModel(treatmentBMP);
            return ViewManageTreatmentBMPImages(viewModel, treatmentBMP);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ManageTreatmentBMPImages(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ManageTreatmentBMPImagesViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewManageTreatmentBMPImages(viewModel, treatmentBMP);
            }

            treatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModels(CurrentPerson, treatmentBMP);
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");

            return Redirect(
                SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c =>
                    c.ManageTreatmentBMPImages(treatmentBMP)));
        }

        private ViewResult ViewManageTreatmentBMPImages(ManageTreatmentBMPImagesViewModel viewModel,
            TreatmentBMP treatmentBMP)
        {
            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(CurrentPerson, treatmentBMP);
            var viewData = new ManageTreatmentBMPImagesViewData(CurrentPerson, treatmentBMP, managePhotosWithPreviewViewData);
            return RazorView<ManageTreatmentBMPImages, ManageTreatmentBMPImagesViewData, ManageTreatmentBMPImagesViewModel>(viewData, viewModel);
        }
    }
}
