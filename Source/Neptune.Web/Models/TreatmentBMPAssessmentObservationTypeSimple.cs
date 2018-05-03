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

        public TreatmentBMPAssessmentObservationTypeSimple(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType,
            TreatmentBMPAssessment treatmentBMPAssessment, bool overrideAssessmentScoreIfFailing)
        {
            ObservationTypeID = TreatmentBMPAssessmentObservationType.ObservationTypeID;
            HasBenchmarkAndThresholds = TreatmentBMPAssessmentObservationType.HasBenchmarkAndThreshold;
           
            var unitDisplayName = TreatmentBMPAssessmentObservationType.MeasurementUnitType != null ? $" ({TreatmentBMPAssessmentObservationType.MeasurementUnitType.LegendDisplayName})" : string.Empty;
            DisplayName = $"{TreatmentBMPAssessmentObservationType.ObservationTypeName}{unitDisplayName}";

            var benchmarkValue = TreatmentBMPAssessmentObservationType.GetBenchmarkValue(treatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = TreatmentBMPAssessmentObservationType.GetThresholdValue(treatmentBMPAssessment.TreatmentBMP);
            var assessmentScoreWeight = TreatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeID)?.AssessmentScoreWeight;

            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.ObservationTypeID == TreatmentBMPAssessmentObservationType.ObservationTypeID);
            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation, overrideAssessmentScoreIfFailing) : null;

            var useUpperValue = TreatmentBMPAssessmentObservationType.UseUpperValueForThreshold(benchmarkValue, TreatmentBMPObservationSimple?.ObservationValue);
            ThresholdValueInObservedUnits = TreatmentBMPAssessmentObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue) ?? 0;
            BenchmarkValue = benchmarkValue ?? 0;
            Weight = assessmentScoreWeight?.ToStringShortPercent() ?? "pass/fail";
        }
    }
}