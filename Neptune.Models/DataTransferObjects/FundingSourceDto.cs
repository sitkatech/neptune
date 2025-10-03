namespace Neptune.Models.DataTransferObjects
{
    public class FundingSourceDto
    {
        public int FundingSourceID { get; set; }
        public int OrganizationID { get; set; }
        public string? FundingSourceName { get; set; }
        public string? OrganizationName { get; set; }
        public bool IsActive { get; set; }
        public string? FundingSourceDescription { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
