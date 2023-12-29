using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Shared;
using Detail = Neptune.WebMvc.Views.FundingSource.Detail;
using DetailViewData = Neptune.WebMvc.Views.FundingSource.DetailViewData;
using Edit = Neptune.WebMvc.Views.FundingSource.Edit;
using EditViewData = Neptune.WebMvc.Views.FundingSource.EditViewData;
using EditViewModel = Neptune.WebMvc.Views.FundingSource.EditViewModel;
using Index = Neptune.WebMvc.Views.FundingSource.Index;
using IndexGridSpec = Neptune.WebMvc.Views.FundingSource.IndexGridSpec;
using IndexViewData = Neptune.WebMvc.Views.FundingSource.IndexViewData;

namespace Neptune.WebMvc.Controllers
{
    public class FundingSourceController : NeptuneBaseController<FundingSourceController>
    {
        public FundingSourceController(NeptuneDbContext dbContext, ILogger<FundingSourceController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FundingSourcesList);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        public GridJsonNetJObjectResult<FundingSource> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson, _linkGenerator);
            var fundingSources = FundingSources.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundingSource>(fundingSources, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FundingSourceCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel
            {
                // If the person is not an admin, we want to default the Funding Source organization to their own Organization
                OrganizationID = new List<Role> { Role.Admin, Role.SitkaAdmin }.Any(x => x.RoleID == CurrentPerson.RoleID)
                    ? (int?)null
                    : CurrentPerson.OrganizationID,
                IsActive = true
            };

            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FundingSourceCreateFeature]
        public async Task<IActionResult> New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var fundingSource = new FundingSource { IsActive = true };
            viewModel.UpdateModel(fundingSource, CurrentPerson);
            await _dbContext.FundingSources.AddAsync(fundingSource);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()} {UrlTemplate.MakeHrefString(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(fundingSource)), fundingSource.FundingSourceName)} successfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public PartialViewResult Edit([FromRoute]FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(fundingSource);
            return ViewEdit(viewModel);
        }

        [HttpPost("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            viewModel.UpdateModel(fundingSource, CurrentPerson);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var organizationsAsSelectListItems = Organizations.ListActive(_dbContext)
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                    x => x.OrganizationName);
            var viewData = new EditViewData(organizationsAsSelectListItems, CurrentPerson);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{fundingSourcePrimaryKey}")]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public ViewResult Detail([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = FundingSources.GetByID(_dbContext, fundingSourcePrimaryKey);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, fundingSource);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public PartialViewResult Delete([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = FundingSources.GetByID(_dbContext, fundingSourcePrimaryKey);
            var viewModel = new ConfirmDialogFormViewModel(fundingSource.FundingSourceID);
            return ViewDeleteFundingSource(fundingSource, viewModel);
        }

        private PartialViewResult ViewDeleteFundingSource(FundingSource fundingSource, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = true;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()} '{fundingSource.FundingSourceName}'? Deleting this funding source will remove {fundingSource.FundingEventFundingSources.Count} Treatment BMP Funding Event records from the system, totaling {fundingSource.FundingEventFundingSources.Sum(x => x.Amount).ToStringCurrency()}."
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()}",  UrlTemplate.MakeHrefString(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(fundingSource)), "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundingSource = FundingSources.GetByIDWithChangeTracking(_dbContext, fundingSourcePrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundingSource(fundingSource, viewModel);
            }

            await fundingSource.DeleteFull(_dbContext);
            return new ModalDialogFormJsonResult();
        }
    }
}