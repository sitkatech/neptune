//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanVerify GetWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, int waterQualityManagementPlanVerifyID)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifies.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerifyID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerify, "WaterQualityManagementPlanVerify", waterQualityManagementPlanVerifyID);
            return waterQualityManagementPlanVerify;
        }

        public static void DeleteWaterQualityManagementPlanVerify(this List<int> waterQualityManagementPlanVerifyIDList)
        {
            if(waterQualityManagementPlanVerifyIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifies.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifies.Where(x => waterQualityManagementPlanVerifyIDList.Contains(x.WaterQualityManagementPlanVerifyID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerify(this ICollection<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifiesToDelete)
        {
            if(waterQualityManagementPlanVerifiesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifies.RemoveRange(waterQualityManagementPlanVerifiesToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerify(this int waterQualityManagementPlanVerifyID)
        {
            DeleteWaterQualityManagementPlanVerify(new List<int> { waterQualityManagementPlanVerifyID });
        }

        public static void DeleteWaterQualityManagementPlanVerify(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerifyToDelete)
        {
            DeleteWaterQualityManagementPlanVerify(new List<WaterQualityManagementPlanVerify> { waterQualityManagementPlanVerifyToDelete });
        }
    }
}