//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanPhoto GetWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, int waterQualityManagementPlanPhotoID)
        {
            var waterQualityManagementPlanPhoto = waterQualityManagementPlanPhotos.SingleOrDefault(x => x.WaterQualityManagementPlanPhotoID == waterQualityManagementPlanPhotoID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanPhoto, "WaterQualityManagementPlanPhoto", waterQualityManagementPlanPhotoID);
            return waterQualityManagementPlanPhoto;
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this List<int> waterQualityManagementPlanPhotoIDList)
        {
            if(waterQualityManagementPlanPhotoIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanPhotos.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanPhotos.Where(x => waterQualityManagementPlanPhotoIDList.Contains(x.WaterQualityManagementPlanPhotoID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this ICollection<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotosToDelete)
        {
            if(waterQualityManagementPlanPhotosToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanPhotos.RemoveRange(waterQualityManagementPlanPhotosToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this int waterQualityManagementPlanPhotoID)
        {
            DeleteWaterQualityManagementPlanPhoto(new List<int> { waterQualityManagementPlanPhotoID });
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this WaterQualityManagementPlanPhoto waterQualityManagementPlanPhotoToDelete)
        {
            DeleteWaterQualityManagementPlanPhoto(new List<WaterQualityManagementPlanPhoto> { waterQualityManagementPlanPhotoToDelete });
        }
    }
}