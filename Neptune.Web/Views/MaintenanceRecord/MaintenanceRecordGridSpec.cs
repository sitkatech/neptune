/*-----------------------------------------------------------------------
<copyright file="MaintenancyRecordGridSpec.cs" company="Tahoe Regional Planning Agency">
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
using Person = Neptune.EFModels.Entities.Person;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class MaintenanceRecordGridSpec : GridSpec<vMaintenanceRecordDetailed>
    {

        public MaintenanceRecordGridSpec(Person currentPerson, LinkGenerator linkGenerator)
        {
            var userDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.MaintenanceRecordID),currentPerson.IsManagerOrAdmin()), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.MaintenanceRecordID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            Add("Date", x => x.VisitDate, 150, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdictionName), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Performed By", x => UrlTemplate.MakeHrefString(userDetailUrlTemplate.ParameterReplace(x.PerformedByPersonID), x.PerformedByPersonName), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(FieldDefinitionType.MaintenanceRecordType.ToGridHeaderString("Type"),
                x => x.MaintenanceRecordTypeDisplayName ?? "Not set", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Description", x => x.MaintenanceRecordDescription, 300, DhtmlxGridColumnFilterType.Text);

            Add("Infiltration Surface Restored (sq-ft)", x => x.Infiltration_Surface_Restored, 150, DhtmlxGridColumnFilterType.Text);
            Add("Filtration Surface Restored (sq-ft)", x => x.Filtration_Surface_Restored, 150, DhtmlxGridColumnFilterType.Text);
            Add("Media Replaced (cu-yd)", x => x.Media_Replaced, 150, DhtmlxGridColumnFilterType.Text);
            Add("Mulch Added (cu-yd)", x => x.Mulch_Added, 150, DhtmlxGridColumnFilterType.Text);
            Add("% Trash", x => x.Percent_Trash, 150, DhtmlxGridColumnFilterType.Text);
            Add("% Green Waste", x => x.Percent_Green_Waste, 150, DhtmlxGridColumnFilterType.Text);
            Add("% Sediment", x => x.Percent_Sediment, 150, DhtmlxGridColumnFilterType.Text);
            Add("Area Reseeded (sq-ft)", x => x.Area_Reseeded, 150, DhtmlxGridColumnFilterType.Text);
            Add("Vegetation Planted (sq-ft)", x => x.Vegetation_Planted, 150, DhtmlxGridColumnFilterType.Text);
            Add("Surface and Bank Erosion Repaired (sq-ft)", x => x.Surface_and_Bank_Erosion_Repaired, 150, DhtmlxGridColumnFilterType.Text);
            Add("Total Material Volume Removed (cu-ft)", x => x.Total_Material_Volume_Removed__cu_ft_, 150, DhtmlxGridColumnFilterType.Text);
            Add("Total Material Volume Removed (gal)", x => x.Total_Material_Volume_Removed__gal_, 150, DhtmlxGridColumnFilterType.Text);
        }
    }
}
