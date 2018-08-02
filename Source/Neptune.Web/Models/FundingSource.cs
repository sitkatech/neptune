using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class FundingSource : IAuditableEntity
    {
        public string GetEditUrl()
        {
            return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(t => t.Edit(FundingSourceID));
        }

        public string GetDeleteUrl()
        {
            return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.DeleteFundingSource(FundingSourceID));
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), GetDisplayName());
        }

        public string GetDisplayName()
        {
            return
                $"{FundingSourceName} ({Organization.GetOrganizationShortNameIfAvailable()}){(!IsActive ? " (Inactive)" : string.Empty)}";
        }

        public string GetDetailUrl()
        {
            return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(x => x.Detail(FundingSourceID));
        }

        public static bool IsFundingSourceNameUnique(IEnumerable<FundingSource> fundingSources, string fundingSourceName, int currentFundingSourceID)
        {
            var fundingSource =
                fundingSources.SingleOrDefault(x => x.FundingSourceID != currentFundingSourceID && String.Equals(x.FundingSourceName, fundingSourceName, StringComparison.InvariantCultureIgnoreCase));
            return fundingSource == null;
        }


        public string GetAuditDescriptionString()
        {
            return FundingSourceName;
        }

        public List<TreatmentBMP> GetAssociatedTreatmentBMPs(Person person)
        {
            return FundingEventFundingSources.Select(x => x.FundingEvent.TreatmentBMP).Distinct().ToList();
        }
    }
}