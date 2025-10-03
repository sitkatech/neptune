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
                TreatmentBMPID = entity.TreatmentBMPID,
                TreatmentBMPTypeAssessmentObservationTypeID = entity.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = entity.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = entity.TreatmentBMPAssessmentObservationTypeID,
                BenchmarkValue = entity.BenchmarkValue,
                ThresholdValue = entity.ThresholdValue
            };
        }

        public static void UpdateFromUpsertDto(this TreatmentBMPBenchmarkAndThreshold entity, TreatmentBMPBenchmarkAndThresholdUpsertDto dto)
        {
            entity.TreatmentBMPTypeAssessmentObservationTypeID = dto.TreatmentBMPTypeAssessmentObservationTypeID;
            entity.TreatmentBMPTypeID = dto.TreatmentBMPTypeID;
            entity.TreatmentBMPAssessmentObservationTypeID = dto.TreatmentBMPAssessmentObservationTypeID;
            entity.BenchmarkValue = dto.BenchmarkValue;
            entity.ThresholdValue = dto.ThresholdValue;
        }

        public static TreatmentBMPBenchmarkAndThreshold AsEntity(this TreatmentBMPBenchmarkAndThresholdUpsertDto dto, int treatmentBMPID)
        {
            return new TreatmentBMPBenchmarkAndThreshold
            {
                TreatmentBMPID = treatmentBMPID,
                TreatmentBMPTypeAssessmentObservationTypeID = dto.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = dto.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = dto.TreatmentBMPAssessmentObservationTypeID,
                BenchmarkValue = dto.BenchmarkValue,
                ThresholdValue = dto.ThresholdValue
            };
        }
    }
}
