//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]
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
        public static LandUseBlockStaging GetLandUseBlockStaging(this IQueryable<LandUseBlockStaging> landUseBlockStagings, int landUseBlockStagingID)
        {
            var landUseBlockStaging = landUseBlockStagings.SingleOrDefault(x => x.LandUseBlockStagingID == landUseBlockStagingID);
            Check.RequireNotNullThrowNotFound(landUseBlockStaging, "LandUseBlockStaging", landUseBlockStagingID);
            return landUseBlockStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLandUseBlockStaging(this IQueryable<LandUseBlockStaging> landUseBlockStagings, List<int> landUseBlockStagingIDList)
        {
            if(landUseBlockStagingIDList.Any())
            {
                landUseBlockStagings.Where(x => landUseBlockStagingIDList.Contains(x.LandUseBlockStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLandUseBlockStaging(this IQueryable<LandUseBlockStaging> landUseBlockStagings, ICollection<LandUseBlockStaging> landUseBlockStagingsToDelete)
        {
            if(landUseBlockStagingsToDelete.Any())
            {
                var landUseBlockStagingIDList = landUseBlockStagingsToDelete.Select(x => x.LandUseBlockStagingID).ToList();
                landUseBlockStagings.Where(x => landUseBlockStagingIDList.Contains(x.LandUseBlockStagingID)).Delete();
            }
        }

        public static void DeleteLandUseBlockStaging(this IQueryable<LandUseBlockStaging> landUseBlockStagings, int landUseBlockStagingID)
        {
            DeleteLandUseBlockStaging(landUseBlockStagings, new List<int> { landUseBlockStagingID });
        }

        public static void DeleteLandUseBlockStaging(this IQueryable<LandUseBlockStaging> landUseBlockStagings, LandUseBlockStaging landUseBlockStagingToDelete)
        {
            DeleteLandUseBlockStaging(landUseBlockStagings, new List<LandUseBlockStaging> { landUseBlockStagingToDelete });
        }
    }
}