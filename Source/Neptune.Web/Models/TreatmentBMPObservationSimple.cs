﻿namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationSimple
    {       
        public string ObservationScore { get; }
        public double? ObservationValue { get; }
        public bool IsComplete { get; set; }
        public string OverrideScoreText { get; }
        public bool OverrideScore { get; set; }

        public TreatmentBMPObservationSimple()
        {
        }

        public TreatmentBMPObservationSimple(TreatmentBMPObservation treatmentBMPObservation,
            bool overrideAssessmentScoreIfFailing)
        {           
            ObservationScore = overrideAssessmentScoreIfFailing ? string.Empty : treatmentBMPObservation.FormattedObservationScore();
            ObservationValue = treatmentBMPObservation.CalculateObservationValue();
            IsComplete = treatmentBMPObservation.IsComplete();

            OverrideScore = overrideAssessmentScoreIfFailing && IsComplete && treatmentBMPObservation.OverrideScoreForFailingObservation(treatmentBMPObservation.TreatmentBMPAssessmentObservationType);
            OverrideScoreText = treatmentBMPObservation.CalculateOverrideScoreText(overrideAssessmentScoreIfFailing);
        }
    }
}