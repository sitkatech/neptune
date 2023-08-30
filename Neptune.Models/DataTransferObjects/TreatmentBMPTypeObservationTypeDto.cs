namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPTypeObservationTypeDto
    {
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool? OverrideAssessmentScoreIfFailing { get; set; }
    }
}