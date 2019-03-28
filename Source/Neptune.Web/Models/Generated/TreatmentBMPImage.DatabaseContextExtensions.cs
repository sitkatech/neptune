//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]
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
        public static TreatmentBMPImage GetTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, int treatmentBMPImageID)
        {
            var treatmentBMPImage = treatmentBMPImages.SingleOrDefault(x => x.TreatmentBMPImageID == treatmentBMPImageID);
            Check.RequireNotNullThrowNotFound(treatmentBMPImage, "TreatmentBMPImage", treatmentBMPImageID);
            return treatmentBMPImage;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, List<int> treatmentBMPImageIDList)
        {
            if(treatmentBMPImageIDList.Any())
            {
                treatmentBMPImages.Where(x => treatmentBMPImageIDList.Contains(x.TreatmentBMPImageID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, ICollection<TreatmentBMPImage> treatmentBMPImagesToDelete)
        {
            if(treatmentBMPImagesToDelete.Any())
            {
                var treatmentBMPImageIDList = treatmentBMPImagesToDelete.Select(x => x.TreatmentBMPImageID).ToList();
                treatmentBMPImages.Where(x => treatmentBMPImageIDList.Contains(x.TreatmentBMPImageID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, int treatmentBMPImageID)
        {
            DeleteTreatmentBMPImage(treatmentBMPImages, new List<int> { treatmentBMPImageID });
        }

        public static void DeleteTreatmentBMPImage(this IQueryable<TreatmentBMPImage> treatmentBMPImages, TreatmentBMPImage treatmentBMPImageToDelete)
        {
            DeleteTreatmentBMPImage(treatmentBMPImages, new List<TreatmentBMPImage> { treatmentBMPImageToDelete });
        }
    }
}