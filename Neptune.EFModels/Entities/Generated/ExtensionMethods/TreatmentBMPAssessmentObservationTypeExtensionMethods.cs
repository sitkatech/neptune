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
            var dto = new TreatmentBMPAssessmentObservationTypeSimpleDto()
            {
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID,
                TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName,
                ObservationTypeSpecificationID = treatmentBMPAssessmentObservationType.ObservationTypeSpecificationID,
                TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema
            };
            return dto;
        }
    }
}