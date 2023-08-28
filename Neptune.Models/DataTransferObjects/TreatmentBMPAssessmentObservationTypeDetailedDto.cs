namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPAssessmentObservationTypeDetailedDto
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public bool TargetIsSweetSpot { get; set; }
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }   
        public int? TreatmentBMPAssessmentObservationTypeSortOrder { get; set; }   
        public string BenchmarkUnitLegendDisplayName { get; set; }
        public string ThresholdUnitLegendDisplayName { get; set; }
        public string CollectionMethodDisplayName { get; set; } 
    }
}