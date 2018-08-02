namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeAssessmentObservationTypeSimple
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public bool TargetIsSweetSpot { get; set; }
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }   
        public int? TreatmentBMPAssessmentObservationTypeSortOrder { get; set; }   
        public string BenchmarkUnitLegendDisplayName { get; set; }
        public string ThresholdUnitLegendDisplayName { get; set; }
        public string CollectionMethodDisplayName { get; set; }

        public TreatmentBMPTypeAssessmentObservationTypeSimple(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            HasBenchmarkAndThresholds = treatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold();
            TargetIsSweetSpot = treatmentBMPAssessmentObservationType.GetTargetIsSweetSpot();
            TreatmentBMPAssessmentObservationTypeName = $"{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}";
            BenchmarkUnitLegendDisplayName = treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            ThresholdUnitLegendDisplayName = treatmentBMPAssessmentObservationType.ThresholdMeasurementUnitType()?.LegendDisplayName ?? string.Empty;
            CollectionMethodDisplayName = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.ObservationTypeCollectionMethod
                .ObservationTypeCollectionMethodDisplayName;
        }
    }
}