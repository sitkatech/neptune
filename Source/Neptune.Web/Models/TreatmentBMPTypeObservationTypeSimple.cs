namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeObservationTypeSimple
    {
        public int TreatmentBMPTypeID { get; set; }
        public int ObservationTypeID { get; set; }
        public double? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool? OverrideAssessmentScoreIfFailing { get; set; }

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