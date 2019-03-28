//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]
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
        public static WaterQualityManagementPlanParcel GetWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, int waterQualityManagementPlanParcelID)
        {
            var waterQualityManagementPlanParcel = waterQualityManagementPlanParcels.SingleOrDefault(x => x.WaterQualityManagementPlanParcelID == waterQualityManagementPlanParcelID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanParcel, "WaterQualityManagementPlanParcel", waterQualityManagementPlanParcelID);
            return waterQualityManagementPlanParcel;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, List<int> waterQualityManagementPlanParcelIDList)
        {
            if(waterQualityManagementPlanParcelIDList.Any())
            {
                waterQualityManagementPlanParcels.Where(x => waterQualityManagementPlanParcelIDList.Contains(x.WaterQualityManagementPlanParcelID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, ICollection<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcelsToDelete)
        {
            if(waterQualityManagementPlanParcelsToDelete.Any())
            {
                var waterQualityManagementPlanParcelIDList = waterQualityManagementPlanParcelsToDelete.Select(x => x.WaterQualityManagementPlanParcelID).ToList();
                waterQualityManagementPlanParcels.Where(x => waterQualityManagementPlanParcelIDList.Contains(x.WaterQualityManagementPlanParcelID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, int waterQualityManagementPlanParcelID)
        {
            DeleteWaterQualityManagementPlanParcel(waterQualityManagementPlanParcels, new List<int> { waterQualityManagementPlanParcelID });
        }

        public static void DeleteWaterQualityManagementPlanParcel(this IQueryable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, WaterQualityManagementPlanParcel waterQualityManagementPlanParcelToDelete)
        {
            DeleteWaterQualityManagementPlanParcel(waterQualityManagementPlanParcels, new List<WaterQualityManagementPlanParcel> { waterQualityManagementPlanParcelToDelete });
        }
    }
}