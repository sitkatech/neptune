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
            IsComplete = observationType.IsComplete(treatmentBMPObservation);
            OverrideScore = !observationType.HasBenchmarkAndThreshold && IsComplete ? observationType.CalculateScore(treatmentBMPObservation) == 2 : false;
            OverrideScoreText = string.Empty;
            ObservedValue = IsComplete ? (double?)observationType.GetObservationValue(treatmentBMPObservation) : null;
            ObservationScore = IsComplete ? observationType.FormattedScore(treatmentBMPObservation) : "-";
        }
    }
}