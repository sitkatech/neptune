namespace Neptune.Web.Models
{
    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public bool TargetIsSweetSpot { get; set; }
        public string ObservationTypeName { get; set; }   
        public string BenchmarkUnitLegendDisplayName { get; set; }
        public string ThresholdUnitLegendDisplayName { get; set; }
        public string CollectionMethodDisplayName { get; set; }

        public ObservationTypeSimple(ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            TargetIsSweetSpot = observationType.TargetIsSweetSpot;
            ObservationTypeName = $"{observationType.ObservationTypeName}";
            BenchmarkUnitLegendDisplayName = observationType.BenchmarkMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            ThresholdUnitLegendDisplayName = observationType.ThresholdMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            CollectionMethodDisplayName = observationType.ObservationTypeSpecification.ObservationTypeCollectionMethod
                .ObservationTypeCollectionMethodDisplayName;
        }
    }
}