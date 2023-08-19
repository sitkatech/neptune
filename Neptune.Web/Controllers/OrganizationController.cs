/*-----------------------------------------------------------------------
<copyright file="OrganizationController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Globalization;
using LtInfo.Common.Mvc;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Organization;
using Neptune.Web.Views.Shared;
using Index = Neptune.Web.Views.Organization.Index;
using IndexGridSpec = Neptune.Web.Views.Organization.IndexGridSpec;
using IndexViewData = Neptune.Web.Views.Organization.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class OrganizationController : NeptuneBaseController<OrganizationController>
    {
        public OrganizationController(NeptuneDbContext dbContext, ILogger<OrganizationController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet]
        [OrganizationManageFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.OrganizationsList);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, _linkGenerator);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [OrganizationManageFeature]
        public GridJsonNetJObjectResult<Organization> IndexGridJsonData()
        {
            var hasDeleteOrganizationPermission = new OrganizationManageFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new IndexGridSpec(CurrentPerson, hasDeleteOrganizationPermission, _linkGenerator);
            var organizations = Organizations.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Organization>(organizations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel { IsActive = true };
            return ViewEdit(viewModel, false, null);
        }

        [HttpPost]
        [OrganizationManageFeature]
        public async Task<IActionResult> New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, true, null);
            }

            var organization = new Organization()
            {
                IsActive = true
            };
            viewModel.UpdateModel(organization, CurrentPerson);
            _dbContext.Organizations.Add(organization);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Organization {organization.GetDisplayName()} successfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{organizationPrimaryKey}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public PartialViewResult Edit([FromRoute] OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = Organizations.GetByID(_dbContext, organizationPrimaryKey);
            var viewModel = new EditViewModel(organization);
            return ViewEdit(viewModel, organization.IsInKeystone(), organization.PrimaryContactPerson);
        }

        [HttpPost("{organizationPrimaryKey}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] OrganizationPrimaryKey organizationPrimaryKey, [FromForm] EditViewModel viewModel)
        {
            var organization = Organizations.GetByIDWithChangeTracking(_dbContext, organizationPrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, organization.IsInKeystone(), organization.PrimaryContactPerson);
            }
            viewModel.UpdateModel(organization, CurrentPerson);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, bool isInKeystone, Person currentPrimaryContactPerson)
        {
            var organizationTypesAsSelectListItems = _dbContext.OrganizationTypes
                .OrderBy(x => x.OrganizationTypeName)
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.OrganizationTypeName);
            var activePeople = People.ListActive(_dbContext).ToList();
            if (currentPrimaryContactPerson != null && !activePeople.Contains(currentPrimaryContactPerson))
            {
                activePeople.Add(currentPrimaryContactPerson);
            }
            var people = activePeople.OrderBy(x => x.GetFullNameLastFirst()).ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.GetFullNameFirstLastAndOrg());
            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var requestOrganizationChangeUrl = "";//todo: SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.RequestOrganizationNameChange());
            var viewData = new EditViewData(organizationTypesAsSelectListItems, people, isInKeystone, requestOrganizationChangeUrl, isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{organizationPrimaryKey}")]
        [OrganizationViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public ViewResult Detail([FromRoute] OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = Organizations.GetByID(_dbContext, organizationPrimaryKey);
            var viewData = new DetailViewData(CurrentPerson, organization, _linkGenerator);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{organizationPrimaryKey}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationID")]
        public PartialViewResult DeleteOrganization([FromRoute] OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(organization.OrganizationID);
            return ViewDeleteOrganization(organization, viewModel);
        }

        private PartialViewResult ViewDeleteOrganization(Organization organization, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !organization.IsUnknown() /*&& !organization.HasDependentObjects()*/;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinitionType.Organization.GetFieldDefinitionLabel()} '{organization.OrganizationName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}", UrlTemplate.MakeHrefString(
                    SitkaRoute<OrganizationController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(organization)), "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{organizationID}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationID")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] OrganizationPrimaryKey organizationPrimaryKey, [FromForm] ConfirmDialogFormViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteOrganization(organization, viewModel);
            }

            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        //[HttpGet]
        //[SitkaAdminFeature]
        //public PartialViewResult PullOrganizationFromKeystone()
        //{
        //    var viewModel = new PullOrganizationFromKeystoneViewModel();

        //    return ViewPullOrganizationFromKeystone(viewModel);
        //}

        //[HttpPost]
        //[SitkaAdminFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult PullOrganizationFromKeystone(PullOrganizationFromKeystoneViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewPullOrganizationFromKeystone(viewModel);
        //    }

        //    var keystoneClient = new KeystoneDataClient();

        //    var organizationGuid = viewModel.OrganizationGuid.GetValueOrDefault(); // never null due to RequiredAttribute
        //    KeystoneDataService.Organization keystoneOrganization;
        //    try
        //    {
        //        keystoneOrganization = keystoneClient.GetOrganization(organizationGuid);
        //    }
        //    catch (Exception)
        //    {
        //        SetErrorForDisplay("Organization not added. Guid not found in Keystone or unable to connect to Keystone");
        //        return new ModalDialogFormJsonResult();
        //    }

        //    var neptuneOrganization = HttpRequestStorage.DatabaseEntities.Organizations.SingleOrDefault(x => x.OrganizationGuid == organizationGuid);
        //    if (neptuneOrganization != null)
        //    {
        //        SetErrorForDisplay("Organization not added - it already exists in Neptune");
        //        return new ModalDialogFormJsonResult();
        //    }

        //    var defaultOrganizationType = HttpRequestStorage.DatabaseEntities.OrganizationTypes.GetDefaultOrganizationType();
        //    neptuneOrganization = new Organization(keystoneOrganization.FullName, true, defaultOrganizationType)
        //    {
        //        OrganizationGuid = keystoneOrganization.OrganizationGuid,
        //        OrganizationShortName = keystoneOrganization.ShortName,
        //        OrganizationUrl = keystoneOrganization.URL
        //    };
        //    HttpRequestStorage.DatabaseEntities.Organizations.Add(neptuneOrganization);

        //    HttpRequestStorage.DatabaseEntities.SaveChanges();

        //    SetMessageForDisplay($"Organization {neptuneOrganization.GetDisplayNameAsUrl()} successfully added.");

        //    return new ModalDialogFormJsonResult();
        //}

        //private PartialViewResult ViewPullOrganizationFromKeystone(PullOrganizationFromKeystoneViewModel viewModel)
        //{
        //    var viewData = new PullOrganizationFromKeystoneViewData();
        //    return RazorPartialView<PullOrganizationFromKeystone, PullOrganizationFromKeystoneViewData, PullOrganizationFromKeystoneViewModel>(viewData, viewModel);
        //}
    }
}
