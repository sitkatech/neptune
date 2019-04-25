/*-----------------------------------------------------------------------
<copyright file="ProvisionalFieldVisitGridSpec.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;

namespace Neptune.Web.Views.ManagerDashboard
{
    public class ProvisionalBMPDelineationsGridSpec : GridSpec<Models.Delineation>
    {
        public ProvisionalBMPDelineationsGridSpec(Person currentPerson, string gridName)
        {
            ObjectNameSingular = "Delineation";
            ObjectNamePlural = "Delineations";

            ArbitraryHeaderHtml = new List<string>
            {
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.checkAll()",
                    "glyphicon-check", "Select All"),
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.uncheckAll()",
                    "glyphicon-unchecked", "Unselect All")
            };
            AddCheckBoxColumn();
            Add("EntityID", x => x.DelineationID, 0);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    new DelineationDeleteFeature().HasPermission(currentPerson, x).HasPermission), 20,
                DhtmlxGridColumnFilterType.None);
            Add(string.Empty,x => x.GetDetailUrlForGrid(), 45, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add("BMP Type", x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Delineation Type", x => x.TreatmentBMP.Delineation.DelineationType.DelineationTypeDisplayName,80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Delineation Area", x => x.TreatmentBMP.Delineation?.GetDelineationAreaString(), 75,
                DhtmlxGridColumnFilterType.Text);
            Add("Date of Last Delineation Verification", x => x.TreatmentBMP.Delineation?.DateLastVerified, 120,
                DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(),
                x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}