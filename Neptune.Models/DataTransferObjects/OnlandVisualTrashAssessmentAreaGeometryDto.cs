namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentAreaGeometryDto
{
    public int OnlandVisualTrashAssessmentAreaID { get; set; }
    public List<int> ParcelIDs { get; set; }
    public string Geometry { get; set; }
    public bool UsingParcels { get; set; }
}