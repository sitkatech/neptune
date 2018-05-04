namespace Neptune.Web.Models
{
    public partial class TreatmentBMPFundingSource : IAuditableEntity
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