namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationUpsertDto
{
    public int? OnlandVisualTrashAssessmentObservationID { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public string Note { get; set; }
    public DateTime? ObservationDatetime { get; set; }
    public int? FileResourceID { get; set; }
    public string? FileResourceGUID { get; set; }
    public int? PhotoStagingID { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}