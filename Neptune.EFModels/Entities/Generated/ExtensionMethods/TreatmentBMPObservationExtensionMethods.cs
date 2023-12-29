//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPObservationExtensionMethods
    {
        public static TreatmentBMPObservationSimpleDto AsSimpleDto(this TreatmentBMPObservation treatmentBMPObservation)
        {
            var dto = new TreatmentBMPObservationSimpleDto()
            {
                TreatmentBMPObservationID = treatmentBMPObservation.TreatmentBMPObservationID,
                TreatmentBMPAssessmentID = treatmentBMPObservation.TreatmentBMPAssessmentID,
                TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPObservation.TreatmentBMPTypeAssessmentObservationTypeID,
                TreatmentBMPTypeID = treatmentBMPObservation.TreatmentBMPTypeID,
                TreatmentBMPAssessmentObservationTypeID = treatmentBMPObservation.TreatmentBMPAssessmentObservationTypeID,
                ObservationData = treatmentBMPObservation.ObservationData
            };
            return dto;
        }
    }
}