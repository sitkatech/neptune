//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
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
        public static WaterQualityManagementPlanVerify GetWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, int waterQualityManagementPlanVerifyID)
        {
            var waterQualityManagementPlanVerify = waterQualityManagementPlanVerifies.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyID == waterQualityManagementPlanVerifyID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerify, "WaterQualityManagementPlanVerify", waterQualityManagementPlanVerifyID);
            return waterQualityManagementPlanVerify;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, List<int> waterQualityManagementPlanVerifyIDList)
        {
            if(waterQualityManagementPlanVerifyIDList.Any())
            {
                waterQualityManagementPlanVerifies.Where(x => waterQualityManagementPlanVerifyIDList.Contains(x.WaterQualityManagementPlanVerifyID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, ICollection<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifiesToDelete)
        {
            if(waterQualityManagementPlanVerifiesToDelete.Any())
            {
                var waterQualityManagementPlanVerifyIDList = waterQualityManagementPlanVerifiesToDelete.Select(x => x.WaterQualityManagementPlanVerifyID).ToList();
                waterQualityManagementPlanVerifies.Where(x => waterQualityManagementPlanVerifyIDList.Contains(x.WaterQualityManagementPlanVerifyID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, int waterQualityManagementPlanVerifyID)
        {
            DeleteWaterQualityManagementPlanVerify(waterQualityManagementPlanVerifies, new List<int> { waterQualityManagementPlanVerifyID });
        }

        public static void DeleteWaterQualityManagementPlanVerify(this IQueryable<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, WaterQualityManagementPlanVerify waterQualityManagementPlanVerifyToDelete)
        {
            DeleteWaterQualityManagementPlanVerify(waterQualityManagementPlanVerifies, new List<WaterQualityManagementPlanVerify> { waterQualityManagementPlanVerifyToDelete });
        }
    }
}