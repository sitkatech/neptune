//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]
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
        public static NeptuneHomePageImage GetNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, int neptuneHomePageImageID)
        {
            var neptuneHomePageImage = neptuneHomePageImages.SingleOrDefault(x => x.NeptuneHomePageImageID == neptuneHomePageImageID);
            Check.RequireNotNullThrowNotFound(neptuneHomePageImage, "NeptuneHomePageImage", neptuneHomePageImageID);
            return neptuneHomePageImage;
        }

        public static void DeleteNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, List<int> neptuneHomePageImageIDList)
        {
            if(neptuneHomePageImageIDList.Any())
            {
                neptuneHomePageImages.Where(x => neptuneHomePageImageIDList.Contains(x.NeptuneHomePageImageID)).Delete();
            }
        }

        public static void DeleteNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, ICollection<NeptuneHomePageImage> neptuneHomePageImagesToDelete)
        {
            if(neptuneHomePageImagesToDelete.Any())
            {
                var neptuneHomePageImageIDList = neptuneHomePageImagesToDelete.Select(x => x.NeptuneHomePageImageID).ToList();
                neptuneHomePageImages.Where(x => neptuneHomePageImageIDList.Contains(x.NeptuneHomePageImageID)).Delete();
            }
        }

        public static void DeleteNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, int neptuneHomePageImageID)
        {
            DeleteNeptuneHomePageImage(neptuneHomePageImages, new List<int> { neptuneHomePageImageID });
        }

        public static void DeleteNeptuneHomePageImage(this IQueryable<NeptuneHomePageImage> neptuneHomePageImages, NeptuneHomePageImage neptuneHomePageImageToDelete)
        {
            DeleteNeptuneHomePageImage(neptuneHomePageImages, new List<NeptuneHomePageImage> { neptuneHomePageImageToDelete });
        }
    }
}