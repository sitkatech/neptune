//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockGeometryStaging]
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
        public static LandUseBlockGeometryStaging GetLandUseBlockGeometryStaging(this IQueryable<LandUseBlockGeometryStaging> landUseBlockGeometryStagings, int landUseBlockGeometryStagingID)
        {
            var landUseBlockGeometryStaging = landUseBlockGeometryStagings.SingleOrDefault(x => x.LandUseBlockGeometryStagingID == landUseBlockGeometryStagingID);
            Check.RequireNotNullThrowNotFound(landUseBlockGeometryStaging, "LandUseBlockGeometryStaging", landUseBlockGeometryStagingID);
            return landUseBlockGeometryStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLandUseBlockGeometryStaging(this IQueryable<LandUseBlockGeometryStaging> landUseBlockGeometryStagings, List<int> landUseBlockGeometryStagingIDList)
        {
            if(landUseBlockGeometryStagingIDList.Any())
            {
                landUseBlockGeometryStagings.Where(x => landUseBlockGeometryStagingIDList.Contains(x.LandUseBlockGeometryStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLandUseBlockGeometryStaging(this IQueryable<LandUseBlockGeometryStaging> landUseBlockGeometryStagings, ICollection<LandUseBlockGeometryStaging> landUseBlockGeometryStagingsToDelete)
        {
            if(landUseBlockGeometryStagingsToDelete.Any())
            {
                var landUseBlockGeometryStagingIDList = landUseBlockGeometryStagingsToDelete.Select(x => x.LandUseBlockGeometryStagingID).ToList();
                landUseBlockGeometryStagings.Where(x => landUseBlockGeometryStagingIDList.Contains(x.LandUseBlockGeometryStagingID)).Delete();
            }
        }

        public static void DeleteLandUseBlockGeometryStaging(this IQueryable<LandUseBlockGeometryStaging> landUseBlockGeometryStagings, int landUseBlockGeometryStagingID)
        {
            DeleteLandUseBlockGeometryStaging(landUseBlockGeometryStagings, new List<int> { landUseBlockGeometryStagingID });
        }

        public static void DeleteLandUseBlockGeometryStaging(this IQueryable<LandUseBlockGeometryStaging> landUseBlockGeometryStagings, LandUseBlockGeometryStaging landUseBlockGeometryStagingToDelete)
        {
            DeleteLandUseBlockGeometryStaging(landUseBlockGeometryStagings, new List<LandUseBlockGeometryStaging> { landUseBlockGeometryStagingToDelete });
        }
    }
}