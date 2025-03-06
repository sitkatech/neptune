namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationWithPhotoDto
{
    public int OnlandVisualTrashAssessmentObservationID { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public string Note { get; set; }
    public DateTime? ObservationDatetime { get; set; }
    public string FileResourceGUID { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}