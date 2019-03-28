//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchment]
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
        public static NetworkCatchment GetNetworkCatchment(this IQueryable<NetworkCatchment> networkCatchments, int networkCatchmentID)
        {
            var networkCatchment = networkCatchments.SingleOrDefault(x => x.NetworkCatchmentID == networkCatchmentID);
            Check.RequireNotNullThrowNotFound(networkCatchment, "NetworkCatchment", networkCatchmentID);
            return networkCatchment;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteNetworkCatchment(this IQueryable<NetworkCatchment> networkCatchments, List<int> networkCatchmentIDList)
        {
            if(networkCatchmentIDList.Any())
            {
                networkCatchments.Where(x => networkCatchmentIDList.Contains(x.NetworkCatchmentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteNetworkCatchment(this IQueryable<NetworkCatchment> networkCatchments, ICollection<NetworkCatchment> networkCatchmentsToDelete)
        {
            if(networkCatchmentsToDelete.Any())
            {
                var networkCatchmentIDList = networkCatchmentsToDelete.Select(x => x.NetworkCatchmentID).ToList();
                networkCatchments.Where(x => networkCatchmentIDList.Contains(x.NetworkCatchmentID)).Delete();
            }
        }

        public static void DeleteNetworkCatchment(this IQueryable<NetworkCatchment> networkCatchments, int networkCatchmentID)
        {
            DeleteNetworkCatchment(networkCatchments, new List<int> { networkCatchmentID });
        }

        public static void DeleteNetworkCatchment(this IQueryable<NetworkCatchment> networkCatchments, NetworkCatchment networkCatchmentToDelete)
        {
            DeleteNetworkCatchment(networkCatchments, new List<NetworkCatchment> { networkCatchmentToDelete });
        }
    }
}