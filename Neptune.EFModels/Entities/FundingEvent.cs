using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class FundingEvent
    {
        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.FundingEventFundingSources.Where(x => x.FundingEventID == FundingEventID).ExecuteDeleteAsync();
            await dbContext.FundingEvents.Where(x => x.FundingEventID == FundingEventID).ExecuteDeleteAsync();
        }

        public string GetDisplayName()
        {
            return $"{Year} {FundingEventType.FundingEventTypeDisplayName}";
        }
    }
}