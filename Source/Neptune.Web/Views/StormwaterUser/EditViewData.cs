/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Models;

namespace Neptune.Web.Views.StormwaterUser
{
    public class EditViewData : NeptuneViewData
    {
        
        public readonly EditViewDataForAngular ViewDataForAngular;

        public EditViewData(Person currentPerson, List<Models.Role> stormwaterRoles, List<StormwaterJurisdiction> stormwaterJurisdictions)
            : base(currentPerson, StormwaterBreadCrumbEntity.Users)
        {
            ViewDataForAngular = new EditViewDataForAngular(stormwaterRoles, stormwaterJurisdictions);
        }

        public class EditViewDataForAngular
        {
            public readonly List<RoleSimple> RolesSimple;
            public readonly List<StormwaterJurisdictionSimple> StormwaterJurisdictionsSimple;

            public EditViewDataForAngular(List<Models.Role> roles, List<StormwaterJurisdiction> stormwaterJurisdictions)
            {
                RolesSimple = roles.Select(x => new RoleSimple(x)).ToList();
                StormwaterJurisdictionsSimple = stormwaterJurisdictions.OrderBy(x => x.Organization.DisplayName).Select(x => new StormwaterJurisdictionSimple(x)).ToList();
            }
        }
    }

}
