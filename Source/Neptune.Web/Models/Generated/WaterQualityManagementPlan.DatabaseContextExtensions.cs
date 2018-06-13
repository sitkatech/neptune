//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlan GetWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, int waterQualityManagementPlanID)
        {
            var waterQualityManagementPlan = waterQualityManagementPlans.SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlan, "WaterQualityManagementPlan", waterQualityManagementPlanID);
            return waterQualityManagementPlan;
        }

        public static void DeleteWaterQualityManagementPlan(this List<int> waterQualityManagementPlanIDList)
        {
            if(waterQualityManagementPlanIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlans.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Where(x => waterQualityManagementPlanIDList.Contains(x.WaterQualityManagementPlanID)));
            }
        }

        public static void DeleteWaterQualityManagementPlan(this ICollection<WaterQualityManagementPlan> waterQualityManagementPlansToDelete)
        {
            if(waterQualityManagementPlansToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlans.RemoveRange(waterQualityManagementPlansToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlan(this int waterQualityManagementPlanID)
        {
            DeleteWaterQualityManagementPlan(new List<int> { waterQualityManagementPlanID });
        }

        public static void DeleteWaterQualityManagementPlan(this WaterQualityManagementPlan waterQualityManagementPlanToDelete)
        {
            DeleteWaterQualityManagementPlan(new List<WaterQualityManagementPlan> { waterQualityManagementPlanToDelete });
        }
    }
}