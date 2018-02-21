using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceActivity;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class MaintenanceActivityController : NeptuneBaseController
    {
        [TreatmentBMPManageFeature]
        public GridJsonNetJObjectResult<MaintenanceActivity> MaintenanceActivitysGridJsonData(
            TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var gridSpec = new MaintenanceActivityGridSpec(CurrentPerson, treatmentBmp);
            var bmpMaintenanceActivitys = treatmentBmp.MaintenanceActivities.ToList()
                .OrderByDescending(x => x.MaintenanceActivityDate).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceActivity>(bmpMaintenanceActivitys, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            return ViewNew(new EditMaintenanceActivityViewModel());
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey, EditMaintenanceActivityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var newMaintenanceActivity = new MaintenanceActivity(treatmentBmp.TreatmentBMPID,viewModel.MaintenanceActivityDate.Value, viewModel.PerformedByPersonID.Value, viewModel.MaintenanceActivityTypeID.Value);
            

            HttpRequestStorage.DatabaseEntities.AllMaintenanceActivities.Add(newMaintenanceActivity);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            viewModel.UpdateModel(newMaintenanceActivity);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceActivity.GetFieldDefinitionLabel()} successfully added.");

            return new ModalDialogFormJsonResult();
        }

        //todo : possibly an unnecessary abstraction
        private PartialViewResult ViewNew(EditMaintenanceActivityViewModel viewModel)
        {
            return ViewEdit(viewModel);
        }

        [HttpGet]
        [MaintenanceActivityManageFeature]
        public PartialViewResult Edit(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey)
        {
            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceActivityViewModel(maintenanceActivity);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [MaintenanceActivityManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey,
            EditMaintenanceActivityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            viewModel.UpdateModel(maintenanceActivity);
            
            SetMessageForDisplay($"{FieldDefinition.MaintenanceActivity.GetFieldDefinitionLabel()} successfully edited.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditMaintenanceActivityViewModel viewModel)
        {
            var persons = HttpRequestStorage.DatabaseEntities.People.OrderBy(x=>x.LastName).ToList();
            var viewData = new EditMaintenanceActivityViewData(persons);
            return RazorPartialView<EditMaintenanceActivity, EditMaintenanceActivityViewData,
                EditMaintenanceActivityViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [MaintenanceActivityManageFeature]
        public ActionResult Delete(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(maintenanceActivityPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel);
        }

        [HttpPost]
        [MaintenanceActivityManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(MaintenanceActivityPrimaryKey maintenanceActivityPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel);
            }
            maintenanceActivity.DeleteMaintenanceActivity();

            SetMessageForDisplay($"{FieldDefinition.MaintenanceActivity.GetFieldDefinitionLabel()} successfully deleted");
            return new ModalDialogFormJsonResult();
        }

        private ActionResult ViewDelete(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.MaintenanceActivity.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}
