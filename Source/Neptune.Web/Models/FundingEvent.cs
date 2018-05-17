namespace Neptune.Web.Models
{
    public partial class FundingEvent : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var treatmentBMP = TreatmentBMP?.TreatmentBMPName ?? "Unknown";
                var fundingSource = FundingSource?.FundingSourceName ?? "Unknown";
                return $"TreatmentBMP: {treatmentBMP}, FundingSource: {fundingSource}";
            }
        }
    }
}