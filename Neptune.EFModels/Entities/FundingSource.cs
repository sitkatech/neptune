namespace Neptune.EFModels.Entities
{
    public partial class FundingSource
    {
        public string GetDisplayName()
        {
            return
                $"{FundingSourceName} ({Organization.GetOrganizationShortNameIfAvailable()}){(!IsActive ? " (Inactive)" : string.Empty)}";
        }

        public List<TreatmentBMP> GetAssociatedTreatmentBMPs()
        {
            return FundingEventFundingSources.Select(x => x.FundingEvent.TreatmentBMP).Distinct().ToList();
        }
    }
}