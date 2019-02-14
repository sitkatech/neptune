//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]
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
        public static WaterQualityManagementPlanVisitStatus GetWaterQualityManagementPlanVisitStatus(this IQueryable<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses, int waterQualityManagementPlanVisitStatusID)
        {
            var waterQualityManagementPlanVisitStatus = waterQualityManagementPlanVisitStatuses.SingleOrDefault(x => x.WaterQualityManagementPlanVisitStatusID == waterQualityManagementPlanVisitStatusID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVisitStatus, "WaterQualityManagementPlanVisitStatus", waterQualityManagementPlanVisitStatusID);
            return waterQualityManagementPlanVisitStatus;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanVisitStatus(this IQueryable<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses, List<int> waterQualityManagementPlanVisitStatusIDList)
        {
            if(waterQualityManagementPlanVisitStatusIDList.Any())
            {
                waterQualityManagementPlanVisitStatuses.Where(x => waterQualityManagementPlanVisitStatusIDList.Contains(x.WaterQualityManagementPlanVisitStatusID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanVisitStatus(this IQueryable<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses, ICollection<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatusesToDelete)
        {
            if(waterQualityManagementPlanVisitStatusesToDelete.Any())
            {
                var waterQualityManagementPlanVisitStatusIDList = waterQualityManagementPlanVisitStatusesToDelete.Select(x => x.WaterQualityManagementPlanVisitStatusID).ToList();
                waterQualityManagementPlanVisitStatuses.Where(x => waterQualityManagementPlanVisitStatusIDList.Contains(x.WaterQualityManagementPlanVisitStatusID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVisitStatus(this IQueryable<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses, int waterQualityManagementPlanVisitStatusID)
        {
            DeleteWaterQualityManagementPlanVisitStatus(waterQualityManagementPlanVisitStatuses, new List<int> { waterQualityManagementPlanVisitStatusID });
        }

        public static void DeleteWaterQualityManagementPlanVisitStatus(this IQueryable<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses, WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatusToDelete)
        {
            DeleteWaterQualityManagementPlanVisitStatus(waterQualityManagementPlanVisitStatuses, new List<WaterQualityManagementPlanVisitStatus> { waterQualityManagementPlanVisitStatusToDelete });
        }
    }
}