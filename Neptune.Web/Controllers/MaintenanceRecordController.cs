using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class MaintenanceRecordController : NeptuneBaseController<MaintenanceRecordController>
    {
        public MaintenanceRecordController(NeptuneDbContext dbContext, ILogger<MaintenanceRecordController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceRecord> AllMaintenanceRecordsGridJsonData()
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var customAttributeTypes = CustomAttributeTypes.List(_dbContext).Where(x => x.CustomAttributeTypePurposeID == (int) CustomAttributeTypePurposeEnum.Maintenance);
            var maintenanceRecords = MaintenanceRecords.List(_dbContext)
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
            var maintenanceRecord = MaintenanceRecords.GetByID(_dbContext, maintenanceRecordPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, maintenanceRecord.TreatmentBMPTypeID);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, maintenanceRecord, treatmentBMPType);
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
