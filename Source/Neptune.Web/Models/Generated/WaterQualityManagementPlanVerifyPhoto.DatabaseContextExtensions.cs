//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
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
        public static WaterQualityManagementPlanVerifyPhoto GetWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, int waterQualityManagementPlanVerifyPhotoID)
        {
            var waterQualityManagementPlanVerifyPhoto = waterQualityManagementPlanVerifyPhotos.SingleOrDefault(x => x.WaterQualityManagementPlanVerifyPhotoID == waterQualityManagementPlanVerifyPhotoID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanVerifyPhoto, "WaterQualityManagementPlanVerifyPhoto", waterQualityManagementPlanVerifyPhotoID);
            return waterQualityManagementPlanVerifyPhoto;
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, List<int> waterQualityManagementPlanVerifyPhotoIDList)
        {
            if(waterQualityManagementPlanVerifyPhotoIDList.Any())
            {
                waterQualityManagementPlanVerifyPhotos.Where(x => waterQualityManagementPlanVerifyPhotoIDList.Contains(x.WaterQualityManagementPlanVerifyPhotoID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, ICollection<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotosToDelete)
        {
            if(waterQualityManagementPlanVerifyPhotosToDelete.Any())
            {
                var waterQualityManagementPlanVerifyPhotoIDList = waterQualityManagementPlanVerifyPhotosToDelete.Select(x => x.WaterQualityManagementPlanVerifyPhotoID).ToList();
                waterQualityManagementPlanVerifyPhotos.Where(x => waterQualityManagementPlanVerifyPhotoIDList.Contains(x.WaterQualityManagementPlanVerifyPhotoID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, int waterQualityManagementPlanVerifyPhotoID)
        {
            DeleteWaterQualityManagementPlanVerifyPhoto(waterQualityManagementPlanVerifyPhotos, new List<int> { waterQualityManagementPlanVerifyPhotoID });
        }

        public static void DeleteWaterQualityManagementPlanVerifyPhoto(this IQueryable<WaterQualityManagementPlanVerifyPhoto> waterQualityManagementPlanVerifyPhotos, WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhotoToDelete)
        {
            DeleteWaterQualityManagementPlanVerifyPhoto(waterQualityManagementPlanVerifyPhotos, new List<WaterQualityManagementPlanVerifyPhoto> { waterQualityManagementPlanVerifyPhotoToDelete });
        }
    }
}