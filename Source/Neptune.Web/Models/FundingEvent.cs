using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class FundingEvent : IAuditableEntity
    {
        public string DisplayName => $"{Year} {FundingEventType.FundingEventTypeDisplayName}";

        public string AuditDescriptionString => DisplayName;
    }

    public static class FundingEventModelExtensions
    {
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<FundingEventController>.BuildUrlFromExpression(
                t => t.EditFundingEvent(UrlTemplate.Parameter1Int)));

        public static string GetEditUrl(this FundingEvent fundingEvent)
        {
            return EditUrlTemplate.ParameterReplace(fundingEvent.FundingEventID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<FundingEventController>.BuildUrlFromExpression(t =>
                t.DeleteFundingEvent(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this FundingEvent fundingEvent)
        {
            return DeleteUrlTemplate.ParameterReplace(fundingEvent.FundingEventID);
        }
    }
}