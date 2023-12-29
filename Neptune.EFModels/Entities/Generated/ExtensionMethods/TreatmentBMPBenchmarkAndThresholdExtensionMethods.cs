//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPBenchmarkAndThresholdExtensionMethods
    {
        public static TreatmentBMPBenchmarkAndThresholdSimpleDto AsSimpleDto(this TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold)
        {
            var dto = new TreatmentBMPBenchmarkAndThresholdSimpleDto()
            {
                TreatmentBMPBenchmarkAndThresholdID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPBenchmarkAndThresholdID,
                TreatmentBMPID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPID,
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPAssessmentObservationTypeID,
                BenchmarkValue = treatmentBMPBenchmarkAndThreshold.BenchmarkValue,
                ThresholdValue = treatmentBMPBenchmarkAndThreshold.ThresholdValue
            };
            return dto;
        }
    }
}