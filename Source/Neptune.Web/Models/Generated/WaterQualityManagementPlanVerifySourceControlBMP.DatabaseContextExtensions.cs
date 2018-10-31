//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this List<int> waterQualityManagementPlanVerifySourceControlBMPIDList)
        {
            if(waterQualityManagementPlanVerifySourceControlBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifySourceControlBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifySourceControlBMPs.Where(x => waterQualityManagementPlanVerifySourceControlBMPIDList.Contains(x.WaterQualityManagementPlanVerifySourceControlBMPID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this ICollection<WaterQualityManagementPlanVerifySourceControlBMP> waterQualityManagementPlanVerifySourceControlBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifySourceControlBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifySourceControlBMPs.RemoveRange(waterQualityManagementPlanVerifySourceControlBMPsToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this int waterQualityManagementPlanVerifySourceControlBMPID)
        {
            DeleteWaterQualityManagementPlanVerifySourceControlBMP(new List<int> { waterQualityManagementPlanVerifySourceControlBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifySourceControlBMP(this WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifySourceControlBMP(new List<WaterQualityManagementPlanVerifySourceControlBMP> { waterQualityManagementPlanVerifySourceControlBMPToDelete });
        }
    }
}