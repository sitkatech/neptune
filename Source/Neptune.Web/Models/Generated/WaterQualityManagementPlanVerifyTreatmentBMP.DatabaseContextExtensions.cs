//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
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
        public static WaterQualityManagementPlanVerifyTreatmentBMP GetWaterQualityManagementPlanVerifyTreatmentBMP(this IQueryable<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, int waterQualityManagementPlanVerifyTreatmentBMPID)
        {
            var waterQualityManagementPlanVerifyTreatmentBMP = waterQualityManagementPlanVerifyTreatmentBMPs.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyTreatmentBMPID == waterQualityManagementPlanVerifyTreatmentBMPID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyTreatmentBMP, "WaterQualityManagementPlanVerifyTreatmentBMP", waterQualityManagementPlanVerifyTreatmentBMPID);
            return waterQualityManagementPlanVerifyTreatmentBMP;
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this IQueryable<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, List<int> waterQualityManagementPlanVerifyTreatmentBMPIDList)
        {
            if(waterQualityManagementPlanVerifyTreatmentBMPIDList.Any())
            {
                waterQualityManagementPlanVerifyTreatmentBMPs.Where(x => waterQualityManagementPlanVerifyTreatmentBMPIDList.Contains(x.WaterQualityManagementPlanVerifyTreatmentBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this IQueryable<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifyTreatmentBMPsToDelete.Any())
            {
                var waterQualityManagementPlanVerifyTreatmentBMPIDList = waterQualityManagementPlanVerifyTreatmentBMPsToDelete.Select(x => x.WaterQualityManagementPlanVerifyTreatmentBMPID).ToList();
                waterQualityManagementPlanVerifyTreatmentBMPs.Where(x => waterQualityManagementPlanVerifyTreatmentBMPIDList.Contains(x.WaterQualityManagementPlanVerifyTreatmentBMPID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this IQueryable<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, int waterQualityManagementPlanVerifyTreatmentBMPID)
        {
            DeleteWaterQualityManagementPlanVerifyTreatmentBMP(waterQualityManagementPlanVerifyTreatmentBMPs, new List<int> { waterQualityManagementPlanVerifyTreatmentBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this IQueryable<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyTreatmentBMP(waterQualityManagementPlanVerifyTreatmentBMPs, new List<WaterQualityManagementPlanVerifyTreatmentBMP> { waterQualityManagementPlanVerifyTreatmentBMPToDelete });
        }
    }
}