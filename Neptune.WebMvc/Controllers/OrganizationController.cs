﻿/*-----------------------------------------------------------------------
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.Organization;
using Neptune.WebMvc.Views.Shared;
using Index = Neptune.WebMvc.Views.Organization.Index;
using IndexGridSpec = Neptune.WebMvc.Views.Organization.IndexGridSpec;
using IndexViewData = Neptune.WebMvc.Views.Organization.IndexViewData;

namespace Neptune.WebMvc.Controllers
{
    public class OrganizationController : NeptuneBaseController<OrganizationController>
    {
        private readonly FileResourceService _fileResourceService;

        public OrganizationController(NeptuneDbContext dbContext,
            ILogger<OrganizationController> logger,
            IOptions<WebConfiguration> webConfiguration,
            LinkGenerator linkGenerator,
            FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
        }

        [HttpGet]
        [OrganizationManageFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.OrganizationsList);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
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
            await viewModel.UpdateModel(organization, CurrentPerson, _fileResourceService);
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
            await viewModel.UpdateModel(organization, CurrentPerson, _fileResourceService);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, bool isInKeystone, Person currentPrimaryContactPerson)
        {
            var organizationTypesAsSelectListItems = OrganizationTypes.List(_dbContext)
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
            var requestOrganizationChangeUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(_linkGenerator, x => x.RequestOrganizationNameChange());
            var viewData = new EditViewData(organizationTypesAsSelectListItems, people, isInKeystone, requestOrganizationChangeUrl, isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{organizationPrimaryKey}")]
        [OrganizationViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public ViewResult Detail([FromRoute] OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = Organizations.GetByID(_dbContext, organizationPrimaryKey);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, organization);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{organizationPrimaryKey}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public PartialViewResult Delete([FromRoute] OrganizationPrimaryKey organizationPrimaryKey)
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

        [HttpPost("{organizationPrimaryKey}")]
        [OrganizationManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("organizationPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] OrganizationPrimaryKey organizationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteOrganization(organization, viewModel);
            }

            await organization.DeleteFull(_dbContext);
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

        //    var neptuneOrganization = _dbContext.Organizations.SingleOrDefault(x => x.OrganizationGuid == organizationGuid);
        //    if (neptuneOrganization != null)
        //    {
        //        SetErrorForDisplay("Organization not added - it already exists in Neptune");
        //        return new ModalDialogFormJsonResult();
        //    }

        //    var defaultOrganizationType = _dbContext.OrganizationTypes.GetDefaultOrganizationType();
        //    neptuneOrganization = new Organization(keystoneOrganization.FullName, true, defaultOrganizationType)
        //    {
        //        OrganizationGuid = keystoneOrganization.OrganizationGuid,
        //        OrganizationShortName = keystoneOrganization.ShortName,
        //        OrganizationUrl = keystoneOrganization.URL
        //    };
        //    _dbContext.Organizations.Add(neptuneOrganization);

        //    _dbContext.SaveChanges();

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
