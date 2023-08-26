using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class FundingEventFundingSourceModelExtensions
    {
        public static HtmlString GetAmountIfSpecified(this FundingEventFundingSource fundingEventFundingSource)
        {
            if (fundingEventFundingSource.Amount != null)
            {
                return new HtmlString(fundingEventFundingSource.Amount.ToStringCurrency());
            }

            return new HtmlString("<span style=\"font-style:italic; font-weight:normal;\">Not specified</span>");
        }
    }
}