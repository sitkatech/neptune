namespace Neptune.Web.Models
{
    public partial class FundingEvent : IAuditableEntity
    {
        public string GetDisplayName()
        {
            return $"{Year} {FundingEventType.FundingEventTypeDisplayName}";
        }

        public string GetAuditDescriptionString()
        {
            return GetDisplayName();
        }
    }
}