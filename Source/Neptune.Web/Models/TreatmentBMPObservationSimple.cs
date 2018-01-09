namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationSimple
    {
        public bool IsComplete { get; set; }
        public bool OverrideScore { get; set; }
        public string OverrideScoreText { get; set; }
        public double? ObservedValue { get; set; }
        public string ObservationScore { get; set; }

        public TreatmentBMPObservationSimple(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationType = treatmentBMPObservation.ObservationType;
            IsComplete = treatmentBMPObservation.IsComplete();
            OverrideScore = !observationType.HasBenchmarkAndThreshold && IsComplete ? treatmentBMPObservation.CalculateScoreForObservationType() == 2 : false;
            OverrideScoreText = string.Empty;
            ObservedValue = IsComplete ? (double?)treatmentBMPObservation.GetObservationValue(observationType) : null;
            ObservationScore = IsComplete ? treatmentBMPObservation.FormattedScoreForObservationType() : "-";
        }
    }
}