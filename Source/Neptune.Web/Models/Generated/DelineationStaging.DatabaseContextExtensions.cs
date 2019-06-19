//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]
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
        public static DelineationStaging GetDelineationStaging(this IQueryable<DelineationStaging> delineationStagings, int delineationStagingID)
        {
            var delineationStaging = delineationStagings.SingleOrDefault(x => x.DelineationStagingID == delineationStagingID);
            Check.RequireNotNullThrowNotFound(delineationStaging, "DelineationStaging", delineationStagingID);
            return delineationStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDelineationStaging(this IQueryable<DelineationStaging> delineationStagings, List<int> delineationStagingIDList)
        {
            if(delineationStagingIDList.Any())
            {
                delineationStagings.Where(x => delineationStagingIDList.Contains(x.DelineationStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDelineationStaging(this IQueryable<DelineationStaging> delineationStagings, ICollection<DelineationStaging> delineationStagingsToDelete)
        {
            if(delineationStagingsToDelete.Any())
            {
                var delineationStagingIDList = delineationStagingsToDelete.Select(x => x.DelineationStagingID).ToList();
                delineationStagings.Where(x => delineationStagingIDList.Contains(x.DelineationStagingID)).Delete();
            }
        }

        public static void DeleteDelineationStaging(this IQueryable<DelineationStaging> delineationStagings, int delineationStagingID)
        {
            DeleteDelineationStaging(delineationStagings, new List<int> { delineationStagingID });
        }

        public static void DeleteDelineationStaging(this IQueryable<DelineationStaging> delineationStagings, DelineationStaging delineationStagingToDelete)
        {
            DeleteDelineationStaging(delineationStagings, new List<DelineationStaging> { delineationStagingToDelete });
        }
    }
}