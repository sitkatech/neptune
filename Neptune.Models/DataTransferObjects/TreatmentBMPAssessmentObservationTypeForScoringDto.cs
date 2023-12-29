namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPAssessmentObservationTypeForScoringDto
{
    public int TreatmentBMPAssessmentObservationTypeID { get; set; }
    public bool HasBenchmarkAndThresholds { get; set; }
    public string DisplayName { get; set; }
    public double? ThresholdValueInObservedUnits { get; set; }
    public double? BenchmarkValue { get; set; }
    public string Weight { get; set; }
    public ObservationScoreDto? TreatmentBMPObservationSimple { get; set; }
}