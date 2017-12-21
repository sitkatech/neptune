/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ModeledCatchment
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly MapInitJson MapInitJson;
        public readonly string FindCatchmentByNameUrl;
        public readonly string UpdateModeledCatchmentGeometryUrl;

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, Models.NeptunePage neptunePage, string updateModeledCatchmentGeometryUrl)
            : base(currentPerson, StormwaterBreadCrumbEntity.ModeledCatchment, neptunePage)
        {
            MapInitJson = mapInitJson;
            PageTitle = "Catchments";

            GridSpec = new IndexGridSpec(currentPerson) {ObjectNameSingular = "Catchment", ObjectNamePlural = "Catchments", SaveFiltersInCookie = true};
            GridName = "catchmentsGrid";
            GridDataUrl = SitkaRoute<ModeledCatchmentController>.BuildUrlFromExpression(j => j.IndexGridJsonData());

            FindCatchmentByNameUrl = SitkaRoute<ModeledCatchmentController>.BuildUrlFromExpression(x => x.FindByName(null));
            UpdateModeledCatchmentGeometryUrl = updateModeledCatchmentGeometryUrl;
        }
    }
}
