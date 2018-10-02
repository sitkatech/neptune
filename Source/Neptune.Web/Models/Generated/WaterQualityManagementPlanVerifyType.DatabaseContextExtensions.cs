//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanVerifyType GetWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, int waterQualityManagementPlanVerifyTypeID)
        {
            var waterQualityManagementPlanVerifyType = waterQualityManagementPlanVerifyTypes.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyTypeID == waterQualityManagementPlanVerifyTypeID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyType, "WaterQualityManagementPlanVerifyType", waterQualityManagementPlanVerifyTypeID);
            return waterQualityManagementPlanVerifyType;
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this List<int> waterQualityManagementPlanVerifyTypeIDList)
        {
            if(waterQualityManagementPlanVerifyTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyTypes.Where(x => waterQualityManagementPlanVerifyTypeIDList.Contains(x.WaterQualityManagementPlanVerifyTypeID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this ICollection<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypesToDelete)
        {
            if(waterQualityManagementPlanVerifyTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTypes.RemoveRange(waterQualityManagementPlanVerifyTypesToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this int waterQualityManagementPlanVerifyTypeID)
        {
            DeleteWaterQualityManagementPlanVerifyType(new List<int> { waterQualityManagementPlanVerifyTypeID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyTypeToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyType(new List<WaterQualityManagementPlanVerifyType> { waterQualityManagementPlanVerifyTypeToDelete });
        }
    }
}