namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationLocationDto
{
    public int? OnlandVisualTrashAssessmentObservationID { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}