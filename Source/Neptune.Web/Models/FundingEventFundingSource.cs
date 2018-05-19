using System.Web;
using System.Web.Mvc;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public partial class FundingEventFundingSource
    {
        public HtmlString AmountIfSpecified => Amount != null ? new HtmlString(Amount.ToStringCurrency()) : new HtmlString(new TagBuilder("span"){Attributes = {{"style","font-style:italic; font-weight:normal;"}},InnerHtml = "Not specified"}.ToString());
    }
}