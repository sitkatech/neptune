//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentObservationPhotoStagingExtensionMethods
    {
        public static OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStaging)
        {
            var dto = new OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto()
            {
                OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                FileResourceID = onlandVisualTrashAssessmentObservationPhotoStaging.FileResourceID,
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessmentID
            };
            return dto;
        }
    }
}