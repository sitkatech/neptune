using System.Linq;
using LtInfo.Common;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Models
{
    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public string DisplayName { get; set; }
        public double? ThresholdValueInObservedUnits { get; set; }
        public double? BenchmarkValue { get; set; }
        public double Weight { get; set; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public ObservationTypeSimple(ObservationType observationType, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            DisplayName = $"{observationType.ObservationTypeName} {observationType.MeasurementUnitType.LegendDisplayName.EncloseInParaentheses()}";            
            ThresholdValueInObservedUnits = treatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(observationType);
            BenchmarkValue = treatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(observationType);
            Weight = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(observationType).AssessmentScoreWeight;

            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType == observationType);
            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation) : null;
        }
    }
}