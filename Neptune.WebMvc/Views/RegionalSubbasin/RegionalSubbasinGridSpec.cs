/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPController.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class RegionalSubbasinGridSpec : GridSpec<EFModels.Entities.RegionalSubbasin>
    {
        public RegionalSubbasinGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate =
                new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.RegionalSubbasinID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("RSB ID", x => x.RegionalSubbasinID, 100);
            Add("Drain ID", x => x.DrainID, 150);
            Add("Watershed", x => x.Watershed, 150);
            Add("Catchment ID in OC Survey", x => x.OCSurveyCatchmentID, 150);
            Add("Downstream Catchment ID in OC Survey", x => x.OCSurveyDownstreamCatchmentID.HasValue ? x.OCSurveyDownstreamCatchmentID.Value.ToString() : "Terminus", 250, DhtmlxGridColumnFilterType.Numeric);


            Add("Last Updated", x => x.LastUpdate, 150, DhtmlxGridColumnFormatType.DateTime);
            
        }
    }
}