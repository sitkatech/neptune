/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservation.cs" company="Tahoe Regional Planning Agency">
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

using System;
using LtInfo.Common.Views;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPObservation : IAuditableEntity
    {
        public bool IsComplete()
        {
            return true; //todo
        }
        public double CalculateObservationScore()
        {
            return 0; //todo
        }

        public string FormattedObservationScore()
        {
            var score = CalculateObservationScore();
            return score.ToString("0.0");
        }

        public string AuditDescriptionString
        {
            get
            {
                var assessment = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.GetTreatmentBMPAssessment(TreatmentBMPAssessmentID);
                var treatmentBMP = assessment == null ? null : HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(assessment.TreatmentBMPID);
                var assessmentDate = assessment?.AssessmentDate.ToShortDateString() ?? ViewUtilities.NotFoundString;
                var treatmentBMPName = treatmentBMP != null ? treatmentBMP.TreatmentBMPName : ViewUtilities.NotFoundString;

                return $"Observation for BMP {treatmentBMPName} ({ObservationType.ObservationTypeName}) on Assessment Dated {assessmentDate}";
            }
        }
    }
}
