//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasin]
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
        public static RegionalSubbasin GetRegionalSubbasin(this IQueryable<RegionalSubbasin> regionalSubbasins, int regionalSubbasinID)
        {
            var regionalSubbasin = regionalSubbasins.SingleOrDefault(x => x.RegionalSubbasinID == regionalSubbasinID);
            Check.RequireNotNullThrowNotFound(regionalSubbasin, "RegionalSubbasin", regionalSubbasinID);
            return regionalSubbasin;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteRegionalSubbasin(this IQueryable<RegionalSubbasin> regionalSubbasins, List<int> regionalSubbasinIDList)
        {
            if(regionalSubbasinIDList.Any())
            {
                regionalSubbasins.Where(x => regionalSubbasinIDList.Contains(x.RegionalSubbasinID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteRegionalSubbasin(this IQueryable<RegionalSubbasin> regionalSubbasins, ICollection<RegionalSubbasin> regionalSubbasinsToDelete)
        {
            if(regionalSubbasinsToDelete.Any())
            {
                var regionalSubbasinIDList = regionalSubbasinsToDelete.Select(x => x.RegionalSubbasinID).ToList();
                regionalSubbasins.Where(x => regionalSubbasinIDList.Contains(x.RegionalSubbasinID)).Delete();
            }
        }

        public static void DeleteRegionalSubbasin(this IQueryable<RegionalSubbasin> regionalSubbasins, int regionalSubbasinID)
        {
            DeleteRegionalSubbasin(regionalSubbasins, new List<int> { regionalSubbasinID });
        }

        public static void DeleteRegionalSubbasin(this IQueryable<RegionalSubbasin> regionalSubbasins, RegionalSubbasin regionalSubbasinToDelete)
        {
            DeleteRegionalSubbasin(regionalSubbasins, new List<RegionalSubbasin> { regionalSubbasinToDelete });
        }
    }
}