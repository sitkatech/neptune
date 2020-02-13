//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
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
        public static RegionalSubbasinRevisionRequest GetRegionalSubbasinRevisionRequest(this IQueryable<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequests, int regionalSubbasinRevisionRequestID)
        {
            var regionalSubbasinRevisionRequest = regionalSubbasinRevisionRequests.SingleOrDefault(x => x.RegionalSubbasinRevisionRequestID == regionalSubbasinRevisionRequestID);
            Check.RequireNotNullThrowNotFound(regionalSubbasinRevisionRequest, "RegionalSubbasinRevisionRequest", regionalSubbasinRevisionRequestID);
            return regionalSubbasinRevisionRequest;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteRegionalSubbasinRevisionRequest(this IQueryable<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequests, List<int> regionalSubbasinRevisionRequestIDList)
        {
            if(regionalSubbasinRevisionRequestIDList.Any())
            {
                regionalSubbasinRevisionRequests.Where(x => regionalSubbasinRevisionRequestIDList.Contains(x.RegionalSubbasinRevisionRequestID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteRegionalSubbasinRevisionRequest(this IQueryable<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequests, ICollection<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequestsToDelete)
        {
            if(regionalSubbasinRevisionRequestsToDelete.Any())
            {
                var regionalSubbasinRevisionRequestIDList = regionalSubbasinRevisionRequestsToDelete.Select(x => x.RegionalSubbasinRevisionRequestID).ToList();
                regionalSubbasinRevisionRequests.Where(x => regionalSubbasinRevisionRequestIDList.Contains(x.RegionalSubbasinRevisionRequestID)).Delete();
            }
        }

        public static void DeleteRegionalSubbasinRevisionRequest(this IQueryable<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequests, int regionalSubbasinRevisionRequestID)
        {
            DeleteRegionalSubbasinRevisionRequest(regionalSubbasinRevisionRequests, new List<int> { regionalSubbasinRevisionRequestID });
        }

        public static void DeleteRegionalSubbasinRevisionRequest(this IQueryable<RegionalSubbasinRevisionRequest> regionalSubbasinRevisionRequests, RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequestToDelete)
        {
            DeleteRegionalSubbasinRevisionRequest(regionalSubbasinRevisionRequests, new List<RegionalSubbasinRevisionRequest> { regionalSubbasinRevisionRequestToDelete });
        }
    }
}