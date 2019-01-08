//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]
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
        public static NeptunePageImage GetNeptunePageImage(this IQueryable<NeptunePageImage> neptunePageImages, int neptunePageImageID)
        {
            var neptunePageImage = neptunePageImages.SingleOrDefault(x => x.NeptunePageImageID == neptunePageImageID);
            Check.RequireNotNullThrowNotFound(neptunePageImage, "NeptunePageImage", neptunePageImageID);
            return neptunePageImage;
        }

        public static void DeleteNeptunePageImage(this IQueryable<NeptunePageImage> neptunePageImages, List<int> neptunePageImageIDList)
        {
            if(neptunePageImageIDList.Any())
            {
                neptunePageImages.Where(x => neptunePageImageIDList.Contains(x.NeptunePageImageID)).Delete();
            }
        }

        public static void DeleteNeptunePageImage(this IQueryable<NeptunePageImage> neptunePageImages, ICollection<NeptunePageImage> neptunePageImagesToDelete)
        {
            if(neptunePageImagesToDelete.Any())
            {
                var neptunePageImageIDList = neptunePageImagesToDelete.Select(x => x.NeptunePageImageID).ToList();
                neptunePageImages.Where(x => neptunePageImageIDList.Contains(x.NeptunePageImageID)).Delete();
            }
        }

        public static void DeleteNeptunePageImage(this IQueryable<NeptunePageImage> neptunePageImages, int neptunePageImageID)
        {
            DeleteNeptunePageImage(neptunePageImages, new List<int> { neptunePageImageID });
        }

        public static void DeleteNeptunePageImage(this IQueryable<NeptunePageImage> neptunePageImages, NeptunePageImage neptunePageImageToDelete)
        {
            DeleteNeptunePageImage(neptunePageImages, new List<NeptunePageImage> { neptunePageImageToDelete });
        }
    }
}