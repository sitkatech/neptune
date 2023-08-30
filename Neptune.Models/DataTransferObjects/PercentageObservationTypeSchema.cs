namespace Neptune.Models.DataTransferObjects;

public class PercentageObservationTypeSchema
{
    public string MeasurementUnitLabel { get; set; }

    public List<string> PropertiesToObserve { get; set; }
    public string BenchmarkDescription { get; set; }
    public string ThresholdDescription { get; set; }
    public string AssessmentDescription { get; set; }
}