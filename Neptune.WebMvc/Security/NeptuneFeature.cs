/*-----------------------------------------------------------------------
<copyright file="NeptuneFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Security
{
    public abstract class NeptuneFeature : BaseAuthorizationAttribute
    {
        protected NeptuneFeature(IEnumerable<RoleEnum> roles) : base(roles
            //, NeptuneArea.OCStormwaterTools
            ) { }

        public static bool IsAllowed<T>(SitkaRoute<T> sitkaRoute, Person currentPerson) where T : Controller
        {
            var neptuneFeatureLookupAttribute = sitkaRoute.Body.Method.GetCustomAttributes(typeof(NeptuneFeature), true).Cast<NeptuneFeature>().SingleOrDefault();
            if (neptuneFeatureLookupAttribute != null)
            {
                return neptuneFeatureLookupAttribute.HasPermissionByPerson(currentPerson);
            }
            // no feature attribute implies Anonymous access is ok
            return true;
        }
    }
}
