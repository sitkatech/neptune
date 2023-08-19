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
using Neptune.Web.Security;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Organization
{
    public class IndexGridSpec : GridSpec<EFModels.Entities.Organization>
    {
        public IndexGridSpec(Person currentPerson, bool hasDeletePermissions, LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var userViewFeature = new UserViewFeature();
            if (hasDeletePermissions)
            {
                var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.DeleteOrganization(UrlTemplate.Parameter1Int)));
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.OrganizationID), true, true/*!x.HasDependentObjects()*/), 30, DhtmlxGridColumnFilterType.None);
            }
            Add(FieldDefinitionType.Organization.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.OrganizationID), a.OrganizationName), 400, DhtmlxGridColumnFilterType.Html);
            Add("Short Name", a => a.OrganizationShortName, 100);
            Add(FieldDefinitionType.OrganizationType.ToGridHeaderString(), a => a.OrganizationType.OrganizationTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.PrimaryContact.ToGridHeaderString(), a => /*userViewFeature.HasPermission(currentPerson, a.PrimaryContactPerson).HasPermission ? a.GetPrimaryContactPersonAsUrl() : */new HtmlString(a.GetPrimaryContactPersonAsString()), 120);
            Add("# of Users", a => a.People.Count, 90);
            Add("Is Active", a => a.IsActive.ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Is in Keystone", a => a.IsInKeystone().ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
