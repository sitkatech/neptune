//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentObservationTypeExtensionMethods
    {
        public static TreatmentBMPAssessmentObservationTypeDto AsDto(this TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPAssessmentObservationTypeDto = new TreatmentBMPAssessmentObservationTypeDto()
            {
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
                TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName,
                ObservationTypeSpecification = treatmentBMPAssessmentObservationType.ObservationTypeSpecification.AsDto(),
                TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema
            };
            DoCustomMappings(treatmentBMPAssessmentObservationType, treatmentBMPAssessmentObservationTypeDto);
            return treatmentBMPAssessmentObservationTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, TreatmentBMPAssessmentObservationTypeDto treatmentBMPAssessmentObservationTypeDto);

        public static TreatmentBMPAssessmentObservationTypeSimpleDto AsSimpleDto(this TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPAssessmentObservationTypeSimpleDto = new TreatmentBMPAssessmentObservationTypeSimpleDto()
            {
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
                TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName,
                ObservationTypeSpecificationID = treatmentBMPAssessmentObservationType.ObservationTypeSpecificationID,
                TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema
            };
            DoCustomSimpleDtoMappings(treatmentBMPAssessmentObservationType, treatmentBMPAssessmentObservationTypeSimpleDto);
            return treatmentBMPAssessmentObservationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, TreatmentBMPAssessmentObservationTypeSimpleDto treatmentBMPAssessmentObservationTypeSimpleDto);
    }
}