namespace Neptune.Models.DataTransferObjects;

public class ObservationScoreDto
{
    public string ObservationScore { get; set; }
    public double? ObservationValue { get; set; }
    public bool IsComplete { get; set; }
    public string OverrideScoreText { get; set; }
    public bool OverrideScore { get; set; }
}