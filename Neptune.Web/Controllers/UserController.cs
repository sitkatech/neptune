/*-----------------------------------------------------------------------
<copyright file="UserController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Security;
using Neptune.Web.Views.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.DesignByContract;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.UserJurisdictions;

namespace Neptune.Web.Controllers
{
    public class UserController : NeptuneBaseController<UserController>
    {
        public UserController(NeptuneDbContext dbContext, ILogger<UserController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [UserEditFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson);
            return RazorView<Views.User.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [UserEditFeature]
        public GridJsonNetJObjectResult<Person> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var persons = _dbContext.People.AsNoTracking().Include(x => x.Organization).Include(x => x.Organizations)
                .Include(x => x.StormwaterJurisdictionPeople).ThenInclude(x => x.StormwaterJurisdiction)
                .ToList().Where(x => new UserViewFeature().HasPermission(CurrentPerson, x).HasPermission)
                .OrderBy(x => x.GetFullNameLastFirst()).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Person>(persons, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{personPrimaryKey}")]
        [UserEditRoleFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public PartialViewResult EditRoles([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditRolesViewModel(person);
            return ViewEditRoles(viewModel, person);
        }

        [HttpPost("{personPrimaryKey}")]
        [UserEditRoleFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public async Task<IActionResult> EditRoles([FromRoute] PersonPrimaryKey personPrimaryKey, EditRolesViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditRoles(viewModel, person);
            }
            viewModel.UpdateModel(person, CurrentPerson, _dbContext);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Role successfully changed for {person.GetFullNameFirstLastAndOrgAsUrl()}.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditRoles(EditRolesViewModel viewModel, Person person)
        {
            var roles = CurrentPerson.IsSitkaAdministrator()
                ? Role.All
                : (CurrentPerson.IsAdministrator()
                    ? Role.All.Except(new List<Role> { Role.SitkaAdmin })
                    : Role.All.Except(new List<Role> { Role.SitkaAdmin, Role.Admin }));

            // if the user being updated is a Jurisdiction Manager, only an admin can downgrade them
            if (!CurrentPerson.IsAdministrator() && person.Role == Role.JurisdictionManager)
            {
                roles = new List<Role> { Role.JurisdictionManager };
            }

            // if the user being updated is a SitkaAdmin, only a SitkaAdmin can downgrade them
            if (person.Role == Role.SitkaAdmin && CurrentPerson.Role != Role.SitkaAdmin)
            {
                roles = new List<Role> { Role.SitkaAdmin };
            }

            var rolesAsSelectListItems = roles.ToSelectListWithEmptyFirstRow(x => x.RoleID.ToString(CultureInfo.InvariantCulture), x => x.RoleDisplayName);
            var viewData = new EditRolesViewData(rolesAsSelectListItems, CurrentPerson.IsAdministrator());
            return RazorPartialView<EditRoles, EditRolesViewData, EditRolesViewModel>(viewData, viewModel);
        }

        [HttpGet("{personPrimaryKey}")]
        [UserDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public PartialViewResult Delete([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(person.PersonID);
            return ViewDelete(person, viewModel);
        }

        private PartialViewResult ViewDelete(Person person, ConfirmDialogFormViewModel viewModel)
        {
            //todo: var canDelete = !person.HasDependentObjects() && person != CurrentPerson;
            var canDelete = false;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete {person.GetFullNameFirstLastAndOrg()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Person",
                    UrlTemplate.MakeHrefString(
                        SitkaRoute<UserController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(person.PersonID)),
                            "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{personPrimaryKey}")]
        [UserDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(person, viewModel);
            }
//            _dbContext.People.DeletePerson(person);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{personPrimaryKey}")]
        [UserViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public ViewResult Detail([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var userNotificationGridSpec = new UserNotificationGridSpec();
            var userNotificationGridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(_linkGenerator, x => x.UserNotificationsGridJsonData(personPrimaryKey));
            var activateInactivateUrl = SitkaRoute<UserController>.BuildUrlFromExpression(_linkGenerator, x => x.ActivateInactivatePerson(person));
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, person, userNotificationGridSpec, "userNotifications", userNotificationGridDataUrl, activateInactivateUrl);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{personPrimaryKey}")]
        [UserViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public GridJsonNetJObjectResult<Notification> UserNotificationsGridJsonData([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var gridSpec = new UserNotificationGridSpec();
            var notifications = person.Notifications.OrderByDescending(x => x.NotificationDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Notification>(notifications, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{personPrimaryKey}")]
        [UserEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public PartialViewResult ActivateInactivatePerson([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(personPrimaryKey.PrimaryKeyValue);
            return ViewActivateInactivatePerson(personPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewActivateInactivatePerson(Person person, ConfirmDialogFormViewModel viewModel)
        {
            string confirmMessage;
            if (person.IsActive)
            {
                var isPrimaryContactForAnyOrganization = person.GetPrimaryContactOrganizations().Any();
                confirmMessage =
                    isPrimaryContactForAnyOrganization
                        ? $@"You cannot inactive user '{person.GetFullNameFirstLast()}' because {person.FirstName} is the {FieldDefinitionType.PrimaryContact.GetFieldDefinitionLabel()} for the following organizations: <ul> {string.Join("\r\n", person.GetPrimaryContactOrganizations().Select(x => $"<li>{x.OrganizationName}</li>"))}</ul>"
                        : $"Are you sure you want to inactivate user '{person.GetFullNameFirstLast()}'?";
                var viewData = new ConfirmDialogFormViewData(confirmMessage, !isPrimaryContactForAnyOrganization);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
            else
            {
                confirmMessage = $"Are you sure you want to activate user '{person.GetFullNameFirstLast()}'?";
                var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
        }

        [HttpPost("{personPrimaryKey}")]
        [UserEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public async Task<IActionResult> ActivateInactivatePerson([FromRoute] PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (person.IsActive)
            {
                Check.Require(!person.GetPrimaryContactOrganizations().Any(),
                    $@"You cannot inactive user '{person.GetFullNameFirstLast()}' because {person.FirstName} is the {FieldDefinitionType.PrimaryContact.GetFieldDefinitionLabel()} for one or more {FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized()}!");
            }
            if (!ModelState.IsValid)
            {
                return ViewActivateInactivatePerson(person, viewModel);
            }
            if (person.IsActive)
            {
                // if the person is currently active, we need to remove them from the support email list no matter what since we are about to inactivate the person
                person.ReceiveSupportEmails = false;
            }
            person.IsActive = !person.IsActive;
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{personPrimaryKey}")]
        [UserEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public PartialViewResult EditJurisdiction([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditUserJurisdictionsViewModel(person, CurrentPerson);
            return ViewEditJurisdiction(viewModel);
        }

        [HttpPost("{personPrimaryKey}")]
        [UserEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public async Task<IActionResult> EditJurisdiction([FromRoute] PersonPrimaryKey personPrimaryKey, EditUserJurisdictionsViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditJurisdiction(viewModel);
            }

            viewModel.UpdateModel(person, _dbContext.StormwaterJurisdictionPeople);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Assigned {FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()} successfully changed for {person.GetFullNameFirstLastAndOrgAsUrl()}.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditJurisdiction(EditUserJurisdictionsViewModel viewModel)
        {
            var allStormwaterJurisdictions = _dbContext.StormwaterJurisdictions.ToList();
            var stormwaterJurisdictionsCurrentPersonCanManage = StormwaterJurisdictions.ListViewableByPerson(_dbContext, CurrentPerson);
            var viewData = new EditUserJurisdictionsViewData(HttpContext, _linkGenerator, CurrentPerson, allStormwaterJurisdictions, stormwaterJurisdictionsCurrentPersonCanManage, true);
            return RazorPartialView<EditUserJurisdictions, EditUserJurisdictionsViewData, EditUserJurisdictionsViewModel>(viewData, viewModel);
        }

        //[JurisdictionManageFeature]
        //[HttpGet]
        //public ActionResult Invite()
        //{
        //    var viewModel = new InviteViewModel();
        //    return ViewInvite(viewModel);
        //}

        //private ActionResult ViewInvite(InviteViewModel viewModel)
        //{
        //    var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.InviteUser);
        //    var organizations = _dbContext.Organizations.OrderBy(x => x.OrganizationName).ToList();
        //    var cancelUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, x => x.Index());
        //    var viewData = new InviteViewData(HttpContext, _linkGenerator, CurrentPerson, organizations, neptunePage, cancelUrl);
        //    return RazorView<Invite, InviteViewData, InviteViewModel>(viewData, viewModel);
        //}

        //[JurisdictionManageFeature]
        //[HttpPost]
        //public ActionResult Invite(InviteViewModel viewModel)
        //{
        //    var toolDisplayName = "Orange County Stormwater Tools";
        //    var homeUrl = SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Index(), NeptuneWebConfiguration.CanonicalHostNameRoot);
        //    var loginUrl =
        //        SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(x => x.LogOn(),
        //            NeptuneWebConfiguration.CanonicalHostName);
        //    var supportUrl = SitkaRoute<HelpController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Support(), NeptuneWebConfiguration.CanonicalHostNameRoot);
        //    var inviteModel = new KeystoneService.KeystoneInviteModel
        //    {
        //        FirstName = viewModel.FirstName,
        //        LastName = viewModel.LastName,
        //        Email = viewModel.Email,
        //        SiteName = toolDisplayName,
        //        Subject = $"Invitation to the {toolDisplayName}",
        //        WelcomeText = $"You have been invited by a colleague to create an account in the <a href=\"{homeUrl}\">{toolDisplayName}</a>. The {toolDisplayName} application is a collaborative effort of Orange County Public Works, MS4 Permittees, and other organizations.",
        //        RedirectURL = loginUrl,
        //        SupportBlock = $"If you have any questions, please visit our <a href=\"{supportUrl}\">support page</a>",
        //        OrganizationGuid = viewModel.OrganizationGuid,
        //        SignatureBlock = $"The {toolDisplayName} team"
        //    };

        //    var keystoneService = new KeystoneService(HttpRequestStorage.GetHttpContextUserThroughOwin());
        //    var response = keystoneService.Invite(inviteModel);
        //    if (response.StatusCode != HttpStatusCode.OK || response.Error != null)
        //    {
        //        ModelState.AddModelError("Email", $"There was a problem inviting the user to Keystone: {response.Error.Message}.");
        //        if (response.Error.ModelState != null)
        //        {
        //            foreach (var modelStateKey in response.Error.ModelState.Keys)
        //            {
        //                foreach (var err in response.Error.ModelState[modelStateKey])
        //                {
        //                    ModelState.AddModelError(modelStateKey, err);
        //                }
        //            }
        //        }
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return ViewInvite(viewModel);
        //    }

        //    var keystoneUser = response.Payload.Claims;
        //    var existingUser = _dbContext.People.GetPersonByPersonGuid(keystoneUser.UserGuid);
        //    if (existingUser != null)
        //    {
        //        SetMessageForDisplay($"{existingUser.GetFullNameFirstLastAndOrgAsUrl()} already has an account.</a>.");
        //        return RedirectToAction(new SitkaRoute<UserController>(x => x.Detail(existingUser)));
        //    }

        //    var setJurisdictions = !CurrentPerson.IsAdministrator();
        //    var newUser = CreateNewFirmaPerson(keystoneUser, keystoneUser.OrganizationGuid);

        //    _dbContext.SaveChanges();

        //    if (setJurisdictions)
        //    {
        //        foreach (var stormwaterJurisdictionPerson in CurrentPerson.StormwaterJurisdictionPeople)
        //        {
        //            newUser.StormwaterJurisdictionPeople.Add(new StormwaterJurisdictionPerson(stormwaterJurisdictionPerson.StormwaterJurisdictionID, newUser.PersonID));
        //        }
        //    }

        //    newUser.RoleID = Role.JurisdictionEditor.RoleID;

        //    _dbContext.SaveChanges();

        //    SetMessageForDisplay(
        //        $"{newUser.GetFullNameFirstLastAndOrgAsUrl()} successfully added. You may want to assign them a role</a>.");
        //    return RedirectToAction(new SitkaRoute<UserController>(x => x.Detail(newUser)));
        //}

        //private static Person CreateNewFirmaPerson(KeystoneService.KeystoneUserClaims keystoneUser,
        //    Guid? organizationGuid)
        //{
        //    Organization organization;
        //    if (organizationGuid.HasValue)
        //    {
        //        organization =
        //            _dbContext.Organizations.GetOrganizationByOrganizationGuid(organizationGuid
        //                .Value);

        //        if (organization == null)
        //        {
        //            var keystoneClient = new KeystoneDataClient();


        //            var keystoneOrganization = keystoneClient.GetOrganization(organizationGuid.Value);


        //            var defaultOrganizationType =
        //                _dbContext.OrganizationTypes.GetDefaultOrganizationType();
        //            var neptuneOrganization =
        //                new Organization(keystoneOrganization.FullName, true, defaultOrganizationType)
        //                {
        //                    OrganizationGuid = keystoneOrganization.OrganizationGuid,
        //                    OrganizationShortName = keystoneOrganization.ShortName,
        //                    OrganizationUrl = keystoneOrganization.URL
        //                };
        //            _dbContext.Organizations.Add(neptuneOrganization);

        //            _dbContext.SaveChanges();

        //            organization = neptuneOrganization;
        //        }
        //    }
        //    else
        //    {
        //        organization = _dbContext.Organizations.GetUnknownOrganization();
        //    }


        //    var firmaPerson = new Person(keystoneUser.UserGuid, keystoneUser.FirstName, keystoneUser.LastName,
        //        keystoneUser.Email, Role.Unassigned, DateTime.Now, true, organization, false,
        //        keystoneUser.LoginName, false, Guid.NewGuid(), false);
        //    _dbContext.People.Add(firmaPerson);
        //    return firmaPerson;
        //}

    }
}
