//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this List<int> waterQualityManagementPlanVerifyStatusIDList)
        {
            if(waterQualityManagementPlanVerifyStatusIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyStatuses.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyStatuses.Where(x => waterQualityManagementPlanVerifyStatusIDList.Contains(x.WaterQualityManagementPlanVerifyStatusID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this ICollection<WaterQualityManagementPlanVerifyStatus> waterQualityManagementPlanVerifyStatusesToDelete)
        {
            if(waterQualityManagementPlanVerifyStatusesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyStatuses.RemoveRange(waterQualityManagementPlanVerifyStatusesToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this int waterQualityManagementPlanVerifyStatusID)
        {
            DeleteWaterQualityManagementPlanVerifyStatus(new List<int> { waterQualityManagementPlanVerifyStatusID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyStatus(this WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatusToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyStatus(new List<WaterQualityManagementPlanVerifyStatus> { waterQualityManagementPlanVerifyStatusToDelete });
        }
    }
}