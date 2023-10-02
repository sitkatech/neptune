namespace Neptune.EFModels.Entities
{
    public partial class FundingEvent
    {
        public void DeleteFull(NeptuneDbContext dbContext)
        {
            // todo: deletefull
            throw new NotImplementedException("Deleting of Funding Event not implemented yet!");
        }

        public string GetDisplayName()
        {
            return $"{Year} {FundingEventType.FundingEventTypeDisplayName}";
        }
    }
}