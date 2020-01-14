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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Web;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitGridSpec : GridSpec<Models.FieldVisit>
    {
        public FieldVisitGridSpec(Person currentPerson, bool detailPage)
        {
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    new FieldVisitDeleteFeature().HasPermission(currentPerson, x).HasPermission), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x =>
                {
                    // do this first because if the field visit is verified, fieldvisiteditfeature will fail
                    if (x.IsFieldVisitVerified || x.FieldVisitStatus == FieldVisitStatus.Complete)
                    {
                        return new HtmlString($"<a href={x.GetDetailUrl()} class='gridButton'>View</a>");
                    }
                    
                    if (!new FieldVisitEditFeature().HasPermission(currentPerson, x).HasPermission)
                    {
                        // only reason we would get here is that the user can't manage field visits for this jurisdiction
                        return new HtmlString("");
                    }

                    return new HtmlString($"<a href={x.GetEditUrl()} class='gridButton'>Continue</a>");
                }, 60,
                DhtmlxGridColumnFilterType.None);

            if (!detailPage)
            {
                Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            }

            Add("Visit Date", x => x.VisitDate, 130, DhtmlxGridColumnFormatType.Date);

            if (!detailPage)
            {
                Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(),
                    x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140,
                    DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            }

            Add("Performed By", x => x.PerformedByPerson.GetFullNameFirstLastAsUrl(), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Verified", x => x.IsFieldVisitVerified.ToYesNo(), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.FieldVisitStatus.ToGridHeaderString(), x => x.GetStatusAsWorkflowUrl(), 85,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Type", x => x.FieldVisitType.FieldVisitTypeDisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Inventory Updated?", x => new HtmlString(x.InventoryUpdated ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
            Add("Required Attributes Entered?", x => new HtmlString((!x.TreatmentBMP.RequiredAttributeDoesNotHaveValue()).ToYesNo()), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
            Add("Initial Assessment?",
                x =>
                {
                    var initialAssessment = x.GetInitialAssessment();
                    return initialAssessment != null
                        ? UrlTemplate.MakeHrefString(initialAssessment.GetDetailUrl(),
                            initialAssessment.CalculateIsAssessmentComplete() ? "Complete" : "In Progress",
                            new Dictionary<string, string>())
                        : new HtmlString("Not Performed");
                }, 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Initial Assessment Score", x => x.GetInitialAssessment()?.FormattedScore() ?? "N/A", 95,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Maintenance Occurred?",
                x =>
                {
                    var maintenanceRecord = x.GetMaintenanceRecord();
                    return maintenanceRecord != null
                        ? UrlTemplate.MakeHrefString(maintenanceRecord.GetDetailUrl(), "Performed",
                            new Dictionary<string, string>())
                        : new HtmlString("Not Performed");
                }, 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Post-Maintenance Assessment?",
                x =>
                {
                    var postMaintenanceAssessment = x.GetPostMaintenanceAssessment();
                    return postMaintenanceAssessment != null
                        ? UrlTemplate.MakeHrefString(postMaintenanceAssessment.GetDetailUrl(),
                            postMaintenanceAssessment.CalculateIsAssessmentComplete() ? "Complete" : "In Progress",
                            new Dictionary<string, string>())
                        : new HtmlString("Not Performed");
                }, 120, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Post-Maintenance Assessment Score", x => x.GetPostMaintenanceAssessment()?.FormattedScore() ?? "N/A", 95,
                DhtmlxGridColumnFilterType.Numeric);
        }
    }
}