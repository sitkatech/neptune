/*-----------------------------------------------------------------------
<copyright file="ProvisionalTreatmentBMPGridSpec.cs" company="Tahoe Regional Planning Agency">
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
    public class ProvisionalTreatmentBMPGridSpec : GridSpec<Models.TreatmentBMP>
    {
        public ProvisionalTreatmentBMPGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(y => y.Inventory(x.PrimaryKey)), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => x.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => x.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMPType.TreatmentBMPTypeName, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date of Last Verification", x => x.DateOfLastInventoryVerification, 125);
            Add("Date of Last Field Visit", x => x.GetLastFieldVisitWithAnInventoryUpdate()?.VisitDate, 120);

            Add("Has Photos?", x => x.TreatmentBMPImages.Any().ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Benchmark and Thresholds Set?", x => x.IsBenchmarkAndThresholdsComplete().ToString(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
