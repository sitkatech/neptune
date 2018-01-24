//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPPhoto]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPPhoto GetTreatmentBMPPhoto(this IQueryable<TreatmentBMPPhoto> treatmentBMPPhotos, int treatmentBMPPhotoID)
        {
            var treatmentBMPPhoto = treatmentBMPPhotos.SingleOrDefault(x => x.TreatmentBMPPhotoID == treatmentBMPPhotoID);
            Check.RequireNotNullThrowNotFound(treatmentBMPPhoto, "TreatmentBMPPhoto", treatmentBMPPhotoID);
            return treatmentBMPPhoto;
        }

        public static void DeleteTreatmentBMPPhoto(this List<int> treatmentBMPPhotoIDList)
        {
            if(treatmentBMPPhotoIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPPhotos.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPPhotos.Where(x => treatmentBMPPhotoIDList.Contains(x.TreatmentBMPPhotoID)));
            }
        }

        public static void DeleteTreatmentBMPPhoto(this ICollection<TreatmentBMPPhoto> treatmentBMPPhotosToDelete)
        {
            if(treatmentBMPPhotosToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPPhotos.RemoveRange(treatmentBMPPhotosToDelete);
            }
        }

        public static void DeleteTreatmentBMPPhoto(this int treatmentBMPPhotoID)
        {
            DeleteTreatmentBMPPhoto(new List<int> { treatmentBMPPhotoID });
        }

        public static void DeleteTreatmentBMPPhoto(this TreatmentBMPPhoto treatmentBMPPhotoToDelete)
        {
            DeleteTreatmentBMPPhoto(new List<TreatmentBMPPhoto> { treatmentBMPPhotoToDelete });
        }
    }
}