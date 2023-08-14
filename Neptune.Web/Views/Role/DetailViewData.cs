/*-----------------------------------------------------------------------
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

using Neptune.EFModels;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Role
{
    public class DetailViewData : NeptuneViewData
    {
        public PersonWithRoleGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public string RoleName { get; }
        public string RoleDescription { get; }

        // ReSharper disable once InconsistentNaming
        public readonly NeptuneAreaEnum? NeptuneAreaEnum;
        // ReSharper disable once InconsistentNaming
        public readonly string NeptuneAreaName;


        public DetailViewData(Person currentPerson, IRole role, LinkGenerator linkGenerator)
            : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator)
        {
            RoleName = role.RoleDisplayName;
            RoleDescription = role.RoleDescription;

            //Grid
            if (role.NeptuneAreaEnum.HasValue)
            {
                GridSpec = new PersonWithRoleGridSpec() { ObjectNameSingular = "Person", ObjectNamePlural = "People", SaveFiltersInCookie = true };
                GridName = "PersonWithRoleGrid";
                GridDataUrl = "";//todo:SitkaRoute<RoleController>().BuildUrlFromExpression(tc => tc.PersonWithRoleGridJsonData(role.NeptuneAreaEnum.Value, role.RoleID));
            }

            PageTitle = $"Role Summary for {RoleName}";
            EntityName = "Role Summary";
        }
    }
}
