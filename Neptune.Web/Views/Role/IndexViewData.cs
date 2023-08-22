/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.Web.Views.Role
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string? GridDataUrl;

        public IndexViewData(Person currentPerson, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            GridSpec = new IndexGridSpec(linkGenerator) { ObjectNameSingular = "Role", ObjectNamePlural = "Roles", SaveFiltersInCookie = true };
            GridName = "RoleGrid";
            GridDataUrl = _linkGenerator.GetPathByAction("IndexGridJsonData", "Role");
            PageTitle = "Roles Index";
        }
    }
}
