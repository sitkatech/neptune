﻿/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.WebMvc.Security;
using Neptune.WebMvc.Controllers;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.ModalDialog;

namespace Neptune.WebMvc.Views.User
{
    public class DetailViewData : NeptuneViewData
    {
        public Person Person { get; }
        public string EditPersonOrganizationPrimaryContactUrl { get; }
        public string IndexUrl { get; }
        public string JurisdictionIndexUrl { get; }

        public bool UserHasPersonViewPermissions { get; }
        public bool UserCanManageThisPersonPermissions { get; }
        public bool UserCanManagePeople { get; }
        public bool UserIsAdmin { get; }
        public bool UserHasViewEverythingPermissions { get; }
        public bool IsViewingSelf { get; }
        public UserNotificationGridSpec UserNotificationGridSpec { get; }
        public string UserNotificationGridName { get; }
        public string UserNotificationGridDataUrl { get; }
        public string ActivateInactivateUrl { get; }
        public IHtmlContent EditRolesLink { get; }
        public IHtmlContent EditJurisdictionsLink { get; }
        public UrlTemplate<int> OrganizationDetailUrlTemplate { get; }
        public UrlTemplate<int> JurisdictionDetailUrlTemplate { get; }
        public string RoleDetailUrl { get; }
        public List<EFModels.Entities.Organization> PrimaryContactOrganizations { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            Person personToView,
            UserNotificationGridSpec userNotificationGridSpec,
            string userNotificationGridName,
            string userNotificationGridDataUrl,
            string activateInactivateUrl, List<EFModels.Entities.Organization> primaryContactOrganizations)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            Person = personToView;
            PageTitle = personToView.GetFullNameFirstLast() + (!personToView.IsActive ? " (inactive)" : string.Empty);
            EntityName = "Users";

            EditPersonOrganizationPrimaryContactUrl = SitkaRoute<PersonOrganizationController>.BuildUrlFromExpression(LinkGenerator, x => x.EditPersonOrganizationPrimaryContacts(personToView));
            IndexUrl = SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            RoleDetailUrl = SitkaRoute<RoleController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(personToView.RoleID));
            JurisdictionIndexUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());

            UserHasPersonViewPermissions = new UserViewFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserCanManageThisPersonPermissions = new UserEditRoleFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserCanManagePeople = new UserEditFeature().HasPermissionByPerson(currentPerson);
            UserIsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            UserHasViewEverythingPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            if (UserCanManagePeople)
            {
                EntityUrl = IndexUrl;
            }

            IsViewingSelf = currentPerson != null && currentPerson.PersonID == personToView.PersonID;
            EditRolesLink = UserCanManageThisPersonPermissions
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.EditRoles(personToView)),
                    $"Edit Roles for User - {personToView.GetFullNameFirstLast()}",
                    true)
                : new HtmlString(string.Empty);

            EditJurisdictionsLink = UserCanManageThisPersonPermissions
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.EditJurisdiction(personToView)),
                    $"Edit Assigned Jurisdictions for User - {personToView.GetFullNameFirstLast()}",
                    true)
                : new HtmlString(string.Empty);

            OrganizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            JurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));


            UserNotificationGridSpec = userNotificationGridSpec;
            UserNotificationGridName = userNotificationGridName;
            UserNotificationGridDataUrl = userNotificationGridDataUrl;
            ActivateInactivateUrl = activateInactivateUrl;
            PrimaryContactOrganizations = primaryContactOrganizations;
        }


    }
}
