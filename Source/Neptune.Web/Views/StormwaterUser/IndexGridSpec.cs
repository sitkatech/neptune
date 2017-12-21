/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.StormwaterUser
{
    public class IndexGridSpec : GridSpec<Person>
    {
        public IndexGridSpec(Person currentPerson, IOrderedEnumerable<StormwaterJurisdiction> stormwaterJurisdictions)
        {
            if (new NeptuneAdminFeature().HasPermissionByPerson(currentPerson))
            {
                Add(string.Empty,
                    x =>
                    {
                        return ModalDialogFormHelper.ModalDialogFormLink("Assign Roles",
                            SitkaRoute<StormwaterUserController>.BuildUrlFromExpression(p => p.Edit(x.PersonID)),
                            x.FullNameFirstLastAndOrg,
                            800,
                            "Save",
                            "Cancel",
                            new List<string> {"gridButton", "btn", "btn-trpa", "btn-xs"},
                            null,
                            null);
                    },
                    100,
                    DhtmlxGridColumnFilterType.None);
            }
            Add("Last Name", a =>
            {
                var hasViewUserPermissions = new UserViewFeature().HasPermission(currentPerson, a).HasPermission;
                return hasViewUserPermissions ? UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.LastName) : a.LastName.ToHTMLFormattedString();
            }, 100, DhtmlxGridColumnFilterType.Html);
            Add("First Name", a =>
            {
                var hasViewUserPermissions = new UserViewFeature().HasPermission(currentPerson, a).HasPermission;
                return hasViewUserPermissions ? UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.FirstName) : a.FirstName.ToHTMLFormattedString();
            }, 100, DhtmlxGridColumnFilterType.Html);
            
            Add("Email", a => a.Email, 200);
            Add("Organization", a => a.Organization.GetDisplayNameAsUrl(), 200);
            Add("Phone", a => a.Phone.ToPhoneNumberString(), 100);
            Add("Role", a => a.Role.GetDisplayNameAsUrl(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);            
            foreach (var stormwaterJurisdiction in stormwaterJurisdictions)
            {
                var jurisdiction = stormwaterJurisdiction;
                Add(stormwaterJurisdiction.OrganizationDisplayName, a => a.CanManageStormwaterJurisdiction(jurisdiction).ToYesOrEmpty(), 80, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            } 
        }   
    }
}
