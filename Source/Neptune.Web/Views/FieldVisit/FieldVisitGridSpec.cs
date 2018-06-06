/*-----------------------------------------------------------------------
<copyright file="FieldVisitGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using System;
using System.Collections.Generic;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitGridSpec : GridSpec<Models.FieldVisit>
    {
        public FieldVisitGridSpec(Person currentPerson)
        {
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    new FieldVisitDeleteFeature().HasPermission(currentPerson, x).HasPermission), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(y => y.Inventory(x)),new FieldVisitEditFeature().HasPermission(currentPerson, x).HasPermission), 30,
                DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add("Visit Date", x => x.VisitDate, 130, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Performed By", x => x.PerformedByPerson.GetFullNameFirstLastAsUrl(), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.FieldVisitStatus.ToGridHeaderString(), x => x.GetStatusAsWorkflowUrl(), 85,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Type", x => x.FieldVisitType.FieldVisitTypeDisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Inventory Updated?", x => new HtmlString(x.InventoryUpdated ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
            Add("Required Attributes Entered?", x => new HtmlString(!x.RequiredAttributeDoesNotHaveValue() ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
            Add("Initial Assessment?",
                x => x.InitialAssessmentID != null
                    ? UrlTemplate.MakeHrefString(x.InitialAssessment.GetDetailUrl(), x.InitialAssessment.IsAssessmentComplete() ? "Complete" : "In Progress",
                           new Dictionary<string, string> ())
                    : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Maintenance Occurred?",
                x => x.MaintenanceRecordID != null
                    ? UrlTemplate.MakeHrefString(x.MaintenanceRecord.GetDetailUrl(), "Performed",
                        new Dictionary<string, string>())
                    : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Post-Maintenance Assessment?",
                x => x.PostMaintenanceAssessmentID != null
                    ? UrlTemplate.MakeHrefString(x.PostMaintenanceAssessment.GetDetailUrl(), x.PostMaintenanceAssessment.IsAssessmentComplete() ? "Complete" : "In Progress",
                        new Dictionary<string, string>())
                    : new HtmlString("Not Performed"), 120, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
        }
    }
}