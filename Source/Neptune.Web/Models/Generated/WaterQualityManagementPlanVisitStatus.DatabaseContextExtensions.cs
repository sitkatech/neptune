//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteWaterQualityManagementPlanVisitStatus(this List<int> waterQualityManagementPlanVisitStatusIDList)
        {
            if(waterQualityManagementPlanVisitStatusIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVisitStatuses.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVisitStatuses.Where(x => waterQualityManagementPlanVisitStatusIDList.Contains(x.WaterQualityManagementPlanVisitStatusID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVisitStatus(this ICollection<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatusesToDelete)
        {
            if(waterQualityManagementPlanVisitStatusesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVisitStatuses.RemoveRange(waterQualityManagementPlanVisitStatusesToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVisitStatus(this int waterQualityManagementPlanVisitStatusID)
        {
            DeleteWaterQualityManagementPlanVisitStatus(new List<int> { waterQualityManagementPlanVisitStatusID });
        }

        public static void DeleteWaterQualityManagementPlanVisitStatus(this WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatusToDelete)
        {
            DeleteWaterQualityManagementPlanVisitStatus(new List<WaterQualityManagementPlanVisitStatus> { waterQualityManagementPlanVisitStatusToDelete });
        }
    }
}