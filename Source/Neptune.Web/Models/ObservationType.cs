/*-----------------------------------------------------------------------
<copyright file="ObservationType.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class ObservationType : IAuditableEntity
    {
        public string AssessmentUrl(int treatmentBMPAssessmentID)
        {
            throw new NotImplementedException();
        }

        public string BenchmarkAndThresholdUrl(int treatmentBMPID)
        {
            throw new NotImplementedException();
        }

        public bool IsComplete(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationTypeID);

            if (treatmentBMPObservation == null)
            {
                return false;
            }

            return IsComplete(treatmentBMPObservation);
        }
        public bool IsComplete(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public double CalculateScore(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationTypeID);

            if (!IsComplete(treatmentBMPObservation))
            {
                throw new Exception("Observation not complete, cannot calculate score");
            }
            return CalculateScore(treatmentBMPObservation);
        }

        public string FormattedScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            var score = CalculateScore(treatmentBMPObservation);
            return score.ToString("0.0");
        }

        public double CalculateScore(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public double GetObservationValue(TreatmentBMPObservation treatmentBMPObservation)
        {
            return GetObservationValueImpl(treatmentBMPObservation);
        }

        protected double GetObservationValueImpl(TreatmentBMPObservation treatmentBMPObservation)
        {
            throw new NotImplementedException();
        }

        public bool IsBenchmarkAndThresholdComplete(TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.ObservationTypeID == ObservationTypeID) != null;
        }

        public bool HasBenchmarkAndThreshold => ObservationTypeSpecification.ObservationThresholdType != ObservationThresholdType.None;
        public bool ThresholdPercentDecline => ObservationTypeSpecification.ObservationThresholdType == ObservationThresholdType.PercentFromBenchmark;

        public string AuditDescriptionString => "todo";
    }

   
}
