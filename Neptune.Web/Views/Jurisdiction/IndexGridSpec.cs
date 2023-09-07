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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Jurisdiction
{
    public class IndexGridSpec : GridSpec<StormwaterJurisdiction>
    {
        public IndexGridSpec(LinkGenerator linkGenerator)
        {
            var stormwaterJurisdictionUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.Organization.GetDisplayName()), 400, DhtmlxGridColumnFilterType.Html);
            Add("Number of Users", x => x.StormwaterJurisdictionPeople.Count, 80);
            Add("Number of BMPs", x => x.TreatmentBMPs.Count, 80, DhtmlxGridColumnAggregationType.Total);
            Add("Public BMP Visibility",
                x => x.StormwaterJurisdictionPublicBMPVisibilityType
                    .StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Public WQMP Visibility",
                x => x.StormwaterJurisdictionPublicWQMPVisibilityType
                    .StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}
