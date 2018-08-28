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

        public double? CalculateAssessmentScore()
        {
            if (!TreatmentBMP.IsBenchmarkAndThresholdsComplete())
            {
                return null;
            }

            if (!IsAssessmentComplete())
            {
                return null;
            }           

            //if any observations that override the score have a failing score, return 0
            var observationTypesThatPotentiallyOverrideScore = TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => x.OverrideAssessmentScoreIfFailing)
                .ToList().Select(x => x.TreatmentBMPAssessmentObservationType);

            if (observationTypesThatPotentiallyOverrideScore.Any(x =>
                {
                    var treatmentBMPObservation = TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType == x);
                    return treatmentBMPObservation?.OverrideScoreForFailingObservation(x) ?? false;
                }))
            {
                return 0;
            }

            //if all observations override the score and all are passing, return 5
            if (TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .All(x => x.OverrideAssessmentScoreIfFailing))
            {
                return 5;
            }

            //otherwise calculate the score
            var score = TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => !x.OverrideAssessmentScoreIfFailing)
                .Select(x => x.TreatmentBMPAssessmentObservationType).ToList().Sum(x =>
                {
                    var observationScore = TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == x.TreatmentBMPAssessmentObservationTypeID).CalculateObservationScore();

                    var treatmentBMPAssessmentObservationType = TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == x.TreatmentBMPAssessmentObservationTypeID).TreatmentBMPAssessmentObservationType;
                    var observationWeight = Convert.ToDouble(TreatmentBMP.TreatmentBMPType
                        .GetTreatmentBMPTypeObservationType(treatmentBMPAssessmentObservationType).AssessmentScoreWeight
                        .Value);
                    return observationScore * observationWeight;
                });

            return Math.Round(score.Value, 1);
        }

        public string FormattedScore()
        {
            var score = CalculateAssessmentScore();
            return score?.ToString("0.0") ?? "-";
        }

        public bool IsObservationComplete(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);

            return treatmentBMPObservation != null;
        }

        public FieldVisit GetFieldVisit()
        {
            return FieldVisitWhereYouAreTheInitialAssessment ??
                   FieldVisitWhereYouAreThePostMaintenanceAssessment;
        }

        public Person GetFieldVisitPerson()
        {
            return GetFieldVisit().PerformedByPerson;
        }


        public DateTime GetAssessmentDate()
        {
            return GetFieldVisit().VisitDate;
        }

        public bool IsInitialAssessment()
        {
            return FieldVisitWhereYouAreTheInitialAssessment != null;
        }

        public bool IsPostMaintenanceAssessment()
        {
            return FieldVisitWhereYouAreThePostMaintenanceAssessment != null;
        }

        public string CalculateObservationValueForObservationType(TreatmentBMPAssessmentObservationType observationType)
        {
            if (!TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Select(x => x.TreatmentBMPAssessmentObservationType).Contains(observationType))
                return "N/A";
            return TreatmentBMPObservations?.SingleOrDefault(y =>
                       y.TreatmentBMPAssessmentObservationTypeID ==
                       observationType.TreatmentBMPAssessmentObservationTypeID)?.FormattedObservationValue() ?? "Not Provided";
        }
    }
}
