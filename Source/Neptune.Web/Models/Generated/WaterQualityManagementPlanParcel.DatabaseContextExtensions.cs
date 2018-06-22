//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanParcel GetWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, int waterQualityManagementPlanParcelID)
        {
            var waterQualityManagementPlanParcel = waterQualityManagementPlanParcels.SingleOrDefault(x => x.WaterQualityManagementPlanParcelID == waterQualityManagementPlanParcelID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanParcel, "WaterQualityManagementPlanParcel", waterQualityManagementPlanParcelID);
            return waterQualityManagementPlanParcel;
        }

        public static void DeleteWaterQualityManagementPlanParcel(this List<int> waterQualityManagementPlanParcelIDList)
        {
            if(waterQualityManagementPlanParcelIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanParcels.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanParcels.Where(x => waterQualityManagementPlanParcelIDList.Contains(x.WaterQualityManagementPlanParcelID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanParcel(this ICollection<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcelsToDelete)
        {
            if(waterQualityManagementPlanParcelsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanParcels.RemoveRange(waterQualityManagementPlanParcelsToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanParcel(this int waterQualityManagementPlanParcelID)
        {
            DeleteWaterQualityManagementPlanParcel(new List<int> { waterQualityManagementPlanParcelID });
        }

        public static void DeleteWaterQualityManagementPlanParcel(this WaterQualityManagementPlanParcel waterQualityManagementPlanParcelToDelete)
        {
            DeleteWaterQualityManagementPlanParcel(new List<WaterQualityManagementPlanParcel> { waterQualityManagementPlanParcelToDelete });
        }
    }
}