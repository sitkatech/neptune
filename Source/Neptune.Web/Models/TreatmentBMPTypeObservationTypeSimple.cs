namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeObservationTypeSimple
    {
        public int TreatmentBMPTypeID { get; }
        public int ObservationTypeID { get; }
        public double? AssessmentScoreWeight { get; }
        public double? DefaultThresholdValue { get; }
        public double? DefaultBenchmarkValue { get; }
        public bool? OverrideAssessmentScoreIfFailing { get; }

        public TreatmentBMPTypeObservationTypeSimple()
        {
        }

        public TreatmentBMPTypeObservationTypeSimple(TreatmentBMPTypeObservationType treatmentBMPTypeObservationType)
        {
            TreatmentBMPTypeID = treatmentBMPTypeObservationType.TreatmentBMPTypeID;
            ObservationTypeID = treatmentBMPTypeObservationType.ObservationTypeID;
            AssessmentScoreWeight = treatmentBMPTypeObservationType.AssessmentScoreWeight;
            DefaultThresholdValue = treatmentBMPTypeObservationType.DefaultThresholdValue;
            DefaultBenchmarkValue = treatmentBMPTypeObservationType.DefaultBenchmarkValue;
            OverrideAssessmentScoreIfFailing = treatmentBMPTypeObservationType.OverrideAssessmentScoreIfFailing;
        }
    }
}