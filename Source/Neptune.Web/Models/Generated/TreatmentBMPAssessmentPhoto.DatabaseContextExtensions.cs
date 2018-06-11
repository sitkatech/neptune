//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPAssessmentPhoto GetTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, int treatmentBMPAssessmentPhotoID)
        {
            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotos.SingleOrDefault(x => x.TreatmentBMPAssessmentPhotoID == treatmentBMPAssessmentPhotoID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAssessmentPhoto, "TreatmentBMPAssessmentPhoto", treatmentBMPAssessmentPhotoID);
            return treatmentBMPAssessmentPhoto;
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this List<int> treatmentBMPAssessmentPhotoIDList)
        {
            if(treatmentBMPAssessmentPhotoIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentPhotos.Where(x => treatmentBMPAssessmentPhotoIDList.Contains(x.TreatmentBMPAssessmentPhotoID)));
            }
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this ICollection<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotosToDelete)
        {
            if(treatmentBMPAssessmentPhotosToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.RemoveRange(treatmentBMPAssessmentPhotosToDelete);
            }
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this int treatmentBMPAssessmentPhotoID)
        {
            DeleteTreatmentBMPAssessmentPhoto(new List<int> { treatmentBMPAssessmentPhotoID });
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhotoToDelete)
        {
            DeleteTreatmentBMPAssessmentPhoto(new List<TreatmentBMPAssessmentPhoto> { treatmentBMPAssessmentPhotoToDelete });
        }
    }
}