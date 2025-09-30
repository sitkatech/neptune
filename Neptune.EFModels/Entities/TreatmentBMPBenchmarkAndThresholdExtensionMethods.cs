using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPBenchmarkAndThresholdExtensionMethods
    {
        public static TreatmentBMPBenchmarkAndThresholdDto AsDto(this TreatmentBMPBenchmarkAndThreshold entity)
        {
            return new TreatmentBMPBenchmarkAndThresholdDto
            {
                TreatmentBMPBenchmarkAndThresholdID = entity.TreatmentBMPBenchmarkAndThresholdID,
                Name = null
            };
        }
    }
}
