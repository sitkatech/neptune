//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
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
        public static QuickBMP GetQuickBMP(this IQueryable<QuickBMP> quickBMPs, int quickBMPID)
        {
            var quickBMP = quickBMPs.SingleOrDefault(x => x.QuickBMPID == quickBMPID);
            Check.RequireNotNullThrowNotFound(quickBMP, "QuickBMP", quickBMPID);
            return quickBMP;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteQuickBMP(this IQueryable<QuickBMP> quickBMPs, List<int> quickBMPIDList)
        {
            if(quickBMPIDList.Any())
            {
                quickBMPs.Where(x => quickBMPIDList.Contains(x.QuickBMPID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteQuickBMP(this IQueryable<QuickBMP> quickBMPs, ICollection<QuickBMP> quickBMPsToDelete)
        {
            if(quickBMPsToDelete.Any())
            {
                var quickBMPIDList = quickBMPsToDelete.Select(x => x.QuickBMPID).ToList();
                quickBMPs.Where(x => quickBMPIDList.Contains(x.QuickBMPID)).Delete();
            }
        }

        public static void DeleteQuickBMP(this IQueryable<QuickBMP> quickBMPs, int quickBMPID)
        {
            DeleteQuickBMP(quickBMPs, new List<int> { quickBMPID });
        }

        public static void DeleteQuickBMP(this IQueryable<QuickBMP> quickBMPs, QuickBMP quickBMPToDelete)
        {
            DeleteQuickBMP(quickBMPs, new List<QuickBMP> { quickBMPToDelete });
        }
    }
}