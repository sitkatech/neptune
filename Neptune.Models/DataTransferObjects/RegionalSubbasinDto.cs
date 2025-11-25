namespace Neptune.Models.DataTransferObjects;

public class RegionalSubbasinDto
{
    public int RegionalSubbasinID { get; set; }
    public int? OCSurveyCatchmentID { get; set; }
    public int? OCSurveyDownstreamCatchmentID { get; set; }
    public string? Watershed { get; set; }
    public string? DrainID { get; set; }
    public string? DisplayName { get; set; }
    public int? DownstreamRegionalSubbasinID { get; set; }
    public double? Area { get; set; }
}

public class RegionalSubbasinSimpleDto
{
    public int RegionalSubbasinID { get; set; }
    public string? DisplayName { get; set; }
}

public class RegionalSubbasinUpsertDto
{
    public int OCSurveyCatchmentID { get; set; }
    public int? OCSurveyDownstreamCatchmentID { get; set; }
    public string? Watershed { get; set; }
    public string? DrainID { get; set; }
}
