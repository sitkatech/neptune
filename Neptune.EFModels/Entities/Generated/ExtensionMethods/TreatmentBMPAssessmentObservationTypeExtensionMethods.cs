//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentObservationTypeExtensionMethods
    {

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