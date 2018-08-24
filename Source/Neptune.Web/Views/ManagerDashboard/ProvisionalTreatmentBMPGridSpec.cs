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
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ManagerDashboard
{
    public class ProvisionalTreatmentBMPGridSpec : GridSpec<Models.TreatmentBMPAssessment>
    {
        public ProvisionalTreatmentBMPGridSpec(Person currentPerson,
            IEnumerable<Models.TreatmentBMPAssessmentObservationType> allObservationTypes)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(y => y.Inventory(x.GetFieldVisit().PrimaryKey)), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date of last Verification", x => x.TreatmentBMP.DateOfLastInventoryVerification.ToStringDate(), 125, DhtmlxGridColumnFilterType.Html);
            Add("Date of last Field Visit that included an Update to BMP Record", x => x.TreatmentBMP.FieldVisits.Select(y => y.VisitDate).FirstOrDefault().ToStringDate(), 120, DhtmlxGridColumnFilterType.Html);

            Add("Has Photos?", x => x.TreatmentBMP.TreatmentBMPImages.Any().ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Benchmark and Thresholds Set?", x => x.TreatmentBMP.IsBenchmarkAndThresholdsComplete().ToString(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
