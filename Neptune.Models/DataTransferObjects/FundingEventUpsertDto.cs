namespace Neptune.Models.DataTransferObjects;

public class FundingEventUpsertDto
{
    public int FundingEventTypeID { get; set; }
    public int Year { get; set; }
    public string? Description { get; set; }
    public List<FundingEventFundingSourceSimpleDto> FundingEventFundingSources { get; set; } = new();
}