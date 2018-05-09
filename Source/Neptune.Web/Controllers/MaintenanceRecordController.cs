using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.EditAttributes;

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
            return ViewNew(new EditMaintenanceRecordViewModel(), treatmentBmpPrimaryKey.EntityObject);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBmpPrimaryKey, EditMaintenanceRecordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, treatmentBmpPrimaryKey.EntityObject);
            }

            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
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

        private ViewResult ViewNew(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            return ViewEdit(viewModel, treatmentBMP, true);
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ViewResult Edit(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewModel = new EditMaintenanceRecordViewModel(maintenanceRecord);
            return ViewEdit(viewModel, maintenanceRecord.TreatmentBMP, false);
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
                return ViewNew(viewModel, maintenanceRecord.TreatmentBMP);
            }

            viewModel.UpdateModel(maintenanceRecord);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully edited.");

            return new RedirectResult(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.Detail(maintenanceRecord)));
        }

        private ViewResult ViewEdit(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP, bool isNew)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x => x.OrganizationShortName)
                .ToList();
            var viewData = new EditMaintenanceRecordViewData(CurrentPerson, organizations, treatmentBMP, isNew);
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
            return ViewEditObservations(viewModel, maintenanceRecord.TreatmentBMP);
        }

        private ViewResult ViewEditObservations(EditMaintenanceRecordObservationsViewModel viewModel,
            TreatmentBMP treatmentBMP)
        {
            var viewData = new EditMaintenanceRecordObservationsViewData(CurrentPerson, treatmentBMP, CustomAttributeTypePurpose.Maintenance);
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
                return ViewEditObservations(viewModel, maintenanceRecord.TreatmentBMP);
            }

            viewModel.UpdateModel(maintenanceRecord);
            SetMessageForDisplay("Maintenance Record Observations Successfully saved.");
            return RedirectToAction(
                new SitkaRoute<MaintenanceRecordController>(c => c.Detail(maintenanceRecordPrimaryKey)));
        }
    }

}

namespace Neptune.Web.Views.MaintenanceRecord
{
    public abstract class Detail : TypedWebViewPage<DetailViewData>
    {

    }

    public class DetailViewData : NeptuneViewData
    {
        public DetailViewData(Person currentPerson, Models.MaintenanceRecord maintenanceRecord) : base(currentPerson)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = maintenanceRecord.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = maintenanceRecord.TreatmentBMP.GetDetailUrl();
            PageTitle = $"Maintenance Record Detail";
            EditUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.Edit(maintenanceRecord));
            EditObservationsUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.EditObservations(maintenanceRecord));
            MaintenanceRecord = maintenanceRecord;
            CurrentPersonCanManage = new MaintenanceRecordManageFeature().HasPermissionByPerson(currentPerson);
            BMPTypeHasObservationTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
        }

        public string EditObservationsUrl { get; }

        public string EditUrl { get; }
        public Models.MaintenanceRecord MaintenanceRecord { get; }
        public bool CurrentPersonCanManage { get; }
        public bool BMPTypeHasObservationTypes { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; set; }
    }

    public abstract class EditMaintenanceRecordObservations : TypedWebViewPage<EditMaintenanceRecordObservationsViewData, EditMaintenanceRecordObservationsViewModel>
    {

    }

    public class EditMaintenanceRecordObservationsViewData : EditAttributesViewData
    {
        public EditMaintenanceRecordObservationsViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, CustomAttributeTypePurpose customAttributeTypePurpose) : base(currentPerson, treatmentBMP, customAttributeTypePurpose)
        {
            PageTitle = $"Edit Maintenance Record Observations";
            // todo: set ParentDetailUrl
        }
    }

    public class EditMaintenanceRecordObservationsViewModel : EditAttributesViewModel
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceRecordObservationsViewModel()
        {

        }

        public EditMaintenanceRecordObservationsViewModel(Models.MaintenanceRecord maintenanceRecord)
        {
            CustomAttributes =
                maintenanceRecord.MaintenanceRecordObservations.Select(x => new CustomAttributeSimple(x)).ToList();
        }

        public void UpdateModel(Models.MaintenanceRecord maintenanceRecord)
        {

            var treatmentBMPTypeCustomAttributeTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var customAttributeSimplesWithValues = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var customAttributesToUpdate = new List<MaintenanceRecordObservation>();
            var customAttributeValuesToUpdate = new List<MaintenanceRecordObservationValue>();
            foreach (var x in customAttributeSimplesWithValues)
            {

                var customAttribute = new MaintenanceRecordObservation(maintenanceRecord.MaintenanceRecordID,
                    treatmentBMPTypeCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID)
                        .TreatmentBMPTypeCustomAttributeTypeID, maintenanceRecord.TreatmentBMP.TreatmentBMPTypeID,
                    x.CustomAttributeTypeID);
                customAttributesToUpdate.Add(customAttribute);

                foreach (var value in x.CustomAttributeValues)
                {
                    var customAttributeValue = new MaintenanceRecordObservationValue(customAttribute,value);
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            var maintenanceRecordObservationsInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.Local;
            var maintenanceRecordObservationValuesInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservationValues.Local;

            var existingMaintenanceRecordObservations = maintenanceRecord.MaintenanceRecordObservations.Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();

            var existingMaintenanceRecordObservationValues = existingMaintenanceRecordObservations.SelectMany(x => x.MaintenanceRecordObservationValues).ToList();

            existingMaintenanceRecordObservations.Merge(customAttributesToUpdate, maintenanceRecordObservationsInDatabase,
                (x, y) => x.MaintenanceRecordID == y.MaintenanceRecordID
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID
                          && x.CustomAttributeTypeID == y.CustomAttributeTypeID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { });

            existingMaintenanceRecordObservationValues.Merge(customAttributeValuesToUpdate, maintenanceRecordObservationValuesInDatabase,
                (x, y) => x.MaintenanceRecordObservationValueID == y.MaintenanceRecordObservationValueID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { x.ObservationValue = y.ObservationValue; });
        }
    }
}
