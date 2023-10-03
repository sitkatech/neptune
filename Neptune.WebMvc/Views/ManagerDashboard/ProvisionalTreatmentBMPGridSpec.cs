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

using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.ManagerDashboard
{
    public class ProvisionalTreatmentBMPGridSpec : GridSpec<EFModels.Entities.TreatmentBMP>
    {
        public ProvisionalTreatmentBMPGridSpec(LinkGenerator linkGenerator, Person currentPerson, string gridName)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Delete(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            ArbitraryHeaderHtml = new List<string> { DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.checkAll()", "glyphicon-check", "Select All"), DatabaseContextExtensions.GetCheckboxSelectingUrl($"Sitka.{gridName}.grid.uncheckAll()", "glyphicon-unchecked", "Unselect All") };
            AddCheckBoxColumn();
            Add("EntityID", x => x.TreatmentBMPID, 0);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.CanDelete(currentPerson), x.CanDelete(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), "View", new Dictionary<string, string> { { "class", "gridButton" } }), 50, DhtmlxGridColumnFilterType.None);
            Add("BMP Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMPName), 120, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString(), x => x.TreatmentBMPType.TreatmentBMPTypeName, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date of Last BMP Record Verification", x => x.DateOfLastInventoryVerification, 125, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.DateOfLastInventoryChange.ToGridHeaderString(), x => x.InventoryLastChangedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Has Photos?", x => x.TreatmentBMPImages.Any().ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Benchmark and Thresholds Set?", x => x.IsBenchmarkAndThresholdsComplete(x.TreatmentBMPType).ToYesNo(), 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.GetOrganizationDisplayName()), 140, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}
