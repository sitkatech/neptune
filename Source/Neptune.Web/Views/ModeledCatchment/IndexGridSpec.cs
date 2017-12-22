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

namespace Neptune.Web.Views.ModeledCatchment
{
    public class IndexGridSpec : GridSpec<Models.ModeledCatchment>
    {
        public IndexGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.ModeledCatchment.ToGridHeaderString("Catchment Name"), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ModeledCatchmentName), 200, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.StormwaterJurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(x.GetJurisdictionSummaryUrl(), x.StormwaterJurisdiction.OrganizationDisplayName), 300, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Notes", x => x.Notes, 400, DhtmlxGridColumnFilterType.Text);
        }
    }
}
