namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentAddRemoveParcelsDto
{
    public int OnlandVisualTrashAssessmentID { get; set; }
    public int? OnlandVisualTrashAssessmentAreaID { get; set; }
    public int StormwaterJurisdictionID { get; set; }
    public bool IsDraftGeometryManuallyRefined { get; set; }
    public List<int> SelectedParcelIDs { get; set; }
}