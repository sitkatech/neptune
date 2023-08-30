/*-----------------------------------------------------------------------
<copyright file="ProvisionalTreatmentBMPGridSpec.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Models;

namespace Neptune.Web.Views.ManagerDashboard
{
    public class ProvisionalTreatmentBMPGridSpec : GridSpec<TreatmentBMP>
    {
        public ProvisionalTreatmentBMPGridSpec(Person currentPerson, string gridName)
        {

            ArbitraryHeaderHtml = new List<string> { DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.checkAll()", "glyphicon-check", "Select All"), DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.uncheckAll()", "glyphicon-unchecked", "Unselect All") };
            AddCheckBoxColumn();
            Add("EntityID", x => x.TreatmentBMPID, 0);
            // todo
            // Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            //Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            // todo
            //Add("BMP Name", x => x.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMPType.TreatmentBMPTypeName, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date of Last BMP Record Verification", x => x.DateOfLastInventoryVerification, 125, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.DateOfLastInventoryChange.ToGridHeaderString(), x => x.InventoryLastChangedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Has Photos?", x => x.TreatmentBMPImages.Any().ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Benchmark and Thresholds Set?", x => x.IsBenchmarkAndThresholdsComplete().ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => x.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}
