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

using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Role
{
    public class IndexGridSpec : GridSpec<RoleSimpleDto>
    {
        public IndexGridSpec(LinkGenerator linkGenerator)
        {
            Add("Role", a => UrlTemplate.MakeHrefString(
                SitkaRoute<RoleController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(a.RoleID)), a.RoleDisplayName), 200, DhtmlxGridColumnFilterType.Html);
            Add("Count", a => a.PeopleWithRoleCount, 50);
        }
    }
}
