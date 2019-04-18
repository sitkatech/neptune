/*-----------------------------------------------------------------------
<copyright file="ProvisionalFieldVisitGridSpec.cs" company="Tahoe Regional Planning Agency">
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
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.ManagerDashboard
{
    public class ProvisionalBMPDelineationsGridSpec : GridSpec<Models.FieldVisit>
    {
        public ProvisionalBMPDelineationsGridSpec(Person currentPerson, string gridName)
        {
            ObjectNameSingular = "Field Visit";
            ObjectNamePlural = "Field Visits";

            ArbitraryHeaderHtml = new List<string>
            {
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.checkAll()",
                    "glyphicon-check", "Select All"),
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.uncheckAll()",
                    "glyphicon-unchecked", "Unselect All")
            };
            AddCheckBoxColumn();
            Add("EntityID", x => x.FieldVisitID, 0);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    new FieldVisitDeleteFeature().HasPermission(currentPerson, x).HasPermission), 30,
                DhtmlxGridColumnFilterType.None);

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

            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add("BMP Type", x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 100, DhtmlxGridColumnFilterType.Text);
            Add("Delineation Type", x => x.TreatmentBMP.Delineation.DelineationType.DelineationTypeDisplayName,100, DhtmlxGridColumnFilterType.Text);
            Add("Delineation Area (to 1/100th acre)", x => x.TreatmentBMP.Delineation?.GetDelineationAreaString(), 50,
                DhtmlxGridColumnFilterType.Text);
            Add("Date of Last Delineation Verification", x => x.TreatmentBMP.Delineation.DateLastVerified.ToString() ?? "Never Verified", 100,
                DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(),
                x => x.TreatmentBMP.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 140,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}