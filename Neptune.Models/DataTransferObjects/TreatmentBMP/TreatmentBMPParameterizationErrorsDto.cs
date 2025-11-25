namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPParameterizationErrorsDto
{
    public bool HasDelineation { get; set; }
    public bool LinkToDelineationMap { get; set; }
    public WaterQualityManagementPlanDisplayDto? SimplifiedWQMP { get; set; }
    public bool MissingModelAttributes { get; set; }
}

public class TreatmentBMPUpstreamestErrorsDto
{
    public TreatmentBMPDisplayDto? UpstreamestBMP { get; set; }
    public bool IsUpstreamestBMPAnalyzedInModelingModule { get; set; }
}