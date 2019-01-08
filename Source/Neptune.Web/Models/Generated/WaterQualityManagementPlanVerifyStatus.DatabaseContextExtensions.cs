//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]
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
        public static WaterQualityManagementPlanVerifyStatus GetWaterQualityManagementPlanVerifyStatus(this IQueryable<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatuses, int waterQualityManagementPlanVerifyStatusID)
        {
            var waterQualityManagementPlanVerifyStatus = waterQualityManagementPlanVerifyStatuses.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyStatusID == waterQualityManagementPlanVerifyStatusID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyStatus, "WaterQualityManagementPlanVerifyStatus", waterQualityManagementPlanVerifyStatusID);
            return waterQualityManagementPlanVerifyStatus;
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this IQueryable<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatuses, List<int> waterQualityManagementPlanVerifyStatusIDList)
        {
            if(waterQualityManagementPlanVerifyStatusIDList.Any())
            {
                waterQualityManagementPlanVerifyStatuses.Where(x => waterQualityManagementPlanVerifyStatusIDList.Contains(x.WaterQualityManagementPlanVerifyStatusID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this IQueryable<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatuses, ICollection<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatusesToDelete)
        {
            if(waterQualityManagementPlanVerifyStatusesToDelete.Any())
            {
                var waterQualityManagementPlanVerifyStatusIDList = waterQualityManagementPlanVerifyStatusesToDelete.Select(x => x.WaterQualityManagementPlanVerifyStatusID).ToList();
                waterQualityManagementPlanVerifyStatuses.Where(x => waterQualityManagementPlanVerifyStatusIDList.Contains(x.WaterQualityManagementPlanVerifyStatusID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this IQueryable<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatuses, int waterQualityManagementPlanVerifyStatusID)
        {
            DeleteWaterQualityManagementPlanVerifyStatus(waterQualityManagementPlanVerifyStatuses, new List<int> { waterQualityManagementPlanVerifyStatusID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this IQueryable<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatuses, WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatusToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyStatus(waterQualityManagementPlanVerifyStatuses, new List<WaterQualityManagementPlanVerifyStatus> { waterQualityManagementPlanVerifyStatusToDelete });
        }
    }
}