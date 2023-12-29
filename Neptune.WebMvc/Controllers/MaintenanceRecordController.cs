using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.MaintenanceRecord;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Controllers
{
    public class MaintenanceRecordController : NeptuneBaseController<MaintenanceRecordController>
    {
        public MaintenanceRecordController(NeptuneDbContext dbContext, ILogger<MaintenanceRecordController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<vMaintenanceRecordDetailed> AllMaintenanceRecordsGridJsonData()
        {
            var maintenanceRecords = vMaintenanceRecordDetaileds.ListViewableByPerson(_dbContext, CurrentPerson);
            var gridSpec = new MaintenanceRecordGridSpec(CurrentPerson, _linkGenerator);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<vMaintenanceRecordDetailed>(maintenanceRecords, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{maintenanceRecordPrimaryKey}")]
        [MaintenanceRecordManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("maintenanceRecordPrimaryKey")]
        public ViewResult Detail([FromRoute] MaintenanceRecordPrimaryKey maintenanceRecordPrimaryKey)
        {
            var maintenanceRecord = MaintenanceRecords.GetByID(_dbContext, maintenanceRecordPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, maintenanceRecord.TreatmentBMPTypeID);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, maintenanceRecord, treatmentBMPType);
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
            await maintenanceRecord.DeleteFull(_dbContext);

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
