/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class TreatmentBMPTypeGridSpec : GridSpec<Models.TreatmentBMPType>
    {
        public TreatmentBMPTypeGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, !x.HasDependentObjects()), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), true), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.TreatmentBMPTypeName), 400, DhtmlxGridColumnFilterType.Html);
            Add($"Number of {Models.FieldDefinition.ObservationType.ToGridHeaderStringPlural("Observation Types")}", a => a.TreatmentBMPTypeObservationTypes.Select(x => x.ObservationType).Count(), 100);
            Add($"Number of {Models.FieldDefinition.TreatmentBMP.ToGridHeaderStringPlural("Treatment BMP Types")}", a => a.TreatmentBMPs.Count, 100, DhtmlxGridColumnAggregationType.Total);
        }
    }
}
