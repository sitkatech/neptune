namespace Neptune.Models.DataTransferObjects;

public class PassFailObservationTypeSchema
{
    public List<string> PropertiesToObserve { get; set; }
    public string AssessmentDescription { get; set; }
    public string PassingScoreLabel { get; set; }
    public string FailingScoreLabel { get; set; }
}