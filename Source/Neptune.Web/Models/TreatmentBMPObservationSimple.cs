namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationSimple
    {       
        public string ObservationScore { get; }
        public double ObservationValue { get; }
        public bool IsComplete { get; }
        public string OverrideScoreText { get; }
        public bool OverrideScore { get; }
        public TreatmentBMPObservationSimple(TreatmentBMPObservation treatmentBMPObservation)
        {           
            ObservationScore = treatmentBMPObservation.FormattedObservationScore();
            ObservationValue = treatmentBMPObservation.CalculateObservationScore();
            IsComplete = treatmentBMPObservation.IsComplete();
            OverrideScore = true;
            OverrideScoreText = "test";
        }
    }
}