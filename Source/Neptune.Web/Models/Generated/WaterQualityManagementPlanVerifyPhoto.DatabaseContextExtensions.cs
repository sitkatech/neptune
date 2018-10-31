//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static WaterQualityManagementPlanVerifyPhoto GetWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, int waterQualityManagementPlanVerifyPhotoID)
        {
            var waterQualityManagementPlanVerifyPhoto = waterQualityManagementPlanVerifyPhotos.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyPhotoID == waterQualityManagementPlanVerifyPhotoID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyPhoto, "WaterQualityManagementPlanVerifyPhoto", waterQualityManagementPlanVerifyPhotoID);
            return waterQualityManagementPlanVerifyPhoto;
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this List<int> waterQualityManagementPlanVerifyPhotoIDList)
        {
            if(waterQualityManagementPlanVerifyPhotoIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyPhotos.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanVerifyPhotos.Where(x => waterQualityManagementPlanVerifyPhotoIDList.Contains(x.WaterQualityManagementPlanVerifyPhotoID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this ICollection<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotosToDelete)
        {
            if(waterQualityManagementPlanVerifyPhotosToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyPhotos.RemoveRange(waterQualityManagementPlanVerifyPhotosToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this int waterQualityManagementPlanVerifyPhotoID)
        {
            DeleteWaterQualityManagementPlanVerifyPhoto(new List<int> { waterQualityManagementPlanVerifyPhotoID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhotoToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyPhoto(new List<WaterQualityManagementPlanVerifyPhoto> { waterQualityManagementPlanVerifyPhotoToDelete });
        }
    }
}