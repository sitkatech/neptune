﻿/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPGridSpec.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPGridSpec : GridSpec<Models.TreatmentBMP>
    {
        public TreatmentBMPGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), x.CanEdit(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(x.GetJurisdictionSummaryUrl(), x.StormwaterJurisdiction.OrganizationDisplayName), 170);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMPType.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Notes", x => x.Notes, 195);
            Add("Last Assessment Date", x => x.GetMostRecentAssessment()?.GetAssessmentDate, 130);
            Add("Last Assessed Score", x => x.GetMostRecentScoreAsString(), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("# of Assessments", x => x.TreatmentBMPAssessments.Count, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);                
            Add("Last Maintenance Date", x => x.LastMaintainedDateTime(), 130, DhtmlxGridColumnFormatType.Date);
            Add("# of Maintenance Events", x => x.MaintenanceRecords.Count, 100, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add("Benchmark and Threshold Set?", x => x.IsBenchmarkAndThresholdsComplete().ToYesOrEmpty(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
