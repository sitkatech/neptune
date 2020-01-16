//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasinStaging]
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
        public static LSPCBasinStaging GetLSPCBasinStaging(this IQueryable<LSPCBasinStaging> lSPCBasinStagings, int lSPCBasinStagingID)
        {
            var lSPCBasinStaging = lSPCBasinStagings.SingleOrDefault(x => x.LSPCBasinStagingID == lSPCBasinStagingID);
            Check.RequireNotNullThrowNotFound(lSPCBasinStaging, "LSPCBasinStaging", lSPCBasinStagingID);
            return lSPCBasinStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLSPCBasinStaging(this IQueryable<LSPCBasinStaging> lSPCBasinStagings, List<int> lSPCBasinStagingIDList)
        {
            if(lSPCBasinStagingIDList.Any())
            {
                lSPCBasinStagings.Where(x => lSPCBasinStagingIDList.Contains(x.LSPCBasinStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLSPCBasinStaging(this IQueryable<LSPCBasinStaging> lSPCBasinStagings, ICollection<LSPCBasinStaging> lSPCBasinStagingsToDelete)
        {
            if(lSPCBasinStagingsToDelete.Any())
            {
                var lSPCBasinStagingIDList = lSPCBasinStagingsToDelete.Select(x => x.LSPCBasinStagingID).ToList();
                lSPCBasinStagings.Where(x => lSPCBasinStagingIDList.Contains(x.LSPCBasinStagingID)).Delete();
            }
        }

        public static void DeleteLSPCBasinStaging(this IQueryable<LSPCBasinStaging> lSPCBasinStagings, int lSPCBasinStagingID)
        {
            DeleteLSPCBasinStaging(lSPCBasinStagings, new List<int> { lSPCBasinStagingID });
        }

        public static void DeleteLSPCBasinStaging(this IQueryable<LSPCBasinStaging> lSPCBasinStagings, LSPCBasinStaging lSPCBasinStagingToDelete)
        {
            DeleteLSPCBasinStaging(lSPCBasinStagings, new List<LSPCBasinStaging> { lSPCBasinStagingToDelete });
        }
    }
}