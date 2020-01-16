//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasin]
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
        public static LSPCBasin GetLSPCBasin(this IQueryable<LSPCBasin> lSPCBasins, int lSPCBasinID)
        {
            var lSPCBasin = lSPCBasins.SingleOrDefault(x => x.LSPCBasinID == lSPCBasinID);
            Check.RequireNotNullThrowNotFound(lSPCBasin, "LSPCBasin", lSPCBasinID);
            return lSPCBasin;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLSPCBasin(this IQueryable<LSPCBasin> lSPCBasins, List<int> lSPCBasinIDList)
        {
            if(lSPCBasinIDList.Any())
            {
                lSPCBasins.Where(x => lSPCBasinIDList.Contains(x.LSPCBasinID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLSPCBasin(this IQueryable<LSPCBasin> lSPCBasins, ICollection<LSPCBasin> lSPCBasinsToDelete)
        {
            if(lSPCBasinsToDelete.Any())
            {
                var lSPCBasinIDList = lSPCBasinsToDelete.Select(x => x.LSPCBasinID).ToList();
                lSPCBasins.Where(x => lSPCBasinIDList.Contains(x.LSPCBasinID)).Delete();
            }
        }

        public static void DeleteLSPCBasin(this IQueryable<LSPCBasin> lSPCBasins, int lSPCBasinID)
        {
            DeleteLSPCBasin(lSPCBasins, new List<int> { lSPCBasinID });
        }

        public static void DeleteLSPCBasin(this IQueryable<LSPCBasin> lSPCBasins, LSPCBasin lSPCBasinToDelete)
        {
            DeleteLSPCBasin(lSPCBasins, new List<LSPCBasin> { lSPCBasinToDelete });
        }
    }
}