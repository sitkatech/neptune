//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationGeometryStaging]
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
        public static DelineationGeometryStaging GetDelineationGeometryStaging(this IQueryable<DelineationGeometryStaging> delineationGeometryStagings, int delineationGeometryStagingID)
        {
            var delineationGeometryStaging = delineationGeometryStagings.SingleOrDefault(x => x.DelineationGeometryStagingID == delineationGeometryStagingID);
            Check.RequireNotNullThrowNotFound(delineationGeometryStaging, "DelineationGeometryStaging", delineationGeometryStagingID);
            return delineationGeometryStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDelineationGeometryStaging(this IQueryable<DelineationGeometryStaging> delineationGeometryStagings, List<int> delineationGeometryStagingIDList)
        {
            if(delineationGeometryStagingIDList.Any())
            {
                delineationGeometryStagings.Where(x => delineationGeometryStagingIDList.Contains(x.DelineationGeometryStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDelineationGeometryStaging(this IQueryable<DelineationGeometryStaging> delineationGeometryStagings, ICollection<DelineationGeometryStaging> delineationGeometryStagingsToDelete)
        {
            if(delineationGeometryStagingsToDelete.Any())
            {
                var delineationGeometryStagingIDList = delineationGeometryStagingsToDelete.Select(x => x.DelineationGeometryStagingID).ToList();
                delineationGeometryStagings.Where(x => delineationGeometryStagingIDList.Contains(x.DelineationGeometryStagingID)).Delete();
            }
        }

        public static void DeleteDelineationGeometryStaging(this IQueryable<DelineationGeometryStaging> delineationGeometryStagings, int delineationGeometryStagingID)
        {
            DeleteDelineationGeometryStaging(delineationGeometryStagings, new List<int> { delineationGeometryStagingID });
        }

        public static void DeleteDelineationGeometryStaging(this IQueryable<DelineationGeometryStaging> delineationGeometryStagings, DelineationGeometryStaging delineationGeometryStagingToDelete)
        {
            DeleteDelineationGeometryStaging(delineationGeometryStagings, new List<DelineationGeometryStaging> { delineationGeometryStagingToDelete });
        }
    }
}