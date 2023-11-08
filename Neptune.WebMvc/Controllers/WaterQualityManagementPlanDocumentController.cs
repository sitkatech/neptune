using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.WaterQualityManagementPlanDocument;

namespace Neptune.WebMvc.Controllers
{
    public class WaterQualityManagementPlanDocumentController : NeptuneBaseController<WaterQualityManagementPlanDocumentController>
    {
        private readonly FileResourceService _fileResourceService;

        public WaterQualityManagementPlanDocumentController(NeptuneDbContext dbContext, ILogger<WaterQualityManagementPlanDocumentController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
        }

        [HttpGet("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public PartialViewResult New([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            var viewModel = new NewViewModel(waterQualityManagementPlan);
            return ViewNew(viewModel);
        }

        [HttpPost("{waterQualityManagementPlanPrimaryKey}")]
        [WaterQualityManagementPlanManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanPrimaryKey")]
        public async Task<IActionResult> New([FromRoute] WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey, NewViewModel viewModel)
        {
            var waterQualityManagementPlan = waterQualityManagementPlanPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            await viewModel.UpdateModel(waterQualityManagementPlan, CurrentPerson, _dbContext, _fileResourceService);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Successfully created new document \"{viewModel.DisplayName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(NewViewModel viewModel)
        {
            var allDocumentTypes = WaterQualityManagementPlanDocumentType.All.ToSelectListWithDisabledEmptyFirstRow(x=>x.WaterQualityManagementPlanDocumentTypeID.ToString(), x=>x.WaterQualityManagementPlanDocumentTypeDisplayName);
            var viewData = new NewViewData(allDocumentTypes);
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanDocumentPrimaryKey}")]
        [WaterQualityManagementPlanDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanDocumentPrimaryKey")]
        public PartialViewResult Edit([FromRoute] WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(waterQualityManagementPlanDocument);
            return ViewEdit(viewModel);
        }

        [HttpPost("{waterQualityManagementPlanDocumentPrimaryKey}")]
        [WaterQualityManagementPlanDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanDocumentPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey, EditViewModel viewModel)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            viewModel.UpdateModel(waterQualityManagementPlanDocument);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Successfully edited document \"{waterQualityManagementPlanDocument.DisplayName}\".");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var allDocumentTypes = WaterQualityManagementPlanDocumentType.All.ToSelectListWithDisabledEmptyFirstRow(x => x.WaterQualityManagementPlanDocumentTypeID.ToString(), x => x.WaterQualityManagementPlanDocumentTypeDisplayName);
            var viewData = new EditViewData(allDocumentTypes);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{waterQualityManagementPlanDocumentPrimaryKey}")]
        [WaterQualityManagementPlanDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanDocumentPrimaryKey")]
        public PartialViewResult Delete([FromRoute] WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(waterQualityManagementPlanDocument.PrimaryKey);
            return ViewDelete(waterQualityManagementPlanDocument, viewModel);
        }

        [HttpPost("{waterQualityManagementPlanDocumentPrimaryKey}")]
        [WaterQualityManagementPlanDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("waterQualityManagementPlanDocumentPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] WaterQualityManagementPlanDocumentPrimaryKey waterQualityManagementPlanDocumentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(waterQualityManagementPlanDocument, viewModel);
            }
            
            await waterQualityManagementPlanDocument.DeleteFull(_dbContext);
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
