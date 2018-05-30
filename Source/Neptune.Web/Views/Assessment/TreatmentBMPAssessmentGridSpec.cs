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

using System.Collections.Generic;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Assessment
{
    public class TreatmentBMPAssessmentGridSpec : GridSpec<Models.TreatmentBMPAssessment>
    {
        public TreatmentBMPAssessmentGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Conducted By", x => x.GetFieldVisitPerson.FullNameFirstLast, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date", x => x.GetAssessmentDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Water Year", x => x.GetWaterYear().ToString("0000"), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Score", x => x.AlternateAssessmentScore ?? x.CalculateAssessmentScore(), 80);
            Add("Status", x => x.HasCalculatedOrAlternateScore() ? "Complete" : "In Progress", 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.AlternativeScore.ToGridHeaderString(), x => x.AlternateAssessmentScore.HasValue ? "Yes" : "No", 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Alternative Score Rationale", x => x.AlternateAssessmentRationale, 200);
        }
    }
}
