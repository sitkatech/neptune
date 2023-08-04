//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPTypeAssessmentObservationTypeExtensionMethods
    {
        public static TreatmentBMPTypeAssessmentObservationTypeDto AsDto(this TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType)
        {
            var treatmentBMPTypeAssessmentObservationTypeDto = new TreatmentBMPTypeAssessmentObservationTypeDto()
            {
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPType = treatmentBMPTypeAssessmentObservationType.TreatmentBMPType.AsDto(),
                TreatmentBMPAssessmentObservationType = treatmentBMPTypeAssessmentObservationType.TreatmentBMPAssessmentObservationType.AsDto(),
                AssessmentScoreWeight = treatmentBMPTypeAssessmentObservationType.AssessmentScoreWeight,
                DefaultThresholdValue = treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue,
                DefaultBenchmarkValue = treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue,
                OverrideAssessmentScoreIfFailing = treatmentBMPTypeAssessmentObservationType.OverrideAssessmentScoreIfFailing,
                SortOrder = treatmentBMPTypeAssessmentObservationType.SortOrder
            };
            DoCustomMappings(treatmentBMPTypeAssessmentObservationType, treatmentBMPTypeAssessmentObservationTypeDto);
            return treatmentBMPTypeAssessmentObservationTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPTypeAssessmentObservationTypeDto treatmentBMPTypeAssessmentObservationTypeDto);

        public static TreatmentBMPTypeAssessmentObservationTypeSimpleDto AsSimpleDto(this TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType)
        {
            var treatmentBMPTypeAssessmentObservationTypeSimpleDto = new TreatmentBMPTypeAssessmentObservationTypeSimpleDto()
            {
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
                AssessmentScoreWeight = treatmentBMPTypeAssessmentObservationType.AssessmentScoreWeight,
                DefaultThresholdValue = treatmentBMPTypeAssessmentObservationType.DefaultThresholdValue,
                DefaultBenchmarkValue = treatmentBMPTypeAssessmentObservationType.DefaultBenchmarkValue,
                OverrideAssessmentScoreIfFailing = treatmentBMPTypeAssessmentObservationType.OverrideAssessmentScoreIfFailing,
                SortOrder = treatmentBMPTypeAssessmentObservationType.SortOrder
            };
            DoCustomSimpleDtoMappings(treatmentBMPTypeAssessmentObservationType, treatmentBMPTypeAssessmentObservationTypeSimpleDto);
            return treatmentBMPTypeAssessmentObservationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPTypeAssessmentObservationTypeSimpleDto treatmentBMPTypeAssessmentObservationTypeSimpleDto);
    }
}