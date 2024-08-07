﻿/*-----------------------------------------------------------------------
<copyright file="NeptuneEditFeature.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows Managing Jurisdiction Assets")]
    public class JurisdictionManageFeature : NeptuneFeature
    {
        public JurisdictionManageFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
        {
        }

        public override bool HasPermissionByPerson(Person person)
        {
            var hasRolePermission = base.HasPermissionByPerson(person);

            if (!hasRolePermission)
            {
                return false;
            }

            return new NeptuneAdminFeature().HasPermissionByPerson(person) || person.StormwaterJurisdictionPeople.Any();
        }
    }
}
