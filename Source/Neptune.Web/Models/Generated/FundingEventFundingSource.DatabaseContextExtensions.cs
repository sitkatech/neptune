//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static FundingEventFundingSource GetFundingEventFundingSource(this IQueryable<FundingEventFundingSource> fundingEventFundingSources, int fundingEventFundingSourceID)
        {
            var fundingEventFundingSource = fundingEventFundingSources.SingleOrDefault(x => x.FundingEventFundingSourceID == fundingEventFundingSourceID);
            Check.RequireNotNullThrowNotFound(fundingEventFundingSource, "FundingEventFundingSource", fundingEventFundingSourceID);
            return fundingEventFundingSource;
        }

        public static void DeleteFundingEventFundingSource(this List<int> fundingEventFundingSourceIDList)
        {
            if(fundingEventFundingSourceIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFundingEventFundingSources.RemoveRange(HttpRequestStorage.DatabaseEntities.FundingEventFundingSources.Where(x => fundingEventFundingSourceIDList.Contains(x.FundingEventFundingSourceID)));
            }
        }

        public static void DeleteFundingEventFundingSource(this ICollection<FundingEventFundingSource> fundingEventFundingSourcesToDelete)
        {
            if(fundingEventFundingSourcesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFundingEventFundingSources.RemoveRange(fundingEventFundingSourcesToDelete);
            }
        }

        public static void DeleteFundingEventFundingSource(this int fundingEventFundingSourceID)
        {
            DeleteFundingEventFundingSource(new List<int> { fundingEventFundingSourceID });
        }

        public static void DeleteFundingEventFundingSource(this FundingEventFundingSource fundingEventFundingSourceToDelete)
        {
            DeleteFundingEventFundingSource(new List<FundingEventFundingSource> { fundingEventFundingSourceToDelete });
        }
    }
}