//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchmentStaging]
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
        public static NetworkCatchmentStaging GetNetworkCatchmentStaging(this IQueryable<NetworkCatchmentStaging> networkCatchmentStagings, int networkCatchmentStagingID)
        {
            var networkCatchmentStaging = networkCatchmentStagings.SingleOrDefault(x => x.NetworkCatchmentStagingID == networkCatchmentStagingID);
            Check.RequireNotNullThrowNotFound(networkCatchmentStaging, "NetworkCatchmentStaging", networkCatchmentStagingID);
            return networkCatchmentStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteNetworkCatchmentStaging(this IQueryable<NetworkCatchmentStaging> networkCatchmentStagings, List<int> networkCatchmentStagingIDList)
        {
            if(networkCatchmentStagingIDList.Any())
            {
                networkCatchmentStagings.Where(x => networkCatchmentStagingIDList.Contains(x.NetworkCatchmentStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteNetworkCatchmentStaging(this IQueryable<NetworkCatchmentStaging> networkCatchmentStagings, ICollection<NetworkCatchmentStaging> networkCatchmentStagingsToDelete)
        {
            if(networkCatchmentStagingsToDelete.Any())
            {
                var networkCatchmentStagingIDList = networkCatchmentStagingsToDelete.Select(x => x.NetworkCatchmentStagingID).ToList();
                networkCatchmentStagings.Where(x => networkCatchmentStagingIDList.Contains(x.NetworkCatchmentStagingID)).Delete();
            }
        }

        public static void DeleteNetworkCatchmentStaging(this IQueryable<NetworkCatchmentStaging> networkCatchmentStagings, int networkCatchmentStagingID)
        {
            DeleteNetworkCatchmentStaging(networkCatchmentStagings, new List<int> { networkCatchmentStagingID });
        }

        public static void DeleteNetworkCatchmentStaging(this IQueryable<NetworkCatchmentStaging> networkCatchmentStagings, NetworkCatchmentStaging networkCatchmentStagingToDelete)
        {
            DeleteNetworkCatchmentStaging(networkCatchmentStagings, new List<NetworkCatchmentStaging> { networkCatchmentStagingToDelete });
        }
    }
}