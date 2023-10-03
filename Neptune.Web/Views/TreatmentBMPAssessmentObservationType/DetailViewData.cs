/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; }
        public bool UserHasObservationTypeManagePermissions { get; }
        public string ViewSchemaDetailUrl { get; }
        public TreatmentBMPTypeGridSpec TreatmentBMPTypeGridSpec { get; }
        public string TreatmentBMPTypeGridName { get; }
        public string TreatmentBMPTypeGridDataUrl { get; }
        public EditViewModel ViewModelForPreview { get; }
        public string PreviewUrl { get; }
        public string EditUrl { get; }


        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationType;
            EntityName = "Observation Type";
            EntityUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;

            EditUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(treatmentBMPAssessmentObservationType));

            UserHasObservationTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            ViewSchemaDetailUrl = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType, linkGenerator);

            TreatmentBMPTypeGridSpec = new TreatmentBMPTypeGridSpec(linkGenerator, currentPerson, new Dictionary<int, int>())
            {
                ObjectNameSingular = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            TreatmentBMPTypeGridName = "treatmentBMPTypeGridForObservationType";
            TreatmentBMPTypeGridDataUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, tc => tc.TreatmentBMPTypeGridJsonData(TreatmentBMPAssessmentObservationType));
            ViewModelForPreview = new EditViewModel(treatmentBMPAssessmentObservationType);
            PreviewUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, x => x.PreviewObservationType());
        }
    }
}
