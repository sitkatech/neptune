using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPBenchmarkAndThreshold : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var treatmentBMPAssessmentObservationType = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.GetTreatmentBMPAssessmentObservationType(TreatmentBMPAssessmentObservationTypeID);
                var treatmentBMP = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(TreatmentBMPID);

                return $"Treatment BMP: {treatmentBMP.TreatmentBMPName}, Observation Type: {treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}, Benchmark Value: {BenchmarkValue}, Threshold Value: {ThresholdValue}";
            }
        }
    }
}