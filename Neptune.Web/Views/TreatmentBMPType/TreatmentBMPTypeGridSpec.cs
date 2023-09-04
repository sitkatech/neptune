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

using Neptune.Web.Security;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class TreatmentBMPTypeGridSpec : GridSpec<EFModels.Entities.TreatmentBMPType>
    {
        public TreatmentBMPTypeGridSpec(LinkGenerator linkGenerator, Person currentPerson)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, t => t.Edit(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, t => t.Delete(UrlTemplate.Parameter1Int)));
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.TreatmentBMPTypeID), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(editUrlTemplate.ParameterReplace(x.TreatmentBMPTypeID), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.TreatmentBMPTypeID), a.TreatmentBMPTypeName), 400, DhtmlxGridColumnFilterType.Html);
            Add($"Number of {FieldDefinitionType.TreatmentBMPAssessmentObservationType.ToGridHeaderStringPlural("Observation Types")}", a => a.TreatmentBMPTypeAssessmentObservationTypes.Select(x => x.TreatmentBMPAssessmentObservationType).Count(), 100);
            Add($"Number of {FieldDefinitionType.TreatmentBMP.ToGridHeaderStringPlural("Treatment BMPs")}", a => a.TreatmentBMPs.Count, 100, DhtmlxGridColumnAggregationType.Total);
        }
    }
}
