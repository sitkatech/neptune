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

using LtInfo.Common;
using Neptune.Common;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPAssessment
    {
        public bool CanEdit(Person currentPerson)
        {
            var canManageStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
            return canManageStormwaterJurisdiction;
        }

        public bool CanDelete(Person currentPerson)
        {
            var canManageStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
            return canManageStormwaterJurisdiction;
        }

        public int GetWaterYear()
        {
            return GetAssessmentDate().GetFiscalYear();
        }

        public string GetAssessmentStatus()
        {
            var completedObservationCount =
                TreatmentBMP.TreatmentBMPType.GetObservationTypes().Count(IsObservationComplete);
            var totalObservationCount = TreatmentBMP.TreatmentBMPType.GetObservationTypes().Count;
            return IsAssessmentComplete
                ? "Complete"
                : $"Incomplete ({completedObservationCount} of {totalObservationCount} observations complete)";
        }

        public string FormattedScore()
        {
            return AssessmentScore?.ToString("0.0") ?? "-";
        }

        public bool IsObservationComplete(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = TreatmentBMPObservations.FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationTypeID ==
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

        public string CalculateObservationValueForObservationType(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            if (treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.All(x => x.TreatmentBMPTypeID != TreatmentBMPTypeID))
            {
                return "n/a";
            }

            return TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID)?.FormattedObservationValueWithoutUnits(treatmentBMPAssessmentObservationType) ?? "not provided";
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
