﻿/*-----------------------------------------------------------------------
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

using Neptune.WebMvc.Models;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.ManagerDashboard
{
    public class ProvisionalBMPDelineationsGridSpec : GridSpec<EFModels.Entities.Delineation>
    {
        public ProvisionalBMPDelineationsGridSpec(LinkGenerator linkGenerator, Person currentPerson, string gridName)
        {
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var delineationMapUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(UrlTemplate.Parameter1Int)));

            ObjectNameSingular = "Delineation";
            ObjectNamePlural = "Delineations";

            ArbitraryHeaderHtml = new List<string>
            {
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.checkAll()",
                    "glyphicon-check", "Select All"),
                DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.uncheckAll()",
                    "glyphicon-unchecked", "Unselect All")
            };
            AddCheckBoxColumn();
            Add("EntityID", x => x.DelineationID, 0);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.DelineationID), new DelineationDeleteFeature().HasPermission(currentPerson, x.TreatmentBMP).HasPermission), 20,
                DhtmlxGridColumnFilterType.None);
            Add(string.Empty,x => UrlTemplate.MakeHrefString(delineationMapUrlTemplate.ParameterReplace(x.TreatmentBMPID), "View", new Dictionary<string, string> {{"class", "gridButton"}}), 45, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            Add("BMP Type", x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Delineation Type", x => x.DelineationType.DelineationTypeDisplayName,80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Delineation Area (ac)", x => x.GetDelineationArea(), 75);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 120,
                DhtmlxGridColumnFormatType.Date);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 120,
                DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(),
                x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.StormwaterJurisdictionID), x.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}