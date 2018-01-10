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
        public bool CanEdit(Person currentPerson, int waterYear)
        {
            var canManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);
            return canManageStormwaterJurisdiction;
        }

        public bool CanDelete(Person currentPerson, int waterYear)
        {
            var canManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);
            return canManageStormwaterJurisdiction;
        }

        public string AuditDescriptionString => $"Assessmentment Dated {AssessmentDate}";

        public int GetWaterYear()
        {
            return AssessmentDate.GetFiscalYear();
        }

        public bool IsAssessmentComplete()
        {
            return TreatmentBMP.TreatmentBMPType.GetObservationTypes().All(IsObservationComplete);
        }

        public bool HasCalculatedOrAlternateScore()
        {
            return IsAssessmentComplete() || AlternateAssessmentScore != null;
        }

        public bool IsPublicRegularAssessment()
        {
            return StormwaterAssessmentType == StormwaterAssessmentType.Regular && !IsPrivate;
        }        

        public double? CalculateAssessmentScore()
        {
            if (!IsAssessmentComplete())
            {
                return null;
            }

            //if any observations that override the score have a failing score, return 2
            if (TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeObservationTypes
                .Where(x => x.OverrideAssessmentScoreIfFailing.HasValue && x.OverrideAssessmentScoreIfFailing.Value)
                .ToList().Select(x => x.ObservationType).Select(x =>
                {
                    var treatmentBMPObservation = TreatmentBMPObservations.SingleOrDefault(y => y.ObservationType == x);
                    return treatmentBMPObservation?.OverrideScoreForFailingObservation(x) ?? false;
                }).Any(x => x))
            {
                return 2;
            }

            //if all observations override the score and all are passing, return 5
            if (TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeObservationTypes
                .All(x => x.OverrideAssessmentScoreIfFailing.HasValue && x.OverrideAssessmentScoreIfFailing.Value))
            {
                return 5;
            }

            //otherwise calculate the score
            var score = TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeObservationTypes
                .Where(x => !x.OverrideAssessmentScoreIfFailing.HasValue || !x.OverrideAssessmentScoreIfFailing.Value)
                .Select(x => x.ObservationType).ToList().Sum(x =>
                {
                    var observationScore = TreatmentBMPObservations.SingleOrDefault(y => y.ObservationType.ObservationTypeID == x.ObservationTypeID).CalculateObservationScore();

                    var observationType = TreatmentBMPObservations.SingleOrDefault(y => y.ObservationType.ObservationTypeID == x.ObservationTypeID).ObservationType;
                    var observationWeight = TreatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(observationType).AssessmentScoreWeight.Value;
                    return observationScore * observationWeight;
                });

            return Math.Round(score.Value, 1);
        }

        public string FormattedScore()
        {
            var score = CalculateAssessmentScore();
            return score?.ToString("0.0") ?? "-";
        }

        public bool IsObservationComplete(ObservationType observationType)
        {
            var treatmentBMPObservation = TreatmentBMPObservations.ToList().FirstOrDefault(x => x.ObservationType.ObservationTypeID == observationType.ObservationTypeID);

            return treatmentBMPObservation != null;
        }
    }
}
