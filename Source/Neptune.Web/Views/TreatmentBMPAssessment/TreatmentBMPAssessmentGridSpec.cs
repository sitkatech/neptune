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

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class TreatmentBMPAssessmentGridSpec : GridSpec<Models.TreatmentBMPAssessment>
    {
        public TreatmentBMPAssessmentGridSpec(Person currentPerson)
        {
            ObjectNameSingular = "Assessment";
            ObjectNamePlural = "Assessments";
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), x.CanEdit(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("Conducted By", x => x.Person.FullNameFirstLast, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date", x => x.AssessmentDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Score", x => x.AlternateAssessmentScore ?? x.CalculateAssessmentScore(), 80);
            Add(Models.FieldDefinition.AssessmentForInternalUseOnly.ToGridHeaderString(), x => x.IsPrivate.ToYesNo(), 80);
            Add(Models.FieldDefinition.TypeOfAssessment.ToGridHeaderString(), x => x.StormwaterAssessmentType.StormwaterAssessmentTypeDisplayName, 100);
        }
    }
}
