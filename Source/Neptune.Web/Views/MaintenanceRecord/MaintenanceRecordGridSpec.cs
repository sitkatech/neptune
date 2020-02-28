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

using System.Collections.Generic;
using System.Web;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class MaintenanceRecordGridSpec : GridSpec<Models.MaintenanceRecord>
    {

        public MaintenanceRecordGridSpec(Person currentPerson, IEnumerable<Models.CustomAttributeType> allMaintenanceAttributeTypes)
        {
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    currentPerson.IsManagerOrAdmin()), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => new HtmlString($"<a href={x.GetDetailUrl()} class='gridButton'>View</a>"), 40, DhtmlxGridColumnFilterType.None);

            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add("Date", x => x.GetMaintenanceRecordDate(), 150, DhtmlxGridColumnFormatType.Date);
            Add("Organization", x => x.GetMaintenanceRecordOrganization().GetDisplayNameAsUrl(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Performed By", x => x.GetMaintenanceRecordPerson() == null ? new HtmlString(string.Empty) : x.GetMaintenanceRecordPerson().GetFullNameFirstLastAsUrl(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.MaintenanceRecordType.ToGridHeaderString("Type"),
                x => x.MaintenanceRecordType?.MaintenanceRecordTypeDisplayName ?? "Not set", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Description", x => x.MaintenanceRecordDescription, 300, DhtmlxGridColumnFilterType.Text);
            foreach (var customAttributeType in allMaintenanceAttributeTypes)
            {
                Add(customAttributeType.DisplayNameWithUnits(),
                    x => x?.GetObservationValueForAttributeType(customAttributeType), 150, DhtmlxGridColumnFilterType.Text);
            }
        }
    }
}
