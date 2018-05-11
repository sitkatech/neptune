using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;
using  DetailViewData = Neptune.Web.Views.MaintenanceRecord.DetailViewData;
using  Detail = Neptune.Web.Views.MaintenanceRecord.Detail;

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
        public ViewResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            return ViewNew(
                new EditMaintenanceRecordViewModel
                {
                    MaintenanceRecordDate = DateTime.Now,
                    PerformedByOrganizationID = CurrentPerson.OrganizationID
                }, treatmentBmpPrimaryKey.EntityObject, null);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey, EditMaintenanceRecordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, treatmentBmpPrimaryKey.EntityObject, null);
            }

            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;

            // These values will not be null by this point because they are required on the ViewModel
            var newMaintenanceRecord =
                new MaintenanceRecord(treatmentBmp.TreatmentBMPID, viewModel.MaintenanceRecordDate.Value,
                    viewModel.MaintenanceRecordTypeID.Value, CurrentPerson.PersonID,
                    viewModel.PerformedByOrganizationID.Value);


            HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.Add(newMaintenanceRecord);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            viewModel.UpdateModel(newMaintenanceRecord);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully added.");

            return new RedirectResult(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x=>x.Detail(newMaintenanceRecord)));
        }

        [HttpGet]
        public ViewResult Detail(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, maintenanceRecord);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        private ViewResult ViewNew(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP,
            MaintenanceRecord maintenanceRecord)
        {
            return ViewEdit(viewModel, treatmentBMP, true, maintenanceRecord);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ViewResult Edit(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceRecordViewModel(maintenanceRecord);
            return ViewEdit(viewModel, maintenanceRecord.TreatmentBMP, false, maintenanceRecord);
        }

        [HttpPost]
        [MaintenanceRecordManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey,
            EditMaintenanceRecordViewModel viewModel)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, maintenanceRecord.TreatmentBMP, maintenanceRecord);
            }

            viewModel.UpdateModel(maintenanceRecord);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully edited.");

            return new RedirectResult(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.Detail(maintenanceRecord)));
        }

        private ViewResult ViewEdit(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP, bool isNew,
            MaintenanceRecord maintenanceRecord)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x => x.OrganizationShortName)
                .ToList();
            var viewData = new EditMaintenanceRecordViewData(CurrentPerson, organizations, treatmentBMP, isNew, maintenanceRecord);
            return RazorView<EditMaintenanceRecord, EditMaintenanceRecordViewData,
                EditMaintenanceRecordViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ActionResult Delete(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(maintenanceRecordPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel);
        }

        [HttpPost]
        [MaintenanceRecordManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel);
            }

            maintenanceRecord.DeleteMaintenanceRecord();

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully deleted");
            return new ModalDialogFormJsonResult();
        }

        private ActionResult ViewDelete(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"Are you sure you want to delete this {FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ViewResult EditObservations(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceRecordObservationsViewModel(maintenanceRecord);
            return ViewEditObservations(viewModel, maintenanceRecord.TreatmentBMP, maintenanceRecord);
        }

        private ViewResult ViewEditObservations(EditMaintenanceRecordObservationsViewModel viewModel,
            TreatmentBMP treatmentBMP, MaintenanceRecord maintenanceRecord)
        {
            var viewData = new EditMaintenanceRecordObservationsViewData(CurrentPerson, treatmentBMP, CustomAttributeTypePurpose.Maintenance, maintenanceRecord);
            return RazorView<EditMaintenanceRecordObservations, EditMaintenanceRecordObservationsViewData,
                EditMaintenanceRecordObservationsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [MaintenanceRecordManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditObservations(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey,
            EditMaintenanceRecordObservationsViewModel viewModel)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditObservations(viewModel, maintenanceRecord.TreatmentBMP, maintenanceRecord);
            }

            viewModel.UpdateModel(maintenanceRecord);
            SetMessageForDisplay("Maintenance Record Observations Successfully saved.");
            return RedirectToAction(
                new SitkaRoute<MaintenanceRecordController>(c => c.Detail(maintenanceRecordPrimaryKey)));
        }
    }
}
