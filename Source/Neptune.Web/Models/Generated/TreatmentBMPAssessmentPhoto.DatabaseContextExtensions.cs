//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]
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
        public static TreatmentBMPAssessmentPhoto GetTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, int treatmentBMPAssessmentPhotoID)
        {
            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotos.SingleOrDefault(x => x.TreatmentBMPAssessmentPhotoID == treatmentBMPAssessmentPhotoID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAssessmentPhoto, "TreatmentBMPAssessmentPhoto", treatmentBMPAssessmentPhotoID);
            return treatmentBMPAssessmentPhoto;
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, List<int> treatmentBMPAssessmentPhotoIDList)
        {
            if(treatmentBMPAssessmentPhotoIDList.Any())
            {
                treatmentBMPAssessmentPhotos.Where(x => treatmentBMPAssessmentPhotoIDList.Contains(x.TreatmentBMPAssessmentPhotoID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, ICollection<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotosToDelete)
        {
            if(treatmentBMPAssessmentPhotosToDelete.Any())
            {
                var treatmentBMPAssessmentPhotoIDList = treatmentBMPAssessmentPhotosToDelete.Select(x => x.TreatmentBMPAssessmentPhotoID).ToList();
                treatmentBMPAssessmentPhotos.Where(x => treatmentBMPAssessmentPhotoIDList.Contains(x.TreatmentBMPAssessmentPhotoID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, int treatmentBMPAssessmentPhotoID)
        {
            DeleteTreatmentBMPAssessmentPhoto(treatmentBMPAssessmentPhotos, new List<int> { treatmentBMPAssessmentPhotoID });
        }

        public static void DeleteTreatmentBMPAssessmentPhoto(this IQueryable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos, TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhotoToDelete)
        {
            DeleteTreatmentBMPAssessmentPhoto(treatmentBMPAssessmentPhotos, new List<TreatmentBMPAssessmentPhoto> { treatmentBMPAssessmentPhotoToDelete });
        }
    }
}