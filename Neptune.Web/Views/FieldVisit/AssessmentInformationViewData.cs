/*-----------------------------------------------------------------------
<copyright file="AssessmentInformationViewData.cs" company="Tahoe Regional Planning Agency">
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

using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentInformationViewData : FieldVisitSectionViewData
    {
        public readonly IEnumerable<SelectListItem> JurisdictionPeople;
        public EFModels.Entities.TreatmentBMPAssessment TreatmentBMPAssessment { get; }

        public AssessmentInformationViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, IEnumerable<SelectListItem> jurisdictionPeople, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
            : base(httpContext, linkGenerator, currentPerson, fieldVisit, treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial ? (EFModels.Entities.FieldVisitSection)EFModels.Entities.FieldVisitSection.Assessment : EFModels.Entities.FieldVisitSection.PostMaintenanceAssessment)
        {
            JurisdictionPeople = jurisdictionPeople;
            SubsectionName = "Assessment Information";
            TreatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);
        }  
    }
}