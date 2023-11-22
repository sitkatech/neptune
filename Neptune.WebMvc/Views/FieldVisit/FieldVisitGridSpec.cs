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

using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class FieldVisitGridSpec : GridSpec<vFieldVisitDetailed>
    {
        public FieldVisitGridSpec(Person currentPerson, bool detailPage, LinkGenerator linkGenerator)
        {
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var personDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPAssessmentDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var maintenanceRecordDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var waterQualityManagementPlanDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Inventory(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));

            var isAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";
            
            if (!isAnonymousOrUnassigned)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(
                        deleteUrlTemplate.ParameterReplace(x.FieldVisitID),
                        currentPerson.IsManagerOrAdmin()), 30,
                    DhtmlxGridColumnFilterType.None);
                Add(string.Empty,
                    x =>
                    {
                        // do this first because if the field visit is verified, fieldvisiteditfeature will fail
                        if (x.IsFieldVisitVerified ||
                            x.FieldVisitStatusID == FieldVisitStatus.Complete.FieldVisitStatusID)
                        {
                            return UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.FieldVisitID)
                                , "View", new Dictionary<string, string> { { "class", "gridButton" } });
                        }

                        return UrlTemplate.MakeHrefString(
                            editUrlTemplate.ParameterReplace(x.FieldVisitID)
                            , "Continue", new Dictionary<string, string> { { "class", "gridButton" } });
                    }, 60,
                    DhtmlxGridColumnFilterType.None);
            }
            if (!detailPage)
            {
                Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            }

            Add("Visit Date", x => x.VisitDate, 130, DhtmlxGridColumnFormatType.Date);

            if (!detailPage)
            {
                Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
                Add(FieldDefinitionType.WaterQualityManagementPlan.ToGridHeaderString(),
                    x => x.WaterQualityManagementPlanID.HasValue ? UrlTemplate.MakeHrefString(
                        waterQualityManagementPlanDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID.Value),
                        x.WaterQualityManagementPlanName) : new HtmlString("No WQMP"), 105,
                    DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            }

            if (!isAnonymousOrUnassigned)
            {
                Add("Performed By",
                    x => UrlTemplate.MakeHrefString(
                        personDetailUrlTemplate.ParameterReplace(x.PerformedByPersonID),
                        x.PerformedByPersonName), 105,
                    DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            }
            else
            {
                Add("Performed By", x => x.PerformedByPersonName, 105, DhtmlxGridColumnFilterType.SelectFilterStrict);
            }

            Add("Field Visit Verified", x => x.IsFieldVisitVerified.ToYesNo(), 105,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(FieldDefinitionType.FieldVisitStatus.ToGridHeaderString(), x => isAnonymousOrUnassigned || x.FieldVisitStatusID != FieldVisitStatus.InProgress.FieldVisitStatusID
                    ? new HtmlString(x.FieldVisitStatusDisplayName)
                    : UrlTemplate.MakeHrefString(editUrlTemplate.ParameterReplace(x.FieldVisitID),
                        x.FieldVisitStatusDisplayName), 85,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Type", x => x.FieldVisitTypeDisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            if (!isAnonymousOrUnassigned)
            {
                Add("Inventory Updated?", x => new HtmlString(x.InventoryUpdated ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center);
                Add("Required Attributes Entered?", x => (x.NumberRequiredAttributesEntered >= x.NumberOfRequiredAttributes ? "Yes" : "No"), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);

                Add("Initial Assessment?",
                x => x.TreatmentBMPAssessmentIDInitial.HasValue
                    ? UrlTemplate.MakeHrefString(treatmentBMPAssessmentDetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentIDInitial.Value),
                        x.IsAssessmentCompleteInitial ? "Complete" : "In Progress")
                    : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);

                Add("Initial Assessment Score", x => x.AssessmentScoreInitial?.ToString("0.0") ?? "-", 95,
                    DhtmlxGridColumnFilterType.Numeric);
                Add("Maintenance Occurred?",
                    x => x.MaintenanceRecordID.HasValue
                        ? UrlTemplate.MakeHrefString(maintenanceRecordDetailUrlTemplate.ParameterReplace(x.MaintenanceRecordID.Value), "Performed",
                            new Dictionary<string, string>())
                        : new HtmlString("Not Performed"), 95, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
                Add("Post-Maintenance Assessment?",
                    x =>
                        x.TreatmentBMPAssessmentIDPM.HasValue
                            ? UrlTemplate.MakeHrefString(treatmentBMPAssessmentDetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentIDPM.Value),
                                x.IsAssessmentCompletePM ? "Complete" : "In Progress")
                            : new HtmlString("Not Performed"), 120, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
                Add("Post-Maintenance Assessment Score", x => x.AssessmentScorePM?.ToString("0.0") ?? "-", 95,
                    DhtmlxGridColumnFilterType.Numeric);
            }
        }
    }
}