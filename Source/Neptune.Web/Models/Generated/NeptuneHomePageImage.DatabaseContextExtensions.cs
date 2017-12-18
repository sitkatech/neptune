//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static NeptuneHomePageImage GetNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, int neptuneHomePageImageID)
        {
            var neptuneHomePageImage = neptuneHomePageImages.SingleOrDefault(x => x.NeptuneHomePageImageID == neptuneHomePageImageID);
            Check.RequireNotNullThrowNotFound(neptuneHomePageImage, "NeptuneHomePageImage", neptuneHomePageImageID);
            return neptuneHomePageImage;
        }

        public static void DeleteNeptuneHomePageImage(this List<int> neptuneHomePageImageIDList)
        {
            if(neptuneHomePageImageIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptuneHomePageImages.RemoveRange(HttpRequestStorage.DatabaseEntities.NeptuneHomePageImages.Where(x => neptuneHomePageImageIDList.Contains(x.NeptuneHomePageImageID)));
            }
        }

        public static void DeleteNeptuneHomePageImage(this ICollection<NeptuneHomePageImage> neptuneHomePageImagesToDelete)
        {
            if(neptuneHomePageImagesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptuneHomePageImages.RemoveRange(neptuneHomePageImagesToDelete);
            }
        }

        public static void DeleteNeptuneHomePageImage(this int neptuneHomePageImageID)
        {
            DeleteNeptuneHomePageImage(new List<int> { neptuneHomePageImageID });
        }

        public static void DeleteNeptuneHomePageImage(this NeptuneHomePageImage neptuneHomePageImageToDelete)
        {
            DeleteNeptuneHomePageImage(new List<NeptuneHomePageImage> { neptuneHomePageImageToDelete });
        }
    }
}