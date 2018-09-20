//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteQuickBMP(this List<int> quickBMPIDList)
        {
            if(quickBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllQuickBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.QuickBMPs.Where(x => quickBMPIDList.Contains(x.QuickBMPID)));
            }
        }

        public static void DeleteQuickBMP(this ICollection<QuickBMP> quickBMPsToDelete)
        {
            if(quickBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllQuickBMPs.RemoveRange(quickBMPsToDelete);
            }
        }

        public static void DeleteQuickBMP(this int quickBMPID)
        {
            DeleteQuickBMP(new List<int> { quickBMPID });
        }

        public static void DeleteQuickBMP(this QuickBMP quickBMPToDelete)
        {
            DeleteQuickBMP(new List<QuickBMP> { quickBMPToDelete });
        }
    }
}