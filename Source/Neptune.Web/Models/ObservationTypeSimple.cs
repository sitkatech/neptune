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

        public ObservationTypeSimple(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            ObservationTypeID = TreatmentBMPAssessmentObservationType.ObservationTypeID;
            HasBenchmarkAndThresholds = TreatmentBMPAssessmentObservationType.HasBenchmarkAndThreshold;
            TargetIsSweetSpot = TreatmentBMPAssessmentObservationType.TargetIsSweetSpot;
            ObservationTypeName = $"{TreatmentBMPAssessmentObservationType.ObservationTypeName}";
            BenchmarkUnitLegendDisplayName = TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            ThresholdUnitLegendDisplayName = TreatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            CollectionMethodDisplayName = TreatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod
                .ObservationTypeCollectionMethodDisplayName;
        }
    }
}