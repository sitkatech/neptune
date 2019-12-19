//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]
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
        public static DelineationOverlap GetDelineationOverlap(this IQueryable<DelineationOverlap> delineationOverlaps, int delineationOverlapID)
        {
            var delineationOverlap = delineationOverlaps.SingleOrDefault(x => x.DelineationOverlapID == delineationOverlapID);
            Check.RequireNotNullThrowNotFound(delineationOverlap, "DelineationOverlap", delineationOverlapID);
            return delineationOverlap;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDelineationOverlap(this IQueryable<DelineationOverlap> delineationOverlaps, List<int> delineationOverlapIDList)
        {
            if(delineationOverlapIDList.Any())
            {
                delineationOverlaps.Where(x => delineationOverlapIDList.Contains(x.DelineationOverlapID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDelineationOverlap(this IQueryable<DelineationOverlap> delineationOverlaps, ICollection<DelineationOverlap> delineationOverlapsToDelete)
        {
            if(delineationOverlapsToDelete.Any())
            {
                var delineationOverlapIDList = delineationOverlapsToDelete.Select(x => x.DelineationOverlapID).ToList();
                delineationOverlaps.Where(x => delineationOverlapIDList.Contains(x.DelineationOverlapID)).Delete();
            }
        }

        public static void DeleteDelineationOverlap(this IQueryable<DelineationOverlap> delineationOverlaps, int delineationOverlapID)
        {
            DeleteDelineationOverlap(delineationOverlaps, new List<int> { delineationOverlapID });
        }

        public static void DeleteDelineationOverlap(this IQueryable<DelineationOverlap> delineationOverlaps, DelineationOverlap delineationOverlapToDelete)
        {
            DeleteDelineationOverlap(delineationOverlaps, new List<DelineationOverlap> { delineationOverlapToDelete });
        }
    }
}