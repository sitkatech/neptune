using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class MaintenanceRecordController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceRecord> MaintenanceRecordsGridJsonData(
            TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var gridSpec = new MaintenanceRecordGridSpec(CurrentPerson, treatmentBmp);
            var bmpMaintenanceRecords = treatmentBmp.MaintenanceRecords.ToList()
                .OrderByDescending(x => x.MaintenanceRecordDate).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceRecord>(bmpMaintenanceRecords, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            return ViewNew(new EditMaintenanceRecordViewModel());
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey, EditMaintenanceRecordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var newMaintenanceRecord = new MaintenanceRecord(treatmentBmp.TreatmentBMPID,viewModel.MaintenanceRecordDate.Value, viewModel.MaintenanceRecordTypeID.Value);
            

            HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.Add(newMaintenanceRecord);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            viewModel.UpdateModel(newMaintenanceRecord);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully added.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(EditMaintenanceRecordViewModel viewModel)
        {
            return ViewEdit(viewModel);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public PartialViewResult Edit(MaintenanceRecordPrimaryKey maintenanceActivityPrimaryKey)
        {
            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceRecordViewModel(maintenanceActivity);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [MaintenanceRecordManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(MaintenanceRecordPrimaryKey maintenanceActivityPrimaryKey,
            EditMaintenanceRecordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            viewModel.UpdateModel(maintenanceActivity);
            
            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully edited.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditMaintenanceRecordViewModel viewModel)
        {
            var persons = HttpRequestStorage.DatabaseEntities.People.OrderBy(x=>x.LastName).ToList();
            var viewData = new EditMaintenanceRecordViewData(persons);
            return RazorPartialView<EditMaintenanceRecord, EditMaintenanceRecordViewData,
                EditMaintenanceRecordViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ActionResult Delete(MaintenanceRecordPrimaryKey maintenanceActivityPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(maintenanceActivityPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel);
        }

        [HttpPost]
        [MaintenanceRecordManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(MaintenanceRecordPrimaryKey maintenanceActivityPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var maintenanceActivity = maintenanceActivityPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel);
            }
            maintenanceActivity.DeleteMaintenanceRecord();

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully deleted");
            return new ModalDialogFormJsonResult();
        }

        private ActionResult ViewDelete(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}
