//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]
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
        public static WaterQualityManagementPlanPhoto GetWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, int waterQualityManagementPlanPhotoID)
        {
            var waterQualityManagementPlanPhoto = waterQualityManagementPlanPhotos.SingleOrDefault(x => x.WaterQualityManagementPlanPhotoID == waterQualityManagementPlanPhotoID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanPhoto, "WaterQualityManagementPlanPhoto", waterQualityManagementPlanPhotoID);
            return waterQualityManagementPlanPhoto;
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, List<int> waterQualityManagementPlanPhotoIDList)
        {
            if(waterQualityManagementPlanPhotoIDList.Any())
            {
                waterQualityManagementPlanPhotos.Where(x => waterQualityManagementPlanPhotoIDList.Contains(x.WaterQualityManagementPlanPhotoID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, ICollection<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotosToDelete)
        {
            if(waterQualityManagementPlanPhotosToDelete.Any())
            {
                var waterQualityManagementPlanPhotoIDList = waterQualityManagementPlanPhotosToDelete.Select(x => x.WaterQualityManagementPlanPhotoID).ToList();
                waterQualityManagementPlanPhotos.Where(x => waterQualityManagementPlanPhotoIDList.Contains(x.WaterQualityManagementPlanPhotoID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, int waterQualityManagementPlanPhotoID)
        {
            DeleteWaterQualityManagementPlanPhoto(waterQualityManagementPlanPhotos, new List<int> { waterQualityManagementPlanPhotoID });
        }

        public static void DeleteWaterQualityManagementPlanPhoto(this IQueryable<WaterQualityManagementPlanPhoto> waterQualityManagementPlanPhotos, WaterQualityManagementPlanPhoto waterQualityManagementPlanPhotoToDelete)
        {
            DeleteWaterQualityManagementPlanPhoto(waterQualityManagementPlanPhotos, new List<WaterQualityManagementPlanPhoto> { waterQualityManagementPlanPhotoToDelete });
        }
    }
}