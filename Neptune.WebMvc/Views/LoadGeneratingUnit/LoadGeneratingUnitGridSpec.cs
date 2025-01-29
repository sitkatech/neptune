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

using Microsoft.AspNetCore.Html;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LoadGeneratingUnit
{
    public class LoadGeneratingUnitGridSpec : GridSpec<EFModels.Entities.vLoadGeneratingUnit>
    {
        public LoadGeneratingUnitGridSpec(LinkGenerator linkGenerator)
        {
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var wqmpDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var regionalSubbasinDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            
            Add("LGU ID", x => x.LoadGeneratingUnitID, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.Numeric);
            
            Add("Treatment BMP", x => x.TreatmentBMPID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID.Value),
                    x.TreatmentBMPName), 225, DhtmlxGridColumnFilterType.Text);
            Add("Water Quality Management Plan", x => x.WaterQualityManagementPlanID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    wqmpDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID.Value),
                    x.WaterQualityManagementPlanName), 225, DhtmlxGridColumnFilterType.Text);
            Add("Regional Subbasin", x => x.RegionalSubbasinID == null
                ? new HtmlString("N/A")
                : UrlTemplate.MakeHrefString(
                    regionalSubbasinDetailUrlTemplate.ParameterReplace(x.RegionalSubbasinID.Value),
                    x.RegionalSubbasinName), 225, DhtmlxGridColumnFilterType.Text);
            Add("Model Basin", x => x.ModelBasinKey, 100, DhtmlxGridColumnFormatType.Integer);
            Add("Date HRU Requested", x => x.DateHRURequested, 150, DhtmlxGridColumnFormatType.DateTime);
            Add("Is Empty?", x => x.IsEmptyResponseFromHRUService.ToYesNo(), 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
