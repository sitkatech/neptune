//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
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
        public static WaterQualityManagementPlanVerifyQuickBMP GetWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, int waterQualityManagementPlanVerifyQuickBMPID)
        {
            var waterQualityManagementPlanVerifyQuickBMP = waterQualityManagementPlanVerifyQuickBMPs.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyQuickBMPID == waterQualityManagementPlanVerifyQuickBMPID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyQuickBMP, "WaterQualityManagementPlanVerifyQuickBMP", waterQualityManagementPlanVerifyQuickBMPID);
            return waterQualityManagementPlanVerifyQuickBMP;
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<int> waterQualityManagementPlanVerifyQuickBMPIDList)
        {
            if(waterQualityManagementPlanVerifyQuickBMPIDList.Any())
            {
                waterQualityManagementPlanVerifyQuickBMPs.Where(x => waterQualityManagementPlanVerifyQuickBMPIDList.Contains(x.WaterQualityManagementPlanVerifyQuickBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, ICollection<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifyQuickBMPsToDelete.Any())
            {
                var waterQualityManagementPlanVerifyQuickBMPIDList = waterQualityManagementPlanVerifyQuickBMPsToDelete.Select(x => x.WaterQualityManagementPlanVerifyQuickBMPID).ToList();
                waterQualityManagementPlanVerifyQuickBMPs.Where(x => waterQualityManagementPlanVerifyQuickBMPIDList.Contains(x.WaterQualityManagementPlanVerifyQuickBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, int waterQualityManagementPlanVerifyQuickBMPID)
        {
            DeleteWaterQualityManagementPlanVerifyQuickBMP(waterQualityManagementPlanVerifyQuickBMPs, new List<int> { waterQualityManagementPlanVerifyQuickBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyQuickBMP(this IQueryable<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyQuickBMP(waterQualityManagementPlanVerifyQuickBMPs, new List<WaterQualityManagementPlanVerifyQuickBMP> { waterQualityManagementPlanVerifyQuickBMPToDelete });
        }
    }
}