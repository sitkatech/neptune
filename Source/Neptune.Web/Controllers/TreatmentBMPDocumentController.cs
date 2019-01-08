using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPDocument;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPDocumentController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPManageFeature]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var viewModel = new NewViewModel();
            return ViewNewTreatmentBMPDocument(viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewTreatmentBMPDocument(viewModel);
            }
            var treatmentBMPDocument = new TreatmentBMPDocument(treatmentBMP);
            viewModel.UpdateModel(treatmentBMPDocument, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.TreatmentBMPDocuments.Add(treatmentBMPDocument);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewTreatmentBMPDocument(NewViewModel viewModel)
        {
            var viewData = new NewViewData();
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPDocumentManageFeature]
        public PartialViewResult Delete(TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPDocument.TreatmentBMPID);
            return ViewDelete(treatmentBMPDocument, viewModel);
        }

        private PartialViewResult ViewDelete(TreatmentBMPDocument treatmentBMPDocument, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !treatmentBMPDocument.HasDependentObjects();
            var confirmMessage = canDelete
                ? "Are you sure you want to delete this Treatment BMP Document?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP Document",
                    SitkaRoute<TreatmentBMPController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPDocument.TreatmentBMPID), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [TreatmentBMPDocumentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(treatmentBMPDocument, viewModel);
            }
            HttpRequestStorage.DatabaseEntities.TreatmentBMPDocuments.Remove(treatmentBMPDocument);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [TreatmentBMPDocumentManageFeature]
        public ActionResult Edit(TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMPDocument);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [TreatmentBMPDocumentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(treatmentBMPDocument, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var viewData = new EditViewData();
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

    }
}