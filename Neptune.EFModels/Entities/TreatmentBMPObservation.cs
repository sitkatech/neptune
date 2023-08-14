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

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPObservation //: IAuditableEntity
    {
        // todo:
        //public DiscreteObservationSchema GetDiscreteObservationData()
        //{
        //    return JsonConvert.DeserializeObject<DiscreteObservationSchema>(ObservationData);
        //}

        //public PassFailObservationSchema GetPassFailObservationData()
        //{
        //    return JsonConvert.DeserializeObject<PassFailObservationSchema>(ObservationData);
        //}

        public bool IsComplete()
        {
            return true; //todo
        }
        public double? CalculateObservationScore()
        {
            return null;
            //todo: return TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.CalculateScore(this);
        }

        public string FormattedObservationScore()
        {
            var score = CalculateObservationScore();
            return score?.ToString("0.0") ?? "-";
        }

        public double? CalculateObservationValue()
        {
            return null;
            //todo: return TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.GetObservationValueFromObservationData(ObservationData);
        }

        public string FormattedObservationValueWithoutUnits(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return string.Empty;
            //todo:
            //var observationTypeCollectionMethod = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod;
            //var observationValue = observationTypeCollectionMethod.GetObservationValueFromObservationData(ObservationData).GetValueOrDefault();

            //if (observationTypeCollectionMethod == ObservationTypeCollectionMethod.PassFail)
            //{
            //    return Math.Abs(observationValue - 5) < 0.0001 ? "Pass" : "Fail";
            //}

            //return $"{observationValue.ToString(CultureInfo.InvariantCulture)}";
        }

        public bool OverrideScoreForFailingObservation(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var score = CalculateObservationScore();
            if ((score?? 0) < 0.01)
            {
                return true;
            }
            return Math.Abs(score.Value - 2) < 0.01;
        }

        public string GetAuditDescriptionString()
        {
            return $"Observation Deleted";
        }

        public string CalculateOverrideScoreText(bool overrideAssessmentScoreIfFailing)
        {
            return string.Empty;
            //todo: return TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod.CalculateOverrideScoreText(ObservationData, TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema, overrideAssessmentScoreIfFailing);
        }
    }
}
