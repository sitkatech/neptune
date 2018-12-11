using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FundingSource;
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
    public class FundingSourceController : NeptuneBaseController
    {
        [FundingSourceViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.FundingSourcesList);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FundingSourceViewFeature]
        public GridJsonNetJObjectResult<FundingSource> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var fundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().OrderBy(ht => ht.FundingSourceName).ToList();
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
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var fundingSource = new FundingSource(
                viewModel.OrganizationID ?? ModelObjectHelpers.NotYetAssignedID, // Should never be null due to View Model validation
                string.Empty,
                true);

            viewModel.UpdateModel(fundingSource, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllFundingSources.Add(fundingSource);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"{FieldDefinition.FundingSource.GetFieldDefinitionLabel()} {fundingSource.GetDisplayNameAsUrl()} succesfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult Edit(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(fundingSource);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FundingSourcePrimaryKey fundingSourcePrimaryKey, EditViewModel viewModel)
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
            var organizationsAsSelectListItems =
                HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations()
                    .ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), x => x.OrganizationName);
            var viewData = new EditViewData(organizationsAsSelectListItems, CurrentPerson);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [FundingSourceViewFeature]
        public ViewResult Detail(FundingSourcePrimaryKey fundingSourcePrimaryKey)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, fundingSource);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [FundingSourceEditFeature]
        public PartialViewResult DeleteFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey)
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
                ? $"Are you sure you want to delete this {FieldDefinition.FundingSource.GetFieldDefinitionLabel()} '{fundingSource.FundingSourceName}'? Deleting this funding source will remove {count} Treatment BMP Funding Event records from the system, totaling {totalAmount}."
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinition.FundingSource.GetFieldDefinitionLabel()}", SitkaRoute<FundingSourceController>.BuildLinkFromExpression(x => x.Detail(fundingSource), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundingSourceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundingSource(FundingSourcePrimaryKey fundingSourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundingSource = fundingSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundingSource(fundingSource, viewModel);
            }
            fundingSource.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

    }
}