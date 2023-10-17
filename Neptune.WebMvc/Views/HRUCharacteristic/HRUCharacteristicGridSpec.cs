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
using Microsoft.AspNetCore.Html;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.HRUCharacteristic
{
    public class HRUCharacteristicGridSpec : GridSpec<EFModels.Entities.vHRUCharacteristic>
    {
        public HRUCharacteristicGridSpec(LinkGenerator linkGenerator)
        {
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var wqmpDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var regionalSubbasinDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            
            Add("Type of HRU Entity", x => x.HRUEntity, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("LGU ID", x => x.LoadGeneratingUnitID, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            Add("Model Basin Land Use Description", x => x.HRUCharacteristicLandUseCodeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Baseline Model Basin Land Use Description", x => x.HRUCharacteristicLandUseCodeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Soil Group", x => x.HydrologicSoilGroup.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Slope Percentage", x => x.SlopePercentage, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            Add("Impervious Acres", x => x.ImperviousAcres, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Baseline Impervious Acres", x => x.BaselineImperviousAcres, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Total Acres", x => x.Area, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Treatment BMP", x => x.TreatmentBMPID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID.Value),
                    x.TreatmentBMPName), 250, DhtmlxGridColumnFilterType.Text);
            Add("Water Quality Management Plan", x => x.WaterQualityManagementPlanID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    wqmpDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID.Value),
                    x.WaterQualityManagementPlanName), 250, DhtmlxGridColumnFilterType.Text);
            Add("Regional Subbasin", x => x.RegionalSubbasinID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    regionalSubbasinDetailUrlTemplate.ParameterReplace(x.RegionalSubbasinID.Value),
                    x.RegionalSubbasinName), 250, DhtmlxGridColumnFilterType.Text);

            Add("Last Updated", x => x.LastUpdated, 150, DhtmlxGridColumnFormatType.DateTime);
        }
    }
}
