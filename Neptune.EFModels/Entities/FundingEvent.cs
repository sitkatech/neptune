using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
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