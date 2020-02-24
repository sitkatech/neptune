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
using System.Collections.Generic;
using System.Web;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitGridSpec : GridSpec<vFieldVisitDetailed>
    {
        public FieldVisitGridSpec(Person currentPerson, bool detailPage)
        {
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(FieldVisitModelExtensions.DeleteUrlTemplate.ParameterReplace(x.FieldVisitID),
                    currentPerson.IsManagerOrAdmin()), 30,
                DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x =>
                {
                    // do this first because if the field visit is verified, fieldvisiteditfeature will fail
                    if (x.IsFieldVisitVerified || x.FieldVisitStatusID == FieldVisitStatus.Complete.FieldVisitStatusID)
                    {
                        return UrlTemplate.MakeHrefString(FieldVisitModelExtensions.DetailUrlTemplate.ParameterReplace(x.FieldVisitID)
                            , "View", new Dictionary<string, string>{{"class", "gridButton"}});
                    }

                    return UrlTemplate.MakeHrefString(FieldVisitModelExtensions.WorkflowUrlTemplate.ParameterReplace(x.FieldVisitID)
                        , "Continue", new Dictionary<string, string> { { "class", "gridButton" } });
                }, 60,
                DhtmlxGridColumnFilterType.None);

            if (!detailPage)
            {
                Add("BMP Name", x => UrlTemplate.MakeHrefString(TreatmentBMPAssessmentModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            }

            Add("Visit Date", x => x.VisitDate, 130, DhtmlxGridColumnFormatType.Date);

            if (!detailPage)
            {
                Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(StormwaterJurisdictionModelExtensions.DetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            }

            Add("Performed By", x => UrlTemplate.MakeHrefString(PersonModelExtensions.DetailUrlTemplate.ParameterReplace(x.PerformedByPersonID), x.PerformedByPersonName), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Verified", x => x.IsFieldVisitVerified.ToYesNo(), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.FieldVisitStatus.ToGridHeaderString(), x => x.FieldVisitStatusID != FieldVisitStatus.InProgress.FieldVisitStatusID
                    ? new HtmlString(x.FieldVisitStatusDisplayName)
                    : UrlTemplate.MakeHrefString(FieldVisitModelExtensions.WorkflowUrlTemplate.ParameterReplace(x.FieldVisitID),
                        x.FieldVisitStatusDisplayName), 85,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Type", x => x.FieldVisitTypeDisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Inventory Updated?", x => new HtmlString(x.InventoryUpdated ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
            Add("Required Attributes Entered?", x => (x.NumberRequiredAttributesEntered >= x.NumberOfRequiredAttributes ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Initial Assessment?",
                x => x.TreatmentBMPAssessmentIDInitial.HasValue
                    ? UrlTemplate.MakeHrefString(TreatmentBMPAssessmentModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentIDInitial.Value),
                        x.IsAssessmentCompleteInitial ? "Complete" : "In Progress")
                    : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Initial Assessment Score", x => x.AssessmentScoreInitial?.ToString("0.0") ?? "-", 95,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Maintenance Occurred?",
                x => x.MaintenanceRecordID.HasValue
                    ? UrlTemplate.MakeHrefString(MaintenanceRecordModelExtensions.DetailUrlTemplate.ParameterReplace(x.MaintenanceRecordID.Value), "Performed",
                        new Dictionary<string, string>())
                    : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Post-Maintenance Assessment?",
                x =>
                    x.TreatmentBMPAssessmentIDPM.HasValue
                        ? UrlTemplate.MakeHrefString(TreatmentBMPAssessmentModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentIDPM.Value),
                            x.IsAssessmentCompletePM ? "Complete" : "In Progress")
                        : new HtmlString("Not Performed"), 120, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Post-Maintenance Assessment Score", x => x.AssessmentScorePM?.ToString("0.0") ?? "-", 95,
                DhtmlxGridColumnFilterType.Numeric);
        }
    }
}