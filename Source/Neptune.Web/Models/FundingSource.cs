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
        public string EditUrl
        {
            get { return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(t => t.Edit(FundingSourceID)); }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.DeleteFundingSource(FundingSourceID)); }
        }

        public HtmlString DisplayNameAsUrl => UrlTemplate.MakeHrefString(DetailUrl, DisplayName);

        public string DisplayName => $"{FundingSourceName} ({Organization.OrganizationShortNameIfAvailable}){(!IsActive ? " (Inactive)" : string.Empty)}";    

        public string DetailUrl
        {
            get { return SitkaRoute<FundingSourceController>.BuildUrlFromExpression(x => x.Detail(FundingSourceID)); }
        }

        public static bool IsFundingSourceNameUnique(IEnumerable<FundingSource> fundingSources, string fundingSourceName, int currentFundingSourceID)
        {
            var fundingSource =
                fundingSources.SingleOrDefault(x => x.FundingSourceID != currentFundingSourceID && String.Equals(x.FundingSourceName, fundingSourceName, StringComparison.InvariantCultureIgnoreCase));
            return fundingSource == null;
        }
       

        public string AuditDescriptionString => FundingSourceName;

        public List<TreatmentBMP> GetAssociatedTreatmentBMPs(Person person)
        {
            return FundingEvents.Select(x => x.TreatmentBMP).ToList();
        }
    }
}