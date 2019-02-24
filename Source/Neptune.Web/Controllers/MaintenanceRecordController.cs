using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
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
        // todo: delefate the creation of the GJNJOR to a helper method
        // todo: include before where
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceRecord> MaintenanceRecordsGridJsonData(
            TreatmentBMPPrimaryKey treatmentBmpPrimaryKey)
        {
            var treatmentBmp = treatmentBmpPrimaryKey.EntityObject;
            var gridSpec = new MaintenanceRecordGridSpec(CurrentPerson, treatmentBmp);
            var bmpMaintenanceRecords = treatmentBmp.MaintenanceRecords
                .ToList()
                .OrderByDescending(x => x.GetMaintenanceRecordDate()).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceRecord>(bmpMaintenanceRecords, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceRecord> AllMaintenanceRecordsGridJsonData()
        {
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.Where(x => x.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            var bmpMaintenanceRecords = HttpRequestStorage.DatabaseEntities.MaintenanceRecords
                .Include(x=>x.FieldVisit).Include(x=>x.FieldVisit.PerformedByPerson.Organization).Include(x=>x.TreatmentBMP).Include(x=>x.MaintenanceRecordObservations).Include(x=>x.MaintenanceRecordObservations.Select(y=>y.MaintenanceRecordObservationValues))
                .ToList().Where(x=>x.TreatmentBMP.CanView(CurrentPerson)).ToList();
            var gridSpec = new MaintenanceRecordGridSpec(CurrentPerson, customAttributeTypes);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceRecord>(bmpMaintenanceRecords, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [MaintenanceRecordManageFeature]
        public ViewResult Detail(MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, maintenanceRecord);
            return RazorView<Detail, DetailViewData>(viewData);
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
            maintenanceRecord.DeleteFull(HttpRequestStorage.DatabaseEntities);
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
    }
}
