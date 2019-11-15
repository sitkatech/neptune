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

using System.Collections.Generic;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using System.Globalization;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class NetworkCatchmentGridSpec : GridSpec<Models.NetworkCatchment>
    {
        public NetworkCatchmentGridSpec()
        {
            Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);

            Add("Drain ID", x => x.DrainID, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Watershed", x => x.Watershed, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Catchment ID in OC Survey", x => x.OCSurveyCatchmentID.ToString(), 250, DhtmlxGridColumnFilterType.None);
            Add("Downstream Catchment ID in OC Survey", x => x.OCSurveyDownstreamCatchmentID.HasValue ? x.OCSurveyDownstreamCatchmentID.Value.ToString() : "Terminus", 250, DhtmlxGridColumnFilterType.None);


            Add("Last Updated", x => x.LastUpdate, 150, DhtmlxGridColumnFormatType.DateTime);
            
        }
    }
}