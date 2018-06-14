//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanPriority GetWaterQualityManagementPlanPriority(this IQueryable<WaterQualityManagementPlanPriority> waterQualityManagementPlanPriorities, int waterQualityManagementPlanPriorityID)
        {
            var waterQualityManagementPlanPriority = waterQualityManagementPlanPriorities.SingleOrDefault(x => x.WaterQualityManagementPlanPriorityID == waterQualityManagementPlanPriorityID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanPriority, "WaterQualityManagementPlanPriority", waterQualityManagementPlanPriorityID);
            return waterQualityManagementPlanPriority;
        }

        public static void DeleteWaterQualityManagementPlanPriority(this IQueryable<WaterQualityManagementPlanPriority> waterQualityManagementPlanPriorities, List<int> waterQualityManagementPlanPriorityIDList)
        {
            if(waterQualityManagementPlanPriorityIDList.Any())
            {
                waterQualityManagementPlanPriorities.Where(x => waterQualityManagementPlanPriorityIDList.Contains(x.WaterQualityManagementPlanPriorityID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanPriority(this IQueryable<WaterQualityManagementPlanPriority> waterQualityManagementPlanPriorities, ICollection<WaterQualityManagementPlanPriority> waterQualityManagementPlanPrioritiesToDelete)
        {
            if(waterQualityManagementPlanPrioritiesToDelete.Any())
            {
                var waterQualityManagementPlanPriorityIDList = waterQualityManagementPlanPrioritiesToDelete.Select(x => x.WaterQualityManagementPlanPriorityID).ToList();
                waterQualityManagementPlanPriorities.Where(x => waterQualityManagementPlanPriorityIDList.Contains(x.WaterQualityManagementPlanPriorityID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanPriority(this IQueryable<WaterQualityManagementPlanPriority> waterQualityManagementPlanPriorities, int waterQualityManagementPlanPriorityID)
        {
            DeleteWaterQualityManagementPlanPriority(waterQualityManagementPlanPriorities, new List<int> { waterQualityManagementPlanPriorityID });
        }

        public static void DeleteWaterQualityManagementPlanPriority(this IQueryable<WaterQualityManagementPlanPriority> waterQualityManagementPlanPriorities, WaterQualityManagementPlanPriority waterQualityManagementPlanPriorityToDelete)
        {
            DeleteWaterQualityManagementPlanPriority(waterQualityManagementPlanPriorities, new List<WaterQualityManagementPlanPriority> { waterQualityManagementPlanPriorityToDelete });
        }
    }
}