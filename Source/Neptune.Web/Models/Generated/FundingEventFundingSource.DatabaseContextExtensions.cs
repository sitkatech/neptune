//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
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

        // Delete using an IDList (Firma style)
        public static void DeleteFundingEventFundingSource(this IQueryable<FundingEventFundingSource> fundingEventFundingSources, List<int> fundingEventFundingSourceIDList)
        {
            if(fundingEventFundingSourceIDList.Any())
            {
                fundingEventFundingSources.Where(x => fundingEventFundingSourceIDList.Contains(x.FundingEventFundingSourceID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteFundingEventFundingSource(this IQueryable<FundingEventFundingSource> fundingEventFundingSources, ICollection<FundingEventFundingSource> fundingEventFundingSourcesToDelete)
        {
            if(fundingEventFundingSourcesToDelete.Any())
            {
                var fundingEventFundingSourceIDList = fundingEventFundingSourcesToDelete.Select(x => x.FundingEventFundingSourceID).ToList();
                fundingEventFundingSources.Where(x => fundingEventFundingSourceIDList.Contains(x.FundingEventFundingSourceID)).Delete();
            }
        }

        public static void DeleteFundingEventFundingSource(this IQueryable<FundingEventFundingSource> fundingEventFundingSources, int fundingEventFundingSourceID)
        {
            DeleteFundingEventFundingSource(fundingEventFundingSources, new List<int> { fundingEventFundingSourceID });
        }

        public static void DeleteFundingEventFundingSource(this IQueryable<FundingEventFundingSource> fundingEventFundingSources, FundingEventFundingSource fundingEventFundingSourceToDelete)
        {
            DeleteFundingEventFundingSource(fundingEventFundingSources, new List<FundingEventFundingSource> { fundingEventFundingSourceToDelete });
        }
    }
}