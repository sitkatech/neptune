using System.Globalization;
using LtInfo.Common.Mvc;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared;
using Detail = Neptune.Web.Views.FundingSource.Detail;
using DetailViewData = Neptune.Web.Views.FundingSource.DetailViewData;
using Edit = Neptune.Web.Views.FundingSource.Edit;
using EditViewData = Neptune.Web.Views.FundingSource.EditViewData;
using EditViewModel = Neptune.Web.Views.FundingSource.EditViewModel;
using Index = Neptune.Web.Views.FundingSource.Index;
using IndexGridSpec = Neptune.Web.Views.FundingSource.IndexGridSpec;
using IndexViewData = Neptune.Web.Views.FundingSource.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class FundingSourceController : NeptuneBaseController<FundingSourceController>
    {
        public FundingSourceController(NeptuneDbContext dbContext, ILogger<FundingSourceController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FundingSourcesList);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, _linkGenerator, HttpContext);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
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
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var fundingSource = new FundingSource { IsActive = true };
            viewModel.UpdateModel(fundingSource, CurrentPerson);
            _dbContext.FundingSources.Add(fundingSource);
            _dbContext.SaveChangesAsync();
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
        public ActionResult Edit([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            viewModel.UpdateModel(fundingSource, CurrentPerson);
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
        [AnonymousUnclassifiedFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public ViewResult Detail([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = FundingSources.GetByID(_dbContext, fundingSourcePrimaryKey);
            var viewData = new DetailViewData(CurrentPerson, fundingSource, _linkGenerator, HttpContext);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public PartialViewResult Delete([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fundingSource.FundingSourceID);
            return ViewDeleteFundingSource(fundingSource, viewModel);
        }

        private PartialViewResult ViewDeleteFundingSource(FundingSource fundingSource, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = true;
            var count = fundingSource.FundingEventFundingSources.Count;
            var totalAmount = fundingSource.FundingEventFundingSources.Sum(x => x.Amount).ToStringCurrency();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()} '{fundingSource.FundingSourceName}'? Deleting this funding source will remove {count} Treatment BMP Funding Event records from the system, totaling {totalAmount}."
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()}",  UrlTemplate.MakeHrefString(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(fundingSource)), "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{fundingSourcePrimaryKey}")]
        [FundingSourceEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("fundingSourcePrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] FundingSourcePrimaryKey fundingSourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundingSource(fundingSource, viewModel);
            }

            _dbContext.FundingSources.Remove(fundingSource);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }
    }
}