﻿/*-----------------------------------------------------------------------
<copyright file="Role.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neptune.Web.Security;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public partial class Role : IRole
    {
        public List<FeaturePermission> GetFeaturePermissions()
        {
            var featurePermissions = this.GetFeaturePermissions(typeof(NeptuneFeature));
            featurePermissions.AddRange(this.GetFeaturePermissions(typeof(NeptuneFeatureWithContext)));
            return featurePermissions;
        }

        public List<Person> GetPeopleWithRole()
        {
            return HttpRequestStorage.DatabaseEntities.People.Where(x => x.IsActive && x.RoleID == RoleID).ToList();
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Detail(RoleID)), RoleDisplayName);
        }

        public static string GetAnonymousRoleUrl()
        {
            return SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Anonymous());
        }
    }
}
