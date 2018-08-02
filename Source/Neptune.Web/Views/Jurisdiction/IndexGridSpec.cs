/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Views.Jurisdiction
{
    public class IndexGridSpec : GridSpec<StormwaterJurisdiction>
    {
        public IndexGridSpec()
        {
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.Organization.GetDisplayName()), 400, DhtmlxGridColumnFilterType.Html);
            Add("Number of Users", x => x.StormwaterJurisdictionPeople.Count, 80);
            Add("Number of BMPs", x => x.TreatmentBMPs.Count, 80, DhtmlxGridColumnAggregationType.Total);
            Add("Number of Catchments", x => x.ModeledCatchments.Count, 80, DhtmlxGridColumnAggregationType.Total);
        }
    }
}
