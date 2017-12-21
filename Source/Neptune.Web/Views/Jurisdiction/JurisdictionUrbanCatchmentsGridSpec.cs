/*-----------------------------------------------------------------------
<copyright file="JurisdictionUrbanCatchmentsGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Jurisdiction
{
    public class JurisdictionModeledCatchmentsGridSpec : GridSpec<Models.ModeledCatchment>
    {
        public JurisdictionModeledCatchmentsGridSpec(Person currentPerson)
        {
            var userHasEditPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson); 

            Add(Models.FieldDefinition.ModeledCatchment.ToGridHeaderString(),x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ModeledCatchmentName), 200, DhtmlxGridColumnFilterType.Html);
            Add("Notes",x => x.Notes, 300, DhtmlxGridColumnFilterType.Text);
        }
    }
}
