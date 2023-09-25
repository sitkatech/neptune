//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPObservationExtensionMethods
    {
        public static TreatmentBMPObservationDto AsDto(this TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPObservationDto = new TreatmentBMPObservationDto()
            {
                TreatmentBMPObservationID = treatmentBMPObservation.TreatmentBMPObservationID,
                TreatmentBMPAssessment = treatmentBMPObservation.TreatmentBMPAssessment.AsDto(),
                TreatmentBMPTypeAssessmentObservationType = treatmentBMPObservation.TreatmentBMPTypeAssessmentObservationType.AsDto(),
                TreatmentBMPType = treatmentBMPObservation.TreatmentBMPType.AsDto(),
                TreatmentBMPAssessmentObservationType = treatmentBMPObservation.TreatmentBMPAssessmentObservationType.AsDto(),
                ObservationData = treatmentBMPObservation.ObservationData
            };
            DoCustomMappings(treatmentBMPObservation, treatmentBMPObservationDto);
            return treatmentBMPObservationDto;
        }

        static partial void DoCustomMappings(TreatmentBMPObservation treatmentBMPObservation, TreatmentBMPObservationDto treatmentBMPObservationDto);

        public static TreatmentBMPObservationSimpleDto AsSimpleDto(this TreatmentBMPObservation treatmentBMPObservation)
        {
            var treatmentBMPObservationSimpleDto = new TreatmentBMPObservationSimpleDto()
            {
                TreatmentBMPObservationID = treatmentBMPObservation.TreatmentBMPObservationID,
                TreatmentBMPAssessmentID = treatmentBMPObservation.TreatmentBMPAssessmentID,
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPObservation.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = treatmentBMPObservation.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPObservation.TreatmentBMPAssessmentObservationTypeID,
                ObservationData = treatmentBMPObservation.ObservationData
            };
            DoCustomSimpleDtoMappings(treatmentBMPObservation, treatmentBMPObservationSimpleDto);
            return treatmentBMPObservationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPObservation treatmentBMPObservation, TreatmentBMPObservationSimpleDto treatmentBMPObservationSimpleDto);
    }
}