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

using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using System.Globalization;
using System.Web;

namespace Neptune.Web.Areas.Modeling.Views.HRUCharacteristic
{
    public class HRUCharacteristicGridSpec : GridSpec<Web.Models.HRUCharacteristic>
    {
        public HRUCharacteristicGridSpec()
        {
            Add("Type of HRU Entity",
                x => x.GetTreatmentBMP() != null ? "Treatment BMP" :
                    (x.GetWaterQualityManagementPlan() != null ? "Water Quality Management Plan" : "Regional Subbasin"), 150,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("LGU ID", x => x.LoadGeneratingUnitID, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            Add("Model Basin Land Use Description", x => x.HRUCharacteristicLandUseCode?.HRUCharacteristicLandUseCodeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Baseline LSPC Land Use Description", x => x.BaselineHRUCharacteristicLandUseCode?.HRUCharacteristicLandUseCodeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Soil Group", x => x.HydrologicSoilGroup.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Slope Percentage", x => x.SlopePercentage, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            Add("Impervious Acres", x => x.ImperviousAcres, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Baseline Impervious Acres", x => x.BaselineImperviousAcres, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Total Acres", x => x.Area, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Treatment BMP", x => x.GetTreatmentBMP()?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Water Quality Management Plan", x => x.GetWaterQualityManagementPlan()?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Regional Subbasin", x => x.GetRegionalSubbasin()?.GetDisplayNameAsUrl() ?? new HtmlString("N/A"), 250, DhtmlxGridColumnFilterType.Text);
            Add("Last Updated", x => x.LastUpdated, 150, DhtmlxGridColumnFormatType.DateTime);
        }
    }
}
