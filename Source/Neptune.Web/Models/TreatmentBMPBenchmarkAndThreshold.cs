using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPBenchmarkAndThreshold : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var TreatmentBMPAssessmentObservationType = HttpRequestStorage.DatabaseEntities.ObservationTypes.GetObservationType(ObservationTypeID);
                var treatmentBMP = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(TreatmentBMPID);

                return $"Treatment BMP: {treatmentBMP.TreatmentBMPName}, Observation Type: {TreatmentBMPAssessmentObservationType.ObservationTypeName}, Benchmark Value: {BenchmarkValue}, Threshold Value: {ThresholdValue}";
            }
        }
    }
}