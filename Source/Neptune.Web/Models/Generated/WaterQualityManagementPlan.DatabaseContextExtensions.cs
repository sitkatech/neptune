//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
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
        public static WaterQualityManagementPlan GetWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, int waterQualityManagementPlanID)
        {
            var waterQualityManagementPlan = waterQualityManagementPlans.SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlan, "WaterQualityManagementPlan", waterQualityManagementPlanID);
            return waterQualityManagementPlan;
        }

        public static void DeleteWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, List<int> waterQualityManagementPlanIDList)
        {
            if(waterQualityManagementPlanIDList.Any())
            {
                waterQualityManagementPlans.Where(x => waterQualityManagementPlanIDList.Contains(x.WaterQualityManagementPlanID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, ICollection<WaterQualityManagementPlan> waterQualityManagementPlansToDelete)
        {
            if(waterQualityManagementPlansToDelete.Any())
            {
                var waterQualityManagementPlanIDList = waterQualityManagementPlansToDelete.Select(x => x.WaterQualityManagementPlanID).ToList();
                waterQualityManagementPlans.Where(x => waterQualityManagementPlanIDList.Contains(x.WaterQualityManagementPlanID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, int waterQualityManagementPlanID)
        {
            DeleteWaterQualityManagementPlan(waterQualityManagementPlans, new List<int> { waterQualityManagementPlanID });
        }

        public static void DeleteWaterQualityManagementPlan(this IQueryable<WaterQualityManagementPlan> waterQualityManagementPlans, WaterQualityManagementPlan waterQualityManagementPlanToDelete)
        {
            DeleteWaterQualityManagementPlan(waterQualityManagementPlans, new List<WaterQualityManagementPlan> { waterQualityManagementPlanToDelete });
        }
    }
}