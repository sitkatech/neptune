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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; }
        public bool UserHasObservationTypeManagePermissions { get; }
        public string ViewSchemaDetailUrl { get; }
        public TreatmentBMPTypeGridSpec TreatmentBMPTypeGridSpec { get; }
        public string TreatmentBMPTypeGridName { get; }
        public string TreatmentBMPTypeGridDataUrl { get; }
        public EditViewModel ViewModelForPreview { get; }
        public string PreviewUrl { get; }


        public DetailViewData(Person currentPerson,
            Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType) : base(currentPerson)
        {
            TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationType;
            EntityName = "Observation Type";
            EntityUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(c => c.Index());
            PageTitle = TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;

            UserHasObservationTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            ViewSchemaDetailUrl = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.ViewSchemaDetailUrl(TreatmentBMPAssessmentObservationType);

            TreatmentBMPTypeGridSpec = new TreatmentBMPTypeGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            TreatmentBMPTypeGridName = "treatmentBMPTypeGridForObservationType";
            TreatmentBMPTypeGridDataUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(tc => tc.TreatmentBMPTypeGridJsonData(TreatmentBMPAssessmentObservationType));
            ViewModelForPreview = new EditViewModel(treatmentBMPAssessmentObservationType);
            PreviewUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(x => x.PreviewObservationType());
        }
    }
}
