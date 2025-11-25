namespace Neptune.Models.DataTransferObjects
{
    public class FundingEventDto
    {
        public int FundingEventID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FundingEventTypeID { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public List<FundingEventFundingSourceSimpleDto> FundingEventFundingSources { get; set; } = new();
        public string DisplayName { get; set; } = string.Empty; // Added for display
    }
}
