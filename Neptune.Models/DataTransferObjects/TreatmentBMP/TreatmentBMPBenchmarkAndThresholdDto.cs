namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPBenchmarkAndThresholdDto
{
    public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
    public int TreatmentBMPID { get; set; }
    public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
    public int TreatmentBMPTypeID { get; set; }
    public int TreatmentBMPAssessmentObservationTypeID { get; set; }
    public double BenchmarkValue { get; set; }
    public double ThresholdValue { get; set; }
}