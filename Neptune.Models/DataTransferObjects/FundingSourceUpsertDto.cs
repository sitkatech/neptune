namespace Neptune.Models.DataTransferObjects;

public class FundingSourceUpsertDto
{
    public int OrganizationID { get; set; }
    public string? FundingSourceName { get; set; }
    public bool IsActive { get; set; }
    public string? FundingSourceDescription { get; set; }
}
