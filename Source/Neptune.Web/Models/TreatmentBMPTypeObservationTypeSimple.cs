namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeObservationTypeSimple
    {
        public int TreatmentBMPTypeID { get; set; }
        public int ObservationTypeID { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool? OverrideAssessmentScoreIfFailing { get; set; }

        public TreatmentBMPTypeObservationTypeSimple()
        {
        }

        public TreatmentBMPTypeObservationTypeSimple(TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType)
        {
            TreatmentBMPTypeID = TreatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeID;
            ObservationTypeID = TreatmentBMPTypeAssessmentObservationType.ObservationTypeID;
            AssessmentScoreWeight = TreatmentBMPTypeAssessmentObservationType.AssessmentScoreWeight * 100;
            DefaultThresholdValue = TreatmentBMPTypeAssessmentObservationType.DefaultThresholdValue;
            DefaultBenchmarkValue = TreatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue;
            OverrideAssessmentScoreIfFailing = TreatmentBMPTypeAssessmentObservationType.OverrideAssessmentScoreIfFailing;
        }
    }
}