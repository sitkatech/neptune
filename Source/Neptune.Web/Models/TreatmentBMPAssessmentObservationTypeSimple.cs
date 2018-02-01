using System.Linq;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypeSimple
    {
        public int ObservationTypeID { get; }
        public bool HasBenchmarkAndThresholds { get; }
        public string DisplayName { get; }
        public double? ThresholdValueInObservedUnits { get; }
        public double? BenchmarkValue { get; }
        public string Weight { get; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public TreatmentBMPAssessmentObservationTypeSimple(ObservationType observationType,
            TreatmentBMPAssessment treatmentBMPAssessment, bool overrideAssessmentScoreIfFailing)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
           
            var unitDisplayName = observationType.MeasurementUnitType != null ? $" ({observationType.MeasurementUnitType.LegendDisplayName})" : string.Empty;
            DisplayName = $"{observationType.ObservationTypeName}{unitDisplayName}";

            var benchmarkValue = observationType.GetBenchmarkValue(treatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = observationType.GetThresholdValue(treatmentBMPAssessment.TreatmentBMP);
            var assessmentScoreWeight = observationType.TreatmentBMPTypeObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeID)?.AssessmentScoreWeight;

            ThresholdValueInObservedUnits = observationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, observationType.ThresholdMeasurementUnitType() == MeasurementUnitType.PercentIncrease) ?? 0;
            BenchmarkValue = benchmarkValue ?? 0;            
            Weight = assessmentScoreWeight?.ToStringShortPercent() ?? "pass/fail";

            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.ObservationTypeID == observationType.ObservationTypeID);
            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation, overrideAssessmentScoreIfFailing) : null;
        }
    }
}