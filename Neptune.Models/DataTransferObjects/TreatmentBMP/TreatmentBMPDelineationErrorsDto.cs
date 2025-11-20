namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPDelineationErrorsDto
{
    public bool HasDiscrepancies { get; set; }
    public List<TreatmentBMPDisplayDto> OverlappingTreatmentBMPs { get; set; } = new();
}