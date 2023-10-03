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

using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
{
    public class TreatmentBMPAssessmentObservationTypeGridSpec : GridSpec<EFModels.Entities.TreatmentBMPAssessmentObservationType>
    {
        public TreatmentBMPAssessmentObservationTypeGridSpec(LinkGenerator linkGenerator, Person currentPerson)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentObservationTypeID), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson), true), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(editUrlTemplate.ParameterReplace(x.TreatmentBMPAssessmentObservationTypeID), new NeptuneAdminFeature().HasPermissionByPerson(currentPerson)), 30, DhtmlxGridColumnFilterType.None);
            Add(FieldDefinitionType.TreatmentBMPAssessmentObservationType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.TreatmentBMPAssessmentObservationTypeID), a.TreatmentBMPAssessmentObservationTypeName), 200, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.ObservationCollectionMethod.ToGridHeaderString(), a => a.ObservationTypeSpecification.ObservationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.ObservationTargetType.ToGridHeaderString(), a => a.ObservationTypeSpecification.ObservationTargetType.ObservationTargetTypeDisplayName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.ObservationThresholdType.ToGridHeaderString(), a => a.ObservationTypeSpecification.ObservationThresholdType.ObservationThresholdTypeDisplayName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            
        }
    }
}
