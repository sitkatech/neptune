namespace Neptune.EFModels.Entities
{
    public partial class FundingEvent
    {
        public void DeleteFull(NeptuneDbContext dbContext)
        {
            // todo:
            throw new NotImplementedException();
        }

        public string GetDisplayName()
        {
            return $"{Year} {FundingEventType.FundingEventTypeDisplayName}";
        }
    }
}