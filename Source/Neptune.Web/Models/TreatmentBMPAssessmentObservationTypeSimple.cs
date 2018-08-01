using System.Linq;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypeSimple
    {
        public int TreatmentBMPAssessmentObservationTypeID { get; }
        public bool HasBenchmarkAndThresholds { get; }
        public string DisplayName { get; }
        public double? ThresholdValueInObservedUnits { get; }
        public double? BenchmarkValue { get; }
        public string Weight { get; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public TreatmentBMPAssessmentObservationTypeSimple(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType,
            TreatmentBMPAssessment treatmentBMPAssessment, bool overrideAssessmentScoreIfFailing)
        {
            TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            HasBenchmarkAndThresholds = treatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold();
           
            var unitDisplayName = treatmentBMPAssessmentObservationType.GetMeasurementUnitType() != null ? $" ({treatmentBMPAssessmentObservationType.GetMeasurementUnitType().LegendDisplayName})" : string.Empty;
            DisplayName = $"{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}{unitDisplayName}";

            var benchmarkValue = treatmentBMPAssessmentObservationType.GetBenchmarkValue(treatmentBMPAssessment.TreatmentBMP);
            var thresholdValue = treatmentBMPAssessmentObservationType.GetThresholdValue(treatmentBMPAssessment.TreatmentBMP);
            var assessmentScoreWeight = treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeID)?.AssessmentScoreWeight;

            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation, overrideAssessmentScoreIfFailing) : null;

            var useUpperValue = treatmentBMPAssessmentObservationType.UseUpperValueForThreshold(benchmarkValue, TreatmentBMPObservationSimple?.ObservationValue);
            ThresholdValueInObservedUnits = treatmentBMPAssessmentObservationType.GetThresholdValueInBenchmarkUnits(benchmarkValue, thresholdValue, useUpperValue) ?? 0;
            BenchmarkValue = benchmarkValue ?? 0;
            Weight = assessmentScoreWeight?.ToStringShortPercent() ?? "pass/fail";
        }
    }
}