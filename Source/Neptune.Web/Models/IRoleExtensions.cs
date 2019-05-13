/*-----------------------------------------------------------------------
<copyright file="IRoleExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class IRoleExtensions
    {
        public static List<FeaturePermission> GetFeaturePermissions(this IRole role, Type baseFeatureType)
        {
            var featurePermissions = new List<FeaturePermission>();

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => baseFeatureType.IsAssignableFrom(p) && p.Name != baseFeatureType.Name && !p.IsAbstract);
            foreach (var type in types)
            {                                           
                string featureDisplayName = NeptuneBaseFeatureHelpers.GetDisplayName(type);
                var hasPermission = NeptuneBaseFeatureHelpers.DoesRoleHavePermissionsForFeature(role, type);

                //Don't add duplicates to the list
                if (featurePermissions.All(x => x.FeatureName != featureDisplayName))
                {
                    featurePermissions.Add(new FeaturePermission(featureDisplayName, hasPermission));
                }
            }
            return featurePermissions;
        }

        public static readonly UrlTemplate<int, int> SummaryUrlTemplate = new UrlTemplate<int, int>(SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        public static HtmlString GetDisplayNameAsUrl(this IRole role)
        {
            return UrlTemplate.MakeHrefString(SummaryUrlTemplate.ParameterReplace((int)role.NeptuneAreaEnum.Value, role.RoleID), role.RoleDisplayName);
        }
    }
}
