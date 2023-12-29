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

using Neptune.WebMvc.Models;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.User
{
    public class IndexGridSpec : GridSpec<Person>
    {
        public IndexGridSpec(LinkGenerator linkGenerator, Person currentPerson, Dictionary<int, int> countByPrimaryContactPerson)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var roleDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<RoleController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var organizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var hasDeletePermission = new UserDeleteFeature().HasPermissionByPerson(currentPerson);
            if (hasDeletePermission)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.PersonID), hasDeletePermission, true),
                    30, DhtmlxGridColumnFilterType.None);
            }
            Add("Last Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.PersonID), x.LastName), 100, DhtmlxGridColumnFilterType.Html);
            Add("First Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.PersonID), x.FirstName), 100, DhtmlxGridColumnFilterType.Html);
            Add("Email", x => x.Email, 200);
            Add($"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}", x =>
                UrlTemplate.MakeHrefString(organizationDetailUrlTemplate.ParameterReplace(x.OrganizationID), x.Organization.GetOrganizationShortNameIfAvailable()), 200);
            Add("Phone", x => x.Phone.ToPhoneNumberString(), 100);
            Add("Username", x => x.LoginName.ToString(), 200);
            Add("Last Activity", x => x.LastActivityDate, 120);
            Add("Role", x => UrlTemplate.MakeHrefString(roleDetailUrlTemplate.ParameterReplace(x.RoleID), x.Role.RoleDisplayName), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Active?", x => x.IsActive.ToYesNo(), 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Receives Support Emails?", x => x.ReceiveSupportEmails.ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"{FieldDefinitionType.PrimaryContact.GetFieldDefinitionLabel()} for Organizations", x =>
                countByPrimaryContactPerson.TryGetValue(x.PersonID, out var value) ? value : 0, 120);
            Add($"Assigned {FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()}", x => x.StormwaterJurisdictionPeople.Select(y => y.StormwaterJurisdiction).ToList().Count, 120);
        }
    }
}
