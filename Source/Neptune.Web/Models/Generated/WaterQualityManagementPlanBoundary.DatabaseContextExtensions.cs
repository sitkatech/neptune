//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanBoundary]
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
        public static WaterQualityManagementPlanBoundary GetWaterQualityManagementPlanBoundary(this IQueryable<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundaries, int waterQualityManagementPlanGeometryID)
        {
            var waterQualityManagementPlanBoundary = waterQualityManagementPlanBoundaries.SingleOrDefault(x => x.WaterQualityManagementPlanGeometryID == waterQualityManagementPlanGeometryID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanBoundary, "WaterQualityManagementPlanBoundary", waterQualityManagementPlanGeometryID);
            return waterQualityManagementPlanBoundary;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanBoundary(this IQueryable<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundaries, List<int> waterQualityManagementPlanGeometryIDList)
        {
            if(waterQualityManagementPlanGeometryIDList.Any())
            {
                waterQualityManagementPlanBoundaries.Where(x => waterQualityManagementPlanGeometryIDList.Contains(x.WaterQualityManagementPlanGeometryID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanBoundary(this IQueryable<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundaries, ICollection<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundariesToDelete)
        {
            if(waterQualityManagementPlanBoundariesToDelete.Any())
            {
                var waterQualityManagementPlanGeometryIDList = waterQualityManagementPlanBoundariesToDelete.Select(x => x.WaterQualityManagementPlanGeometryID).ToList();
                waterQualityManagementPlanBoundaries.Where(x => waterQualityManagementPlanGeometryIDList.Contains(x.WaterQualityManagementPlanGeometryID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanBoundary(this IQueryable<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundaries, int waterQualityManagementPlanGeometryID)
        {
            DeleteWaterQualityManagementPlanBoundary(waterQualityManagementPlanBoundaries, new List<int> { waterQualityManagementPlanGeometryID });
        }

        public static void DeleteWaterQualityManagementPlanBoundary(this IQueryable<WaterQualityManagementPlanBoundary> waterQualityManagementPlanBoundaries, WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundaryToDelete)
        {
            DeleteWaterQualityManagementPlanBoundary(waterQualityManagementPlanBoundaries, new List<WaterQualityManagementPlanBoundary> { waterQualityManagementPlanBoundaryToDelete });
        }
    }
}