namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypeSimple
    {
        public int ObservationTypeID { get; }
        public bool HasBenchmarkAndThresholds { get; }
        public string DisplayName { get; }
        public double? ThresholdValueInObservedUnits { get; }
        public double? BenchmarkValue { get; }
        public double Weight { get; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public TreatmentBMPAssessmentObservationTypeSimple(ObservationType observationType, TreatmentBMPObservation treatmentBMPObservation)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
           
            var unitDisplayName = observationType.MeasurementUnitType?.LegendDisplayName ?? string.Empty;
            DisplayName = $"{observationType.ObservationTypeName} ({unitDisplayName})";
                        
            ThresholdValueInObservedUnits = 0;
            BenchmarkValue = 0;
            Weight = 0;

            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation) : null;
        }
    }
}