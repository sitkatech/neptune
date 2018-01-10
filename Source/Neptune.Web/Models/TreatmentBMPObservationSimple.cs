using System.Linq;

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationSimple
    {       
        public string ObservationScore { get; }
        public double? ObservationValue { get; }
        public bool IsComplete { get; }
        public string OverrideScoreText { get; }
        public bool OverrideScore { get; }
        public TreatmentBMPObservationSimple(TreatmentBMPObservation treatmentBMPObservation)
        {           
            ObservationScore = treatmentBMPObservation.FormattedObservationScore();
            ObservationValue = treatmentBMPObservation.CalculateObservationValue();
            IsComplete = treatmentBMPObservation.IsComplete();

            var observationType = treatmentBMPObservation.ObservationType;
            var treatmentBMPType = treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;

            var overrideAssessmentScoreIfFailing = observationType.TreatmentBMPTypeObservationTypes.SingleOrDefault(x => x.TreatmentBMPType == treatmentBMPType)?.OverrideAssessmentScoreIfFailing ?? false;

            OverrideScore = overrideAssessmentScoreIfFailing && IsComplete && treatmentBMPObservation.OverrideScoreForFailingObservation(observationType);
            OverrideScoreText = "One or more observations resulted in a failing score";
        }
    }
}