//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteNeptunePageImage(this List<int> neptunePageImageIDList)
        {
            if(neptunePageImageIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptunePageImages.RemoveRange(HttpRequestStorage.DatabaseEntities.NeptunePageImages.Where(x => neptunePageImageIDList.Contains(x.NeptunePageImageID)));
            }
        }

        public static void DeleteNeptunePageImage(this ICollection<NeptunePageImage> neptunePageImagesToDelete)
        {
            if(neptunePageImagesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllNeptunePageImages.RemoveRange(neptunePageImagesToDelete);
            }
        }

        public static void DeleteNeptunePageImage(this int neptunePageImageID)
        {
            DeleteNeptunePageImage(new List<int> { neptunePageImageID });
        }

        public static void DeleteNeptunePageImage(this NeptunePageImage neptunePageImageToDelete)
        {
            DeleteNeptunePageImage(new List<NeptunePageImage> { neptunePageImageToDelete });
        }
    }
}