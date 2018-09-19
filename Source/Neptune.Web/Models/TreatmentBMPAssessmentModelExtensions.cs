/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentModelExtensions.cs" company="Tahoe Regional Planning Agency">
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
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPAssessmentModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return DetailUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static readonly UrlTemplate<int> EditInitialAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Assessment(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> EditPostMaintenanceAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.PostMaintenanceAssessment(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (treatmentBMPAssessment.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial){
                return EditInitialAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
            }

            return EditPostMaintenanceAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }
    }
}
