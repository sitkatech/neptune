/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessment.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Views;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAssessment : IAuditableEntity
    {
        public bool CanEdit(Person currentPerson)
        {
            var canManageStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);
            return canManageStormwaterJurisdiction;
        }

        public bool CanDelete(Person currentPerson)
        {
            var canManageStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);
            return canManageStormwaterJurisdiction;
        }

        public string GetAuditDescriptionString()
        {
            return $"Assessment deleted.";
        }

        public int GetWaterYear()
        {
            return GetAssessmentDate().GetFiscalYear();
        }

        public bool IsAssessmentComplete()
        {
            return TreatmentBMP.TreatmentBMPType.GetObservationTypes().All(IsObservationComplete);
        }

        public string AssessmentStatus()
        {
            var completedObservationCount =
                TreatmentBMP.TreatmentBMPType.GetObservationTypes().Count(IsObservationComplete);
            var totalObservationCount = TreatmentBMP.TreatmentBMPType.GetObservationTypes().Count;
            return IsAssessmentComplete()
                ? "Complete"
                : $"Incomplete ({completedObservationCount} of {totalObservationCount} observations complete)";
        }

        public bool HasCalculatedOrAlternateScore()
        {
            return IsAssessmentComplete();
        }

        public string FormattedScore()
        {
            var score = this.CalculateAssessmentScore();
            return score?.ToString("0.0") ?? "-";
        }

        public bool IsObservationComplete(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);

            return treatmentBMPObservation != null;
        }

        public Person GetFieldVisitPerson()
        {
            return FieldVisit.PerformedByPerson;
        }


        public DateTime GetAssessmentDate()
        {
            return FieldVisit.VisitDate;
        }

        public string CalculateObservationValueForObservationType(TreatmentBMPAssessmentObservationType observationType)
        {
            if (!TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.Select(x => x.TreatmentBMPAssessmentObservationType).Contains(observationType))
            {
                return ViewUtilities.NaString;
            }

            return TreatmentBMPObservations?.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationTypeID == observationType.TreatmentBMPAssessmentObservationTypeID)?.FormattedObservationValue() ?? ViewUtilities.NotProvidedString;
        }
    }
}
