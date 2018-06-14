//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]
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
        public static WaterQualityManagementPlanDevelopmentType GetWaterQualityManagementPlanDevelopmentType(this IQueryable<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypes, int waterQualityManagementPlanDevelopmentTypeID)
        {
            var waterQualityManagementPlanDevelopmentType = waterQualityManagementPlanDevelopmentTypes.SingleOrDefault(x => x.WaterQualityManagementPlanDevelopmentTypeID == waterQualityManagementPlanDevelopmentTypeID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanDevelopmentType, "WaterQualityManagementPlanDevelopmentType", waterQualityManagementPlanDevelopmentTypeID);
            return waterQualityManagementPlanDevelopmentType;
        }

        public static void DeleteWaterQualityManagementPlanDevelopmentType(this IQueryable<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypes, List<int> waterQualityManagementPlanDevelopmentTypeIDList)
        {
            if(waterQualityManagementPlanDevelopmentTypeIDList.Any())
            {
                waterQualityManagementPlanDevelopmentTypes.Where(x => waterQualityManagementPlanDevelopmentTypeIDList.Contains(x.WaterQualityManagementPlanDevelopmentTypeID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanDevelopmentType(this IQueryable<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypes, ICollection<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypesToDelete)
        {
            if(waterQualityManagementPlanDevelopmentTypesToDelete.Any())
            {
                var waterQualityManagementPlanDevelopmentTypeIDList = waterQualityManagementPlanDevelopmentTypesToDelete.Select(x => x.WaterQualityManagementPlanDevelopmentTypeID).ToList();
                waterQualityManagementPlanDevelopmentTypes.Where(x => waterQualityManagementPlanDevelopmentTypeIDList.Contains(x.WaterQualityManagementPlanDevelopmentTypeID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanDevelopmentType(this IQueryable<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypes, int waterQualityManagementPlanDevelopmentTypeID)
        {
            DeleteWaterQualityManagementPlanDevelopmentType(waterQualityManagementPlanDevelopmentTypes, new List<int> { waterQualityManagementPlanDevelopmentTypeID });
        }

        public static void DeleteWaterQualityManagementPlanDevelopmentType(this IQueryable<WaterQualityManagementPlanDevelopmentType> waterQualityManagementPlanDevelopmentTypes, WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentTypeToDelete)
        {
            DeleteWaterQualityManagementPlanDevelopmentType(waterQualityManagementPlanDevelopmentTypes, new List<WaterQualityManagementPlanDevelopmentType> { waterQualityManagementPlanDevelopmentTypeToDelete });
        }
    }
}