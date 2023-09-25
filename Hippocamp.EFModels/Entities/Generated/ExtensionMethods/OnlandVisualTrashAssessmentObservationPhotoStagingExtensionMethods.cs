//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentObservationPhotoStagingExtensionMethods
    {
        public static OnlandVisualTrashAssessmentObservationPhotoStagingDto AsDto(this OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging)
        {
            var onlandVisualTrashAssessmentObservationPhotoStagingDto = new OnlandVisualTrashAssessmentObservationPhotoStagingDto()
            {
                OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                FileResource = onlandVisualTrashAssessmentObservationPhotoStaging.FileResource.AsDto(),
                OnlandVisualTrashAssessment = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessment.AsDto()
            };
            DoCustomMappings(onlandVisualTrashAssessmentObservationPhotoStaging, onlandVisualTrashAssessmentObservationPhotoStagingDto);
            return onlandVisualTrashAssessmentObservationPhotoStagingDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging, OnlandVisualTrashAssessmentObservationPhotoStagingDto onlandVisualTrashAssessmentObservationPhotoStagingDto);

        public static OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging)
        {
            var onlandVisualTrashAssessmentObservationPhotoStagingSimpleDto = new OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto()
            {
                OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                FileResourceID = onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID,
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentID
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentObservationPhotoStaging, onlandVisualTrashAssessmentObservationPhotoStagingSimpleDto);
            return onlandVisualTrashAssessmentObservationPhotoStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging, OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto onlandVisualTrashAssessmentObservationPhotoStagingSimpleDto);
    }
}