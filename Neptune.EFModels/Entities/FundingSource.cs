using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.FundingEventFundingSources.Where(x => x.FundingSourceID == FundingSourceID).ExecuteDeleteAsync();
            await dbContext.FundingSources.Where(x => x.FundingSourceID == FundingSourceID).ExecuteDeleteAsync();
        }
    }
}