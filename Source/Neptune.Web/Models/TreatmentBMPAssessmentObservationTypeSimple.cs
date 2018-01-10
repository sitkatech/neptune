using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypeSimple
    {
        public int ObservationTypeID { get; }
        public bool HasBenchmarkAndThresholds { get; }
        public string DisplayName { get; }
        public double? ThresholdValueInObservedUnits { get; }
        public double? BenchmarkValue { get; }
        public String Weight { get; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public TreatmentBMPAssessmentObservationTypeSimple(ObservationType observationType, TreatmentBMPObservation treatmentBMPObservation)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
           
            var unitDisplayName = observationType.MeasurementUnitType?.LegendDisplayName ?? string.Empty;
            DisplayName = $"{observationType.ObservationTypeName} ({unitDisplayName})";

            var benchmarkValue = observationType.GetBenchmarkValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = observationType.GetThresholdValue(treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP);
            var assessmentScoreWeight = observationType.TreatmentBMPTypeObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPObservation.TreatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeID)?.AssessmentScoreWeight;

            ThresholdValueInObservedUnits = observationType.ThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, observationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease) ?? 0;
            BenchmarkValue = benchmarkValue ?? 0;            
            Weight = assessmentScoreWeight?.ToString("0.0") ?? "pass/fail";

            TreatmentBMPObservationSimple =  new TreatmentBMPObservationSimple(treatmentBMPObservation);
        }
    }
}