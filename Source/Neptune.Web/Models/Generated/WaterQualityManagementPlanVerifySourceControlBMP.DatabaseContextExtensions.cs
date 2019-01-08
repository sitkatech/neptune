//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
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
        public static WaterQualityManagementPlanVerifySourceControlBMP GetWaterQualityManagementPlanVerifySourceControlBMP(this IQueryable<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPs, int waterQualityManagementPlanVerifySourceControlBMPID)
        {
            var waterQualityManagementPlanVerifySourceControlBMP = waterQualityManagementPlanVerifySourceControlBMPs.SingleOrDefault(x => x.WaterQualityManagementPlanVerifySourceControlBMPID == waterQualityManagementPlanVerifySourceControlBMPID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifySourceControlBMP, "WaterQualityManagementPlanVerifySourceControlBMP", waterQualityManagementPlanVerifySourceControlBMPID);
            return waterQualityManagementPlanVerifySourceControlBMP;
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this IQueryable<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPs, List<int> waterQualityManagementPlanVerifySourceControlBMPIDList)
        {
            if(waterQualityManagementPlanVerifySourceControlBMPIDList.Any())
            {
                waterQualityManagementPlanVerifySourceControlBMPs.Where(x => waterQualityManagementPlanVerifySourceControlBMPIDList.Contains(x.WaterQualityManagementPlanVerifySourceControlBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this IQueryable<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPs, ICollection<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifySourceControlBMPsToDelete.Any())
            {
                var waterQualityManagementPlanVerifySourceControlBMPIDList = waterQualityManagementPlanVerifySourceControlBMPsToDelete.Select(x => x.WaterQualityManagementPlanVerifySourceControlBMPID).ToList();
                waterQualityManagementPlanVerifySourceControlBMPs.Where(x => waterQualityManagementPlanVerifySourceControlBMPIDList.Contains(x.WaterQualityManagementPlanVerifySourceControlBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this IQueryable<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPs, int waterQualityManagementPlanVerifySourceControlBMPID)
        {
            DeleteWaterQualityManagementPlanVerifySourceControlBMP(waterQualityManagementPlanVerifySourceControlBMPs, new List<int> { waterQualityManagementPlanVerifySourceControlBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this IQueryable<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPs, WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifySourceControlBMP(waterQualityManagementPlanVerifySourceControlBMPs, new List<WaterQualityManagementPlanVerifySourceControlBMP> { waterQualityManagementPlanVerifySourceControlBMPToDelete });
        }
    }
}