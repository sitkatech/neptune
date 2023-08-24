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

using Neptune.Web.Security;
using Neptune.Web.Controllers;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.ModalDialog;

namespace Neptune.Web.Views.User
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
        public HtmlString EditRolesLink { get; }
        public HtmlString EditJurisdictionsLink { get; }

        public DetailViewData(Person currentPerson,
            Person personToView,
            UserNotificationGridSpec userNotificationGridSpec,
            string userNotificationGridName,
            string userNotificationGridDataUrl,
            string activateInactivateUrl, LinkGenerator linkGenerator, HttpContext httpContext)
            : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            Person = personToView;
            PageTitle = personToView.GetFullNameFirstLast() + (!personToView.IsActive ? " (inactive)" : string.Empty);
            EntityName = "Users";
            //TODO: This gets pulled up to root
            EditPersonOrganizationPrimaryContactUrl = "";//todo:SitkaRoute<PersonOrganizationController>.BuildUrlFromExpression(c => c.EditPersonOrganizationPrimaryContacts(personToView));
            IndexUrl = SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            JurisdictionIndexUrl = "";//todo:SitkaRoute<JurisdictionController>.BuildUrlFromExpression(x => x.Index());

            UserHasPersonViewPermissions = false;//todo:new UserViewFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserCanManageThisPersonPermissions = false;//todo:new UserEditRoleFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserCanManagePeople = new UserEditFeature().HasPermissionByPerson(currentPerson);
            UserIsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            UserHasViewEverythingPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            if (UserCanManagePeople)
            {
                EntityUrl = IndexUrl;
            }

            IsViewingSelf = currentPerson != null && currentPerson.PersonID == personToView.PersonID;
            EditRolesLink = /*TODO: UserCanManageThisPersonPermissions
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(c => c.EditRoles(personToView)),
                    $"Edit Roles for User - {personToView.GetFullNameFirstLast()}",
                    true)
                : */new HtmlString(string.Empty);

            EditJurisdictionsLink = /*TODO: UserCanManageThisPersonPermissions
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(c => c.EditJurisdiction(personToView)),
                    $"Edit Assigned Jurisdictions for User - {personToView.GetFullNameFirstLast()}",
                    true)
                : */new HtmlString(string.Empty);

            UserNotificationGridSpec = userNotificationGridSpec;
            UserNotificationGridName = userNotificationGridName;
            UserNotificationGridDataUrl = userNotificationGridDataUrl;
            ActivateInactivateUrl = activateInactivateUrl;
        }


    }
}