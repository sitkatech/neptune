//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPBenchmarkAndThresholdExtensionMethods
    {
        public static TreatmentBMPBenchmarkAndThresholdDto AsDto(this TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold)
        {
            var treatmentBMPBenchmarkAndThresholdDto = new TreatmentBMPBenchmarkAndThresholdDto()
            {
                TreatmentBMPBenchmarkAndThresholdID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPBenchmarkAndThresholdID,
                TreatmentBMP = treatmentBMPBenchmarkAndThreshold.TreatmentBMP.AsDto(),
                TreatmentBMPTypeAssessmentObservationType = treatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeAssessmentObservationType.AsDto(),
                TreatmentBMPType = treatmentBMPBenchmarkAndThreshold.TreatmentBMPType.AsDto(),
                TreatmentBMPAssessmentObservationType = treatmentBMPBenchmarkAndThreshold.TreatmentBMPAssessmentObservationType.AsDto(),
                BenchmarkValue = treatmentBMPBenchmarkAndThreshold.BenchmarkValue,
                ThresholdValue = treatmentBMPBenchmarkAndThreshold.ThresholdValue
            };
            DoCustomMappings(treatmentBMPBenchmarkAndThreshold, treatmentBMPBenchmarkAndThresholdDto);
            return treatmentBMPBenchmarkAndThresholdDto;
        }

        static partial void DoCustomMappings(TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold, TreatmentBMPBenchmarkAndThresholdDto treatmentBMPBenchmarkAndThresholdDto);

        public static TreatmentBMPBenchmarkAndThresholdSimpleDto AsSimpleDto(this TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold)
        {
            var treatmentBMPBenchmarkAndThresholdSimpleDto = new TreatmentBMPBenchmarkAndThresholdSimpleDto()
            {
                TreatmentBMPBenchmarkAndThresholdID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPBenchmarkAndThresholdID,
                TreatmentBMPID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPID,
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPBenchmarkAndThreshold.TreatmentBMPAssessmentObservationTypeID,
                BenchmarkValue = treatmentBMPBenchmarkAndThreshold.BenchmarkValue,
                ThresholdValue = treatmentBMPBenchmarkAndThreshold.ThresholdValue
            };
            DoCustomSimpleDtoMappings(treatmentBMPBenchmarkAndThreshold, treatmentBMPBenchmarkAndThresholdSimpleDto);
            return treatmentBMPBenchmarkAndThresholdSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPBenchmarkAndThreshold treatmentBMPBenchmarkAndThreshold, TreatmentBMPBenchmarkAndThresholdSimpleDto treatmentBMPBenchmarkAndThresholdSimpleDto);
    }
}