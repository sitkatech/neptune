using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPImage;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPImageController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMP);
            return ViewEditTreatmentBMPImages(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentBMPImages(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP);

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditTreatmentBMPImages(TreatmentBMP treatmentBMP, EditViewModel viewModel)
        {
            var viewData = new EditViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var viewModel = new NewViewModel();
            return ViewNewTreatmentBMPImage(viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNewTreatmentBMPImage(viewModel);
            }

            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewTreatmentBMPImage(NewViewModel viewModel)
        {
            var viewData = new NewViewData(CurrentPerson);
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }
    }
}