namespace Neptune.Models.DataTransferObjects;

public class PreliminarySourceIdentificationTypeWorkflowDto
{
    public int PreliminarySourceIdentificationTypeID { get; set; }
    public string PreliminarySourceIdentificationTypeDisplayName { get; set; }
    public int PreliminarySourceIdentificationCategoryTypeID { get; set; }
    public bool IsInOnlandAssessmentArea { get; set; }
}