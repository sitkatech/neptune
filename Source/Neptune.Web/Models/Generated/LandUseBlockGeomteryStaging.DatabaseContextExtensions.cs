//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeomteryStaging]
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
        public static LandUseBlockGeomteryStaging GetLandUseBlockGeomteryStaging(this IQueryable<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagings, int landUseBlockStagingID)
        {
            var landUseBlockGeomteryStaging = landUseBlockGeomteryStagings.SingleOrDefault(x => x.LandUseBlockStagingID == landUseBlockStagingID);
            Check.RequireNotNullThrowNotFound(landUseBlockGeomteryStaging, "LandUseBlockGeomteryStaging", landUseBlockStagingID);
            return landUseBlockGeomteryStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLandUseBlockGeomteryStaging(this IQueryable<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagings, List<int> landUseBlockStagingIDList)
        {
            if(landUseBlockStagingIDList.Any())
            {
                landUseBlockGeomteryStagings.Where(x => landUseBlockStagingIDList.Contains(x.LandUseBlockStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLandUseBlockGeomteryStaging(this IQueryable<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagings, ICollection<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagingsToDelete)
        {
            if(landUseBlockGeomteryStagingsToDelete.Any())
            {
                var landUseBlockStagingIDList = landUseBlockGeomteryStagingsToDelete.Select(x => x.LandUseBlockStagingID).ToList();
                landUseBlockGeomteryStagings.Where(x => landUseBlockStagingIDList.Contains(x.LandUseBlockStagingID)).Delete();
            }
        }

        public static void DeleteLandUseBlockGeomteryStaging(this IQueryable<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagings, int landUseBlockStagingID)
        {
            DeleteLandUseBlockGeomteryStaging(landUseBlockGeomteryStagings, new List<int> { landUseBlockStagingID });
        }

        public static void DeleteLandUseBlockGeomteryStaging(this IQueryable<LandUseBlockGeomteryStaging> landUseBlockGeomteryStagings, LandUseBlockGeomteryStaging landUseBlockGeomteryStagingToDelete)
        {
            DeleteLandUseBlockGeomteryStaging(landUseBlockGeomteryStagings, new List<LandUseBlockGeomteryStaging> { landUseBlockGeomteryStagingToDelete });
        }
    }
}