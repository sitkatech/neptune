//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]
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
        public static WaterQualityManagementPlanVerifyType GetWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, int waterQualityManagementPlanVerifyTypeID)
        {
            var waterQualityManagementPlanVerifyType = waterQualityManagementPlanVerifyTypes.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyTypeID == waterQualityManagementPlanVerifyTypeID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyType, "WaterQualityManagementPlanVerifyType", waterQualityManagementPlanVerifyTypeID);
            return waterQualityManagementPlanVerifyType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, List<int> waterQualityManagementPlanVerifyTypeIDList)
        {
            if(waterQualityManagementPlanVerifyTypeIDList.Any())
            {
                waterQualityManagementPlanVerifyTypes.Where(x => waterQualityManagementPlanVerifyTypeIDList.Contains(x.WaterQualityManagementPlanVerifyTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, ICollection<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypesToDelete)
        {
            if(waterQualityManagementPlanVerifyTypesToDelete.Any())
            {
                var waterQualityManagementPlanVerifyTypeIDList = waterQualityManagementPlanVerifyTypesToDelete.Select(x => x.WaterQualityManagementPlanVerifyTypeID).ToList();
                waterQualityManagementPlanVerifyTypes.Where(x => waterQualityManagementPlanVerifyTypeIDList.Contains(x.WaterQualityManagementPlanVerifyTypeID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, int waterQualityManagementPlanVerifyTypeID)
        {
            DeleteWaterQualityManagementPlanVerifyType(waterQualityManagementPlanVerifyTypes, new List<int> { waterQualityManagementPlanVerifyTypeID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyType(this IQueryable<WaterQualityManagementPlanVerifyType> waterQualityManagementPlanVerifyTypes, WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyTypeToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyType(waterQualityManagementPlanVerifyTypes, new List<WaterQualityManagementPlanVerifyType> { waterQualityManagementPlanVerifyTypeToDelete });
        }
    }
}