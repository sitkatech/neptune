namespace Neptune.Web.Models
{
    public partial class FundingEventFundingSource : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            var treatmentBMP = FundingEvent.TreatmentBMP?.TreatmentBMPName ?? "Unknown";
            var fundingSource = FundingSource?.FundingSourceName ?? "Unknown";
            return $"TreatmentBMP: {treatmentBMP}, FundingSource: {fundingSource}";
        }
    }
}