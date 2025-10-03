namespace Neptune.Models.DataTransferObjects
{
    public class FundingEventFundingSourceSimpleDto
    {
        public int FundingSourceID { get; set; }
        public decimal? Amount { get; set; }
        public string? FundingSourceName { get; set; } // Added for display
    }
}
