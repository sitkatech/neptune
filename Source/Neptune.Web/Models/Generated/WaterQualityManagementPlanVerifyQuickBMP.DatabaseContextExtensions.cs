//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanVerifyQuickBMP GetWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, int waterQualityManagementPlanVerifyQuickBMPID)
        {
            var waterQualityManagementPlanVerifyQuickBMP = waterQualityManagementPlanVerifyQuickBMPs.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyQuickBMPID == waterQualityManagementPlanVerifyQuickBMPID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyQuickBMP, "WaterQualityManagementPlanVerifyQuickBMP", waterQualityManagementPlanVerifyQuickBMPID);
            return waterQualityManagementPlanVerifyQuickBMP;
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this List<int> waterQualityManagementPlanVerifyQuickBMPIDList)
        {
            if(waterQualityManagementPlanVerifyQuickBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyQuickBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyQuickBMPs.Where(x => waterQualityManagementPlanVerifyQuickBMPIDList.Contains(x.WaterQualityManagementPlanVerifyQuickBMPID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this ICollection<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifyQuickBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyQuickBMPs.RemoveRange(waterQualityManagementPlanVerifyQuickBMPsToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this int waterQualityManagementPlanVerifyQuickBMPID)
        {
            DeleteWaterQualityManagementPlanVerifyQuickBMP(new List<int> { waterQualityManagementPlanVerifyQuickBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyQuickBMP(new List<WaterQualityManagementPlanVerifyQuickBMP> { waterQualityManagementPlanVerifyQuickBMPToDelete });
        }
    }
}