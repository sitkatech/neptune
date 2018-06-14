//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanLandUse]
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
        public static WaterQualityManagementPlanLandUse GetWaterQualityManagementPlanLandUse(this IQueryable<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUses, int waterQualityManagementPlanLandUseID)
        {
            var waterQualityManagementPlanLandUse = waterQualityManagementPlanLandUses.SingleOrDefault(x => x.WaterQualityManagementPlanLandUseID == waterQualityManagementPlanLandUseID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanLandUse, "WaterQualityManagementPlanLandUse", waterQualityManagementPlanLandUseID);
            return waterQualityManagementPlanLandUse;
        }

        public static void DeleteWaterQualityManagementPlanLandUse(this IQueryable<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUses, List<int> waterQualityManagementPlanLandUseIDList)
        {
            if(waterQualityManagementPlanLandUseIDList.Any())
            {
                waterQualityManagementPlanLandUses.Where(x => waterQualityManagementPlanLandUseIDList.Contains(x.WaterQualityManagementPlanLandUseID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanLandUse(this IQueryable<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUses, ICollection<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUsesToDelete)
        {
            if(waterQualityManagementPlanLandUsesToDelete.Any())
            {
                var waterQualityManagementPlanLandUseIDList = waterQualityManagementPlanLandUsesToDelete.Select(x => x.WaterQualityManagementPlanLandUseID).ToList();
                waterQualityManagementPlanLandUses.Where(x => waterQualityManagementPlanLandUseIDList.Contains(x.WaterQualityManagementPlanLandUseID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanLandUse(this IQueryable<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUses, int waterQualityManagementPlanLandUseID)
        {
            DeleteWaterQualityManagementPlanLandUse(waterQualityManagementPlanLandUses, new List<int> { waterQualityManagementPlanLandUseID });
        }

        public static void DeleteWaterQualityManagementPlanLandUse(this IQueryable<WaterQualityManagementPlanLandUse> waterQualityManagementPlanLandUses, WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUseToDelete)
        {
            DeleteWaterQualityManagementPlanLandUse(waterQualityManagementPlanLandUses, new List<WaterQualityManagementPlanLandUse> { waterQualityManagementPlanLandUseToDelete });
        }
    }
}