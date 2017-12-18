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
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.User;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.KeystoneDataService;
using Organization = Neptune.Web.Models.Organization;

namespace Neptune.Web.Controllers
{
    public class UserController : NeptuneBaseController
    {
        [UserEditFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [UserEditFeature]
        public GridJsonNetJObjectResult<Person> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var persons = HttpRequestStorage.DatabaseEntities.People.ToList().Where(x => new UserViewFeature().HasPermission(CurrentPerson, x).HasPermission).OrderBy(x => x.FullNameLastFirst).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Person>(persons, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [UserEditFeature]
        public PartialViewResult EditRoles(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditRolesViewModel(person);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [UserEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditRoles(PersonPrimaryKey personPrimaryKey, EditRolesViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(person, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditRolesViewModel viewModel)
        {
            var roles = CurrentPerson.IsSitkaAdministrator() ? Role.All : Role.All.Except(new[] {Role.SitkaAdmin});
            var rolesAsSelectListItems = roles.ToSelectListWithEmptyFirstRow(x => x.RoleID.ToString(CultureInfo.InvariantCulture), x => x.RoleDisplayName);
            var viewData = new EditRolesViewData(rolesAsSelectListItems);
            return RazorPartialView<EditRoles, EditRolesViewData, EditRolesViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [UserEditFeature]
        public PartialViewResult Delete(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(person.PersonID);
            return ViewDelete(person, viewModel);
        }

        private PartialViewResult ViewDelete(Person person, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !person.HasDependentObjects() && person != CurrentPerson;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete {person.FullNameFirstLastAndOrg}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Person", SitkaRoute<UserController>.BuildLinkFromExpression(x => x.Detail(person), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [UserEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(person, viewModel);
            }
            person.DeletePerson();
            return new ModalDialogFormJsonResult();
        }

        [UserViewFeature]
        public ViewResult Detail(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var userNotificationGridSpec = new UserNotificationGridSpec();
            var userNotificationGridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.UserNotificationsGridJsonData(personPrimaryKey));
            var activateInactivateUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.ActivateInactivatePerson(person));
            var viewData = new DetailViewData(CurrentPerson,
                person,
                userNotificationGridSpec,
                "userNotifications",
                userNotificationGridDataUrl,
                activateInactivateUrl);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [UserViewFeature]
        public GridJsonNetJObjectResult<Notification> UserNotificationsGridJsonData(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var gridSpec = new UserNotificationGridSpec();
            var notifications = person.Notifications.OrderByDescending(x => x.NotificationDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Notification>(notifications, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [UserEditFeature]
        public PartialViewResult ActivateInactivatePerson(PersonPrimaryKey personPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(personPrimaryKey.PrimaryKeyValue);
            return ViewActivateInactivatePerson(personPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewActivateInactivatePerson(Person person, ConfirmDialogFormViewModel viewModel)
        {
            string confirmMessage;
            if (person.IsActive)
            {
                var isPrimaryContactForAnyOrganization = person.OrganizationsWhereYouAreThePrimaryContactPerson.Any();
                if (isPrimaryContactForAnyOrganization)
                {
                    confirmMessage =
                        $@"You cannot inactive user '{person.FullNameFirstLast}' because {person.FirstName} is the {FieldDefinition.PrimaryContact.GetFieldDefinitionLabel()} for the following organizations: <ul> {string.Join("\r\n", person.PrimaryContactOrganizations.Select(x =>$"<li>{x.OrganizationName}</li>"))}</ul>";
                }
                else
                {
                    confirmMessage = $"Are you sure you want to inactivate user '{person.FullNameFirstLast}'?";
                }
                var viewData = new ConfirmDialogFormViewData(confirmMessage, !isPrimaryContactForAnyOrganization);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
            else
            {
                confirmMessage = $"Are you sure you want to activate user '{person.FullNameFirstLast}'?";
                var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
        }

        [HttpPost]
        [UserEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ActivateInactivatePerson(PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (person.IsActive)
            {
                Check.Require(!person.OrganizationsWhereYouAreThePrimaryContactPerson.Any(),
                    $@"You cannot inactive user '{person.FullNameFirstLast}' because {
                            person.FirstName
                        } is the {FieldDefinition.PrimaryContact.GetFieldDefinitionLabel()} for one or more {FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}!");
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
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [SitkaAdminFeature]
        public PartialViewResult PullUserFromKeystone()
        {
            var viewModel = new PullUserFromKeystoneViewModel();

            return ViewPullUserFromKeystone(viewModel);
        }

        [HttpPost]
        [SitkaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PullUserFromKeystone(PullUserFromKeystoneViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewPullUserFromKeystone(viewModel);
            }

            var keystoneClient = new KeystoneDataClient();

            UserProfile keystoneUser = keystoneClient.GetUserProfileByUsername(NeptuneWebConfiguration.KeystoneWebServiceApplicationGuid, viewModel.LoginName);
            if (keystoneUser == null)
            {
                SetErrorForDisplay($"Person not added. The {FieldDefinition.Username.GetFieldDefinitionLabel()} was not found in Keystone");
                return new ModalDialogFormJsonResult();    
            }
            
            if (!keystoneUser.OrganizationGuid.HasValue)
            {
                SetErrorForDisplay($"Person not added. They have no {FieldDefinition.Organization.GetFieldDefinitionLabel()} in Keystone");
            }

            KeystoneDataService.Organization keystoneOrganization = null;
            try
            {
                keystoneOrganization = keystoneClient.GetOrganization(keystoneUser.OrganizationGuid.Value);
            }
            catch (Exception)
            {
                SetErrorForDisplay($"Person not added. Could not find their {FieldDefinition.Organization.GetFieldDefinitionLabel()} in Keystone");
            }

            if (keystoneOrganization == null)
            {
                SetErrorForDisplay("Person not added. Could not find their Organization in Keystone");

            }
            else
            {
                var neptuneOrganization =
                    HttpRequestStorage.DatabaseEntities.Organizations.SingleOrDefault(
                        x => x.OrganizationGuid == keystoneUser.OrganizationGuid);
                if (neptuneOrganization == null)
                {
                    var defaultOrganizationType = HttpRequestStorage.DatabaseEntities.OrganizationTypes.GetDefaultOrganizationType();
                    neptuneOrganization = new Organization(keystoneOrganization.FullName, true, defaultOrganizationType)
                    {
                        OrganizationGuid = keystoneOrganization.OrganizationGuid,
                        OrganizationShortName = keystoneOrganization.ShortName,
                        OrganizationUrl = keystoneOrganization.URL
                    };
                    HttpRequestStorage.DatabaseEntities.AllOrganizations.Add(neptuneOrganization);
                }

                var neptunePerson =
                    HttpRequestStorage.DatabaseEntities.People.SingleOrDefault(
                        x => x.PersonGuid == keystoneUser.UserGuid);
                if (neptunePerson != null)
                {
                    neptunePerson.OrganizationID = neptuneOrganization.OrganizationID;
                }
                else
                {
                    neptunePerson = new Person(keystoneUser.UserGuid, keystoneUser.FirstName, keystoneUser.LastName,
                        keystoneUser.Email, Role.Unassigned, DateTime.Now, true, neptuneOrganization, false,
                        keystoneUser.LoginName);
                    HttpRequestStorage.DatabaseEntities.AllPeople.Add(neptunePerson);
                }

                HttpRequestStorage.DatabaseEntities.SaveChanges();

                SetMessageForDisplay($"{neptunePerson.GetFullNameFirstLastAndOrgAsUrl()} successfully added. You may want to <a href=\"{neptunePerson.GetDetailUrl()}\">assign them a role</a>.");
            }
            return new ModalDialogFormJsonResult();

            
        }

        private PartialViewResult ViewPullUserFromKeystone(PullUserFromKeystoneViewModel viewModel)
        {
            var viewData = new PullUserFromKeystoneViewData();
            return RazorPartialView<PullUserFromKeystone, PullUserFromKeystoneViewData, PullUserFromKeystoneViewModel>(viewData, viewModel);
        }
    }

}
