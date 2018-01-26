//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPImage GetTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, int treatmentBMPImageID)
        {
            var treatmentBMPImage = treatmentBMPImages.SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
            Check.RequireNotNullThrowNotFound(treatmentBMPImage, "TreatmentBMPImage", treatmentBMPImageID);
            return treatmentBMPImage;
        }

        public static void DeleteTreatmentBMPImage(this List<int> treatmentBMPImageIDList)
        {
            if(treatmentBMPImageIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPImages.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPImages.Where(x => treatmentBMPImageIDList.Contains(x.TreatmentBMPImageID)));
            }
        }

        public static void DeleteTreatmentBMPImage(this ICollection<TreatmentBMPImage> treatmentBMPImagesToDelete)
        {
            if(treatmentBMPImagesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPImages.RemoveRange(treatmentBMPImagesToDelete);
            }
        }

        public static void DeleteTreatmentBMPImage(this int treatmentBMPImageID)
        {
            DeleteTreatmentBMPImage(new List<int> { treatmentBMPImageID });
        }

        public static void DeleteTreatmentBMPImage(this TreatmentBMPImage treatmentBMPImageToDelete)
        {
            DeleteTreatmentBMPImage(new List<TreatmentBMPImage> { treatmentBMPImageToDelete });
        }
    }
}