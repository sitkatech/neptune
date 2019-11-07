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

using System.Globalization;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Modeling.Views.HRUCharacteristic{
    public class HRUCharacteristicGridSpec : GridSpec<Models.HRUCharacteristic>
    {
        public HRUCharacteristicGridSpec()
        {
            Add("LSPC Land Use Description", x => x.LSPCLandUseDescription.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Soil Group", x => x.HydrologicSoilGroup.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Slope Percentage", x => x.SlopePercentage, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            Add("Impervious Acres", x => x.ImperviousAcres, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Treatment BMP", x => x.TreatmentBMP?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Water Quality Management Plan", x => x.WaterQualityManagementPlan?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Network Catchment", x => x.NetworkCatchment?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Last Updated", x => x.LastUpdated, 150, DhtmlxGridColumnFormatType.Date);
        }
    }
}