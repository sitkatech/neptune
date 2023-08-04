//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentObservationExtensionMethods
    {
        public static OnlandVisualTrashAssessmentObservationDto AsDto(this OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            var onlandVisualTrashAssessmentObservationDto = new OnlandVisualTrashAssessmentObservationDto()
            {
                OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessment = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessment.AsDto(),
                Note = onlandVisualTrashAssessmentObservation.Note,
                ObservationDatetime = onlandVisualTrashAssessmentObservation.ObservationDatetime
            };
            DoCustomMappings(onlandVisualTrashAssessmentObservation, onlandVisualTrashAssessmentObservationDto);
            return onlandVisualTrashAssessmentObservationDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation, OnlandVisualTrashAssessmentObservationDto onlandVisualTrashAssessmentObservationDto);

        public static OnlandVisualTrashAssessmentObservationSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            var onlandVisualTrashAssessmentObservationSimpleDto = new OnlandVisualTrashAssessmentObservationSimpleDto()
            {
                OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID,
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID,
                Note = onlandVisualTrashAssessmentObservation.Note,
                ObservationDatetime = onlandVisualTrashAssessmentObservation.ObservationDatetime
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentObservation, onlandVisualTrashAssessmentObservationSimpleDto);
            return onlandVisualTrashAssessmentObservationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation, OnlandVisualTrashAssessmentObservationSimpleDto onlandVisualTrashAssessmentObservationSimpleDto);
    }
}