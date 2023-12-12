//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentObservationExtensionMethods
    {

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