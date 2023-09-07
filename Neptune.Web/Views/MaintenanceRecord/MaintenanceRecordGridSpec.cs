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

using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class MaintenanceRecordGridSpec : GridSpec<EFModels.Entities.MaintenanceRecord>
    {

        public MaintenanceRecordGridSpec(Person currentPerson, IEnumerable<EFModels.Entities.CustomAttributeType> allMaintenanceAttributeTypes, LinkGenerator linkGenerator)
        {
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.MaintenanceRecordID),currentPerson.IsManagerOrAdmin()), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.MaintenanceRecordID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            Add("Date", x => x.GetMaintenanceRecordDate(), 150, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.StormwaterJurisdictionID), x.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Performed By", x => x.GetMaintenanceRecordPerson() == null ? new HtmlString(string.Empty) : x.GetMaintenanceRecordPerson().GetFullNameFirstLastAsUrl(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(FieldDefinitionType.MaintenanceRecordType.ToGridHeaderString("Type"),
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
