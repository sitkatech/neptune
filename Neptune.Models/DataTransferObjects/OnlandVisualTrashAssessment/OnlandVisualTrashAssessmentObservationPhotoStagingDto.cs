namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationPhotoStagingDto
{
    public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }
    public int FileResourceID { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public string? FileResourceGUID { get; set; }
    public int? PhotoStagingID { get; set; }
    public int? OnlandVisualTrashAssessmentObservationID { get; set; }

}