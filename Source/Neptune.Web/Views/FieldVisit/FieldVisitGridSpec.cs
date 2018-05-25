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

using System.Collections.Generic;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitGridSpec : GridSpec<Models.FieldVisit>
    {
        public FieldVisitGridSpec(Person currentPerson)
        {
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";
            Add("Visit Date", x => x.VisitDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("Performed By", x => x.PerformedByPerson.GetFullNameFirstLastAsUrl(), 105, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Status", x => x.FieldVisitStatus.FieldVisitStatusDisplayName, 85, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Initial Assessment?", x => (x.InitialAssessment != null) ? "Yes" : "No", 95, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Maintenance Occurred?", x => (x.MaintenanceRecord != null) ? "Yes" : "No", 85, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Post-Maintenance Assessment?", x => (x.PostMaintenanceAssessment != null) ? "Yes" : "No", 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
