/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.Web.Models;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Views.User
{
    public class IndexGridSpec : GridSpec<Person>
    {
        public IndexGridSpec(LinkGenerator linkGenerator, Person currentPerson)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var roleDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<RoleController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var organizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var hasDeletePermission = new UserDeleteFeature().HasPermissionByPerson(currentPerson);
            if (hasDeletePermission)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), hasDeletePermission, true),
                    30, DhtmlxGridColumnFilterType.None);
            }
            Add("Last Name", a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.PersonID), a.LastName), 100, DhtmlxGridColumnFilterType.Html);
            Add("First Name", a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.PersonID), a.FirstName), 100, DhtmlxGridColumnFilterType.Html);
            Add("Email", a => a.Email, 200);
            Add($"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}", a =>
                UrlTemplate.MakeHrefString(organizationDetailUrlTemplate.ParameterReplace(a.OrganizationID), a.Organization.GetOrganizationShortNameIfAvailable()), 200);
            Add("Phone", a => a.Phone.ToPhoneNumberString(), 100);
            Add("Username", a => a.LoginName.ToString(), 200);
            Add("Last Activity", a => a.LastActivityDate, 120);
            Add("Role", a => UrlTemplate.MakeHrefString(roleDetailUrlTemplate.ParameterReplace(a.RoleID), a.Role.RoleDisplayName), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Active?", a => a.IsActive.ToYesNo(), 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Receives Support Emails?", a => a.ReceiveSupportEmails.ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"{FieldDefinitionType.PrimaryContact.GetFieldDefinitionLabel()} for Organizations", a => a.GetPrimaryContactOrganizations().Count, 120);
            Add($"Assigned {FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()}", a => a.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction).ToList().Count, 120);
        }
    }
}
