/*-----------------------------------------------------------------------
<copyright file="PersonWithRoleGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Role
{
    public class PersonWithRoleGridSpec : GridSpec<Person>
    {
        public PersonWithRoleGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            Add("Last Name", a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.PersonID), a.LastName), 200, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("First Name", a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.PersonID), a.FirstName), 200, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add($"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}", a => a.Organization.GetDisplayName(), 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Last Activity", a => a.LastActivityDate, 200);
        }
    }
}
