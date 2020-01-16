//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DroolToolWatershed]
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
        public static DroolToolWatershed GetDroolToolWatershed(this IQueryable<DroolToolWatershed> droolToolWatersheds, int droolToolWatershedID)
        {
            var droolToolWatershed = droolToolWatersheds.SingleOrDefault(x => x.DroolToolWatershedID == droolToolWatershedID);
            Check.RequireNotNullThrowNotFound(droolToolWatershed, "DroolToolWatershed", droolToolWatershedID);
            return droolToolWatershed;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDroolToolWatershed(this IQueryable<DroolToolWatershed> droolToolWatersheds, List<int> droolToolWatershedIDList)
        {
            if(droolToolWatershedIDList.Any())
            {
                droolToolWatersheds.Where(x => droolToolWatershedIDList.Contains(x.DroolToolWatershedID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDroolToolWatershed(this IQueryable<DroolToolWatershed> droolToolWatersheds, ICollection<DroolToolWatershed> droolToolWatershedsToDelete)
        {
            if(droolToolWatershedsToDelete.Any())
            {
                var droolToolWatershedIDList = droolToolWatershedsToDelete.Select(x => x.DroolToolWatershedID).ToList();
                droolToolWatersheds.Where(x => droolToolWatershedIDList.Contains(x.DroolToolWatershedID)).Delete();
            }
        }

        public static void DeleteDroolToolWatershed(this IQueryable<DroolToolWatershed> droolToolWatersheds, int droolToolWatershedID)
        {
            DeleteDroolToolWatershed(droolToolWatersheds, new List<int> { droolToolWatershedID });
        }

        public static void DeleteDroolToolWatershed(this IQueryable<DroolToolWatershed> droolToolWatersheds, DroolToolWatershed droolToolWatershedToDelete)
        {
            DeleteDroolToolWatershed(droolToolWatersheds, new List<DroolToolWatershed> { droolToolWatershedToDelete });
        }
    }
}