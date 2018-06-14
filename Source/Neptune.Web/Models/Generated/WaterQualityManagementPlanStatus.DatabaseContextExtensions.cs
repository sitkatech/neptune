//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanStatus GetWaterQualityManagementPlanStatus(this IQueryable<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatuses, int waterQualityManagementPlanStatusID)
        {
            var waterQualityManagementPlanStatus = waterQualityManagementPlanStatuses.SingleOrDefault(x => x.WaterQualityManagementPlanStatusID == waterQualityManagementPlanStatusID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanStatus, "WaterQualityManagementPlanStatus", waterQualityManagementPlanStatusID);
            return waterQualityManagementPlanStatus;
        }

        public static void DeleteWaterQualityManagementPlanStatus(this IQueryable<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatuses, List<int> waterQualityManagementPlanStatusIDList)
        {
            if(waterQualityManagementPlanStatusIDList.Any())
            {
                waterQualityManagementPlanStatuses.Where(x => waterQualityManagementPlanStatusIDList.Contains(x.WaterQualityManagementPlanStatusID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanStatus(this IQueryable<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatuses, ICollection<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatusesToDelete)
        {
            if(waterQualityManagementPlanStatusesToDelete.Any())
            {
                var waterQualityManagementPlanStatusIDList = waterQualityManagementPlanStatusesToDelete.Select(x => x.WaterQualityManagementPlanStatusID).ToList();
                waterQualityManagementPlanStatuses.Where(x => waterQualityManagementPlanStatusIDList.Contains(x.WaterQualityManagementPlanStatusID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanStatus(this IQueryable<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatuses, int waterQualityManagementPlanStatusID)
        {
            DeleteWaterQualityManagementPlanStatus(waterQualityManagementPlanStatuses, new List<int> { waterQualityManagementPlanStatusID });
        }

        public static void DeleteWaterQualityManagementPlanStatus(this IQueryable<WaterQualityManagementPlanStatus> waterQualityManagementPlanStatuses, WaterQualityManagementPlanStatus waterQualityManagementPlanStatusToDelete)
        {
            DeleteWaterQualityManagementPlanStatus(waterQualityManagementPlanStatuses, new List<WaterQualityManagementPlanStatus> { waterQualityManagementPlanStatusToDelete });
        }
    }
}