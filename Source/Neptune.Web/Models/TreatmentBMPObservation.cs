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
using System.Linq;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Views.ObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPObservation : IAuditableEntity
    {
        public DiscreteObservationSchema DiscreteObservationData => JsonConvert.DeserializeObject<DiscreteObservationSchema>(ObservationData);

        public bool IsComplete()
        {
            return true; //todo
        }
        public double? CalculateObservationScore()
        {
            var observationTypeCollectionMethod = ObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return observationTypeCollectionMethod.CalculateScore(this);
                case ObservationTypeCollectionMethodEnum.Rate:
                    return 0;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return 0;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return 0;
                default:
                    return null;
            }
        }

        public string FormattedObservationScore()
        {
            var score = CalculateObservationScore();
            return score?.ToString("0.0") ?? "-";
        }

        public double? CalculateObservationValue()
        {
            var observationTypeCollectionMethod = ObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return observationTypeCollectionMethod.GetObservationValueFromObservationData(ObservationData);
                case ObservationTypeCollectionMethodEnum.Rate:
                    return 0;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return 0;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return 0;
                default:
                    return null;
            }
        }

        public bool OverrideScoreForFailingObservation(ObservationType observationType)
        {
            var score = CalculateObservationScore();
            if (score == null)
            {
                return false;
            }
            return Math.Abs(score.Value - 2) < 0.01;
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
