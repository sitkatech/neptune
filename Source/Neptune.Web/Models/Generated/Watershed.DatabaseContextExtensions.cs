//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Watershed]
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
        public static Watershed GetWatershed(this IQueryable<Watershed> watersheds, int watershedID)
        {
            var watershed = watersheds.SingleOrDefault(x => x.WatershedID == watershedID);
            Check.RequireNotNullThrowNotFound(watershed, "Watershed", watershedID);
            return watershed;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWatershed(this IQueryable<Watershed> watersheds, List<int> watershedIDList)
        {
            if(watershedIDList.Any())
            {
                watersheds.Where(x => watershedIDList.Contains(x.WatershedID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWatershed(this IQueryable<Watershed> watersheds, ICollection<Watershed> watershedsToDelete)
        {
            if(watershedsToDelete.Any())
            {
                var watershedIDList = watershedsToDelete.Select(x => x.WatershedID).ToList();
                watersheds.Where(x => watershedIDList.Contains(x.WatershedID)).Delete();
            }
        }

        public static void DeleteWatershed(this IQueryable<Watershed> watersheds, int watershedID)
        {
            DeleteWatershed(watersheds, new List<int> { watershedID });
        }

        public static void DeleteWatershed(this IQueryable<Watershed> watersheds, Watershed watershedToDelete)
        {
            DeleteWatershed(watersheds, new List<Watershed> { watershedToDelete });
        }
    }
}