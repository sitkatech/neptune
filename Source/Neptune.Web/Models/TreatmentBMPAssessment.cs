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
            return TreatmentBMP.TreatmentBMPType.GetObservationTypes().All(x => x.IsComplete(this));
        }

        public bool HasCalculatedOrAlternateScore()
        {
            return IsAssessmentComplete() || AlternateAssessmentScore != null;
        }

        public bool IsPublicRegularAssessment()
        {
            return StormwaterAssessmentType == StormwaterAssessmentType.Regular && !IsPrivate;
        }

        private bool OverrideScore(ObservationType observationType)
        {
            if (TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType == observationType) != null)
            {
                if (Math.Abs(observationType.CalculateScore(this) - 2) < 0.01)
                {
                    return true;
                }
            }
            return false;
        }

        public double? CalculateAssessmentScore()
        {
            if (!IsAssessmentComplete())
            {
                return null;
            }

            if (TreatmentBMP.TreatmentBMPType.GetObservationTypes().Where(x => !x.HasBenchmarkAndThreshold).Select(OverrideScore).Any(x => x))
            {
                return 2;
            }
            if (!TreatmentBMP.TreatmentBMPType.GetObservationTypes().Any(x => x.HasBenchmarkAndThreshold))
            {
                return 5;
            }

            var score = TreatmentBMP.TreatmentBMPType.GetObservationTypes().Where(x => x.HasBenchmarkAndThreshold).Sum(x =>
            {
                var treatmentBMPType = TreatmentBMP.TreatmentBMPType;
                var observationType = TreatmentBMPObservations.SingleOrDefault(y => y.ObservationType.ObservationTypeID == x.ObservationTypeID).ObservationType;
                var observationScore = observationType.CalculateScore(this);
                
                var observationWeight = treatmentBMPType.GetTreatmentBMPTypeObservationType(observationType).AssessmentScoreWeight.Value;
                return observationScore * observationWeight;
            });
            return Math.Round(score, 1);
        }

        public string FormattedScore()
        {
            var score = CalculateAssessmentScore();
            return score?.ToString("0.0") ?? "-";
        }
    }
}
