//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]
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
        public static RegionalSubbasinStaging GetRegionalSubbasinStaging(this IQueryable<RegionalSubbasinStaging> regionalSubbasinStagings, int regionalSubbasinStagingID)
        {
            var regionalSubbasinStaging = regionalSubbasinStagings.SingleOrDefault(x => x.RegionalSubbasinStagingID == regionalSubbasinStagingID);
            Check.RequireNotNullThrowNotFound(regionalSubbasinStaging, "RegionalSubbasinStaging", regionalSubbasinStagingID);
            return regionalSubbasinStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteRegionalSubbasinStaging(this IQueryable<RegionalSubbasinStaging> regionalSubbasinStagings, List<int> regionalSubbasinStagingIDList)
        {
            if(regionalSubbasinStagingIDList.Any())
            {
                regionalSubbasinStagings.Where(x => regionalSubbasinStagingIDList.Contains(x.RegionalSubbasinStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteRegionalSubbasinStaging(this IQueryable<RegionalSubbasinStaging> regionalSubbasinStagings, ICollection<RegionalSubbasinStaging> regionalSubbasinStagingsToDelete)
        {
            if(regionalSubbasinStagingsToDelete.Any())
            {
                var regionalSubbasinStagingIDList = regionalSubbasinStagingsToDelete.Select(x => x.RegionalSubbasinStagingID).ToList();
                regionalSubbasinStagings.Where(x => regionalSubbasinStagingIDList.Contains(x.RegionalSubbasinStagingID)).Delete();
            }
        }

        public static void DeleteRegionalSubbasinStaging(this IQueryable<RegionalSubbasinStaging> regionalSubbasinStagings, int regionalSubbasinStagingID)
        {
            DeleteRegionalSubbasinStaging(regionalSubbasinStagings, new List<int> { regionalSubbasinStagingID });
        }

        public static void DeleteRegionalSubbasinStaging(this IQueryable<RegionalSubbasinStaging> regionalSubbasinStagings, RegionalSubbasinStaging regionalSubbasinStagingToDelete)
        {
            DeleteRegionalSubbasinStaging(regionalSubbasinStagings, new List<RegionalSubbasinStaging> { regionalSubbasinStagingToDelete });
        }
    }
}