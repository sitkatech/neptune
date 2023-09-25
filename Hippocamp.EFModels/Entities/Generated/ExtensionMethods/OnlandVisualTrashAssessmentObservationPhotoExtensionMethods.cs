//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhoto]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentObservationPhotoExtensionMethods
    {
        public static OnlandVisualTrashAssessmentObservationPhotoDto AsDto(this OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto)
        {
            var onlandVisualTrashAssessmentObservationPhotoDto = new OnlandVisualTrashAssessmentObservationPhotoDto()
            {
                OnlandVisualTrashAssessmentObservationPhotoID = onlandVisualTrashAssessmentObservationPhoto.OnlandVisualTrashAssessmentObservationPhotoID,
                FileResource = onlandVisualTrashAssessmentObservationPhoto.FileResource.AsDto(),
                OnlandVisualTrashAssessmentObservation = onlandVisualTrashAssessmentObservationPhoto.OnlandVisualTrashAssessmentObservation.AsDto()
            };
            DoCustomMappings(onlandVisualTrashAssessmentObservationPhoto, onlandVisualTrashAssessmentObservationPhotoDto);
            return onlandVisualTrashAssessmentObservationPhotoDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto, OnlandVisualTrashAssessmentObservationPhotoDto onlandVisualTrashAssessmentObservationPhotoDto);

        public static OnlandVisualTrashAssessmentObservationPhotoSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto)
        {
            var onlandVisualTrashAssessmentObservationPhotoSimpleDto = new OnlandVisualTrashAssessmentObservationPhotoSimpleDto()
            {
                OnlandVisualTrashAssessmentObservationPhotoID = onlandVisualTrashAssessmentObservationPhoto.OnlandVisualTrashAssessmentObservationPhotoID,
                FileResourceID = onlandVisualTrashAssessmentObservationPhoto.FileResourceID,
                OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservationPhoto.OnlandVisualTrashAssessmentObservationID
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessmentObservationPhoto, onlandVisualTrashAssessmentObservationPhotoSimpleDto);
            return onlandVisualTrashAssessmentObservationPhotoSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto, OnlandVisualTrashAssessmentObservationPhotoSimpleDto onlandVisualTrashAssessmentObservationPhotoSimpleDto);
    }
}