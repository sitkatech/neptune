namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentRefineAreaDto
{
    public int OnlandVisualTrashAssessmentID { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public BoundingBoxDto BoundingBox { get; set; }
    public string GeometryAsGeoJson { get; set; }
}