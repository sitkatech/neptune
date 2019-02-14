//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
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
        public static FundingEvent GetFundingEvent(this IQueryable<FundingEvent> fundingEvents, int fundingEventID)
        {
            var fundingEvent = fundingEvents.SingleOrDefault(x => x.FundingEventID == fundingEventID);
            Check.RequireNotNullThrowNotFound(fundingEvent, "FundingEvent", fundingEventID);
            return fundingEvent;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteFundingEvent(this IQueryable<FundingEvent> fundingEvents, List<int> fundingEventIDList)
        {
            if(fundingEventIDList.Any())
            {
                fundingEvents.Where(x => fundingEventIDList.Contains(x.FundingEventID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteFundingEvent(this IQueryable<FundingEvent> fundingEvents, ICollection<FundingEvent> fundingEventsToDelete)
        {
            if(fundingEventsToDelete.Any())
            {
                var fundingEventIDList = fundingEventsToDelete.Select(x => x.FundingEventID).ToList();
                fundingEvents.Where(x => fundingEventIDList.Contains(x.FundingEventID)).Delete();
            }
        }

        public static void DeleteFundingEvent(this IQueryable<FundingEvent> fundingEvents, int fundingEventID)
        {
            DeleteFundingEvent(fundingEvents, new List<int> { fundingEventID });
        }

        public static void DeleteFundingEvent(this IQueryable<FundingEvent> fundingEvents, FundingEvent fundingEventToDelete)
        {
            DeleteFundingEvent(fundingEvents, new List<FundingEvent> { fundingEventToDelete });
        }
    }
}