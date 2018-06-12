using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.WaterQualityManagementPlanDocument;

namespace Neptune.Web.Controllers
{
    public class WaterQualityManagementPlanDocumentController : NeptuneBaseController
    {
        [HttpGet]
        [WaterQualityManagementPlanManageFeature]
        public PartialViewResult New(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new NewViewModel(waterQualityManagementPlan);
            return ViewNew(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, NewViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            viewModel.UpdateModel(waterQualityManagementPlan, CurrentPerson);
            SetMessageForDisplay($"Successfully created new document \"{viewModel.DisplayName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(NewViewModel viewModel)
        {
            var viewData = new NewViewData();
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [WaterQualityManagementPlanDocumentManageFeature]
        public PartialViewResult Edit(WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(waterQualityManagementPlanDocument);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanDocumentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey, EditViewModel viewModel)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            viewModel.UpdateModel(waterQualityManagementPlanDocument);
            SetMessageForDisplay($"Successfully edited document \"{waterQualityManagementPlanDocument.DisplayName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var viewData = new EditViewData();
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [WaterQualityManagementPlanDocumentManageFeature]
        public PartialViewResult Delete(WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanDocument.PrimaryKey);
            return ViewDelete(waterQualityManagementPlanDocument, viewModel);
        }

        [HttpPost]
        [WaterQualityManagementPlanDocumentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(waterQualityManagementPlanDocument, viewModel);
            }
            
            waterQualityManagementPlanDocument.DeleteFull();
            SetMessageForDisplay($"Successfully deleted \"{waterQualityManagementPlanDocument.DisplayName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDelete(WaterQualityManagementPlanDocument waterQualityManagementPlanDocument, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to delete \"{waterQualityManagementPlanDocument.DisplayName}\"?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}
