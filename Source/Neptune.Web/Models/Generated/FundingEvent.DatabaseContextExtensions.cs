//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteFundingEvent(this List<int> fundingEventIDList)
        {
            if(fundingEventIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFundingEvents.RemoveRange(HttpRequestStorage.DatabaseEntities.FundingEvents.Where(x => fundingEventIDList.Contains(x.FundingEventID)));
            }
        }

        public static void DeleteFundingEvent(this ICollection<FundingEvent> fundingEventsToDelete)
        {
            if(fundingEventsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFundingEvents.RemoveRange(fundingEventsToDelete);
            }
        }

        public static void DeleteFundingEvent(this int fundingEventID)
        {
            DeleteFundingEvent(new List<int> { fundingEventID });
        }

        public static void DeleteFundingEvent(this FundingEvent fundingEventToDelete)
        {
            DeleteFundingEvent(new List<FundingEvent> { fundingEventToDelete });
        }
    }
}