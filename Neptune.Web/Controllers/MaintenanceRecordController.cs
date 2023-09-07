using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class MaintenanceRecordController : NeptuneBaseController<MaintenanceRecordController>
    {
        public MaintenanceRecordController(NeptuneDbContext dbContext, ILogger<MaintenanceRecordController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceRecord> AllMaintenanceRecordsGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanViewWithContext(_dbContext);
            var customAttributeTypes = _dbContext.CustomAttributeTypes.Include(x => x.TreatmentBMPTypeCustomAttributeTypes).Where(x => x.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();
            var maintenanceRecords = _dbContext.MaintenanceRecords
                .Include(x => x.FieldVisit)
                .Include(x => x.FieldVisit.PerformedByPerson)
                .Include(x => x.TreatmentBMP)
                .Include(x => x.TreatmentBMP.StormwaterJurisdiction)
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.MaintenanceRecordObservations)
                .Include(x => x.MaintenanceRecordObservations.Select(y => y.MaintenanceRecordObservationValues))
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.TreatmentBMP.StormwaterJurisdictionID)).ToList();
            var gridSpec = new MaintenanceRecordGridSpec(CurrentPerson, customAttributeTypes, _linkGenerator);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<MaintenanceRecord>(maintenanceRecords, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{maintenanceRecordPrimaryKey}")]
        [MaintenanceRecordManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("maintenanceRecordPrimaryKey")]
        public ViewResult Detail([FromRoute] MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, maintenanceRecord);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{maintenanceRecordPrimaryKey}")]
        [MaintenanceRecordManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("maintenanceRecordPrimaryKey")]
        public ActionResult Delete([FromRoute] MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(maintenanceRecordPrimaryKey.PrimaryKeyValue);
            return ViewDelete(viewModel);
        }

        [HttpPost("{maintenanceRecordPrimaryKey}")]
        [MaintenanceRecordManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("maintenanceRecordPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var maintenanceRecord = maintenanceRecordPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(viewModel);
            }
            maintenanceRecord.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay($"{FieldDefinitionType.MaintenanceRecord.GetFieldDefinitionLabel()} successfully deleted");
            return new ModalDialogFormJsonResult();
        }

        private ActionResult ViewDelete(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"Are you sure you want to delete this {FieldDefinitionType.MaintenanceRecord.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}
