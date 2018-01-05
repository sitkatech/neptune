namespace Neptune.Web.Models
{
    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public string ObservationTypeName { get; set; }   
        public string BenchmarkUnitLegendDisplayName { get; set; }
        public string ThresholdUnitLegendDisplayName { get; set; }

        public ObservationTypeSimple(ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            ObservationTypeName = $"{observationType.ObservationTypeName}";
            BenchmarkUnitLegendDisplayName = observationType.BenchmarkMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            ThresholdUnitLegendDisplayName = observationType.ThresholdMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
        }
    }
}