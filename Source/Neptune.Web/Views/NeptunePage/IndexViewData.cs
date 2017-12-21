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
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using LtInfo.Common;
using Neptune.Web.Common;

namespace Neptune.Web.Views.NeptunePage
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly NeptunePageGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string NeptunePageUrl;

        public IndexViewData(Person currentPerson) : base(currentPerson, null, null)
        {
            PageTitle = "Manage Page Content";

            GridSpec = new NeptunePageGridSpec(new NeptunePageViewListFeature().HasPermissionByPerson(currentPerson))
            {
                ObjectNameSingular = "Page",
                ObjectNamePlural = "Pages",
                SaveFiltersInCookie = true
            };
            GridName = "neptunePagesGrid";
            GridDataUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
            NeptunePageUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(x => x.NeptunePageDetails(UrlTemplate.Parameter1Int));
        }
    }
}
