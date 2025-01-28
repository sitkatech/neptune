using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class OnlandVisualTrashAssessmentObservationExtensionMethods
{
    public static OnlandVisualTrashAssessmentObservationWithPhotoDto AsPhotoDto(this OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
    {
        var dto = new OnlandVisualTrashAssessmentObservationWithPhotoDto()
        {
            OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID,
            OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentID,
            Note = onlandVisualTrashAssessmentObservation.Note,
            ObservationDatetime = onlandVisualTrashAssessmentObservation.ObservationDatetime,
            FileResourceGUID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationPhotos.SingleOrDefault()?.FileResource.FileResourceGUID.ToString()
        };
        return dto;
    }
}