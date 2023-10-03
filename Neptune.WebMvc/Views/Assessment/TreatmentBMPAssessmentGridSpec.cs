/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Assessment
{
    public class TreatmentBMPAssessmentGridSpec : GridSpec<TreatmentBMPAssessmentDetailedWithObservations>
    {
        public TreatmentBMPAssessmentGridSpec(Person currentPerson,
            IEnumerable<EFModels.Entities.TreatmentBMPAssessmentObservationType> allObservationTypes, LinkGenerator linkGenerator)
        {
            var userDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.TreatmentBMPAssessment.TreatmentBMPAssessmentID), currentPerson.IsManagerOrAdmin()), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessment.TreatmentBMPAssessmentID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessment.TreatmentBMPID), x.TreatmentBMPAssessment.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMPAssessment.TreatmentBMPTypeName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date", x => x.TreatmentBMPAssessment.VisitDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Water Year", x => x.TreatmentBMPAssessment.VisitDate.GetFiscalYear().ToString("0000"), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x =>
                UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessment.StormwaterJurisdictionID), x.TreatmentBMPAssessment.StormwaterJurisdictionName), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Performed By", x => UrlTemplate.MakeHrefString(userDetailUrlTemplate.ParameterReplace(x.TreatmentBMPAssessment.PerformedByPersonID), x.TreatmentBMPAssessment.PerformedByPersonName), 120, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Field Visit Type", x => x.TreatmentBMPAssessment.FieldVisitTypeDisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Assessment Type", x => x.TreatmentBMPAssessment.TreatmentBMPAssessmentTypeDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.TreatmentBMPAssessment.IsAssessmentComplete ? "Complete" : "In Progress", 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Score", x => x.TreatmentBMPAssessment.AssessmentScore?.ToString("0.0") ?? "-", 80, DhtmlxGridColumnFilterType.Numeric);
            foreach (var treatmentBMPAssessmentObservationType in allObservationTypes)
            {
                Add(treatmentBMPAssessmentObservationType.DisplayNameWithUnits(),
                    x => x?.CalculateObservationValueForObservationType(treatmentBMPAssessmentObservationType), 150, DhtmlxGridColumnFilterType.Text);
            }
        }
    }
}
