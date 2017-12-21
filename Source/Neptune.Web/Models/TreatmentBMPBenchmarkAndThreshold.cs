using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPBenchmarkAndThreshold : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var observationType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeObservationTypes.GetTreatmentBMPTypeObservationType(ObservationTypeID);
                var treatmentBMP = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(TreatmentBMPID);

                return $"Treatment BMP: {treatmentBMP.TreatmentBMPName}, Observation Type: {observationType.ObservationType.ObservationTypeDisplayName}, Benchmark Value: {BenchmarkValue}, Threshold Value: {ThresholdValue}";
            }
        }
    }
}