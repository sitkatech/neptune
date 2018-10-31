//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this List<int> waterQualityManagementPlanVerifyTreatmentBMPIDList)
        {
            if(waterQualityManagementPlanVerifyTreatmentBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTreatmentBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTreatmentBMPs.Where(x => waterQualityManagementPlanVerifyTreatmentBMPIDList.Contains(x.WaterQualityManagementPlanVerifyTreatmentBMPID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPsToDelete)
        {
            if(waterQualityManagementPlanVerifyTreatmentBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTreatmentBMPs.RemoveRange(waterQualityManagementPlanVerifyTreatmentBMPsToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this int waterQualityManagementPlanVerifyTreatmentBMPID)
        {
            DeleteWaterQualityManagementPlanVerifyTreatmentBMP(new List<int> { waterQualityManagementPlanVerifyTreatmentBMPID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyTreatmentBMP(this WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMPToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyTreatmentBMP(new List<WaterQualityManagementPlanVerifyTreatmentBMP> { waterQualityManagementPlanVerifyTreatmentBMPToDelete });
        }
    }
}