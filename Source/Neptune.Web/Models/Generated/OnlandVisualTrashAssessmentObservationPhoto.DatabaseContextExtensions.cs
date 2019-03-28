//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhoto]
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
        public static OnlandVisualTrashAssessmentObservationPhoto GetOnlandVisualTrashAssessmentObservationPhoto(this IQueryable<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotos, int onlandVisualTrashAssessmentObservationPhotoID)
        {
            var onlandVisualTrashAssessmentObservationPhoto = onlandVisualTrashAssessmentObservationPhotos.SingleOrDefault(x => x.OnlandVisualTrashAssessmentObservationPhotoID == onlandVisualTrashAssessmentObservationPhotoID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentObservationPhoto, "OnlandVisualTrashAssessmentObservationPhoto", onlandVisualTrashAssessmentObservationPhotoID);
            return onlandVisualTrashAssessmentObservationPhoto;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservationPhoto(this IQueryable<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotos, List<int> onlandVisualTrashAssessmentObservationPhotoIDList)
        {
            if(onlandVisualTrashAssessmentObservationPhotoIDList.Any())
            {
                onlandVisualTrashAssessmentObservationPhotos.Where(x => onlandVisualTrashAssessmentObservationPhotoIDList.Contains(x.OnlandVisualTrashAssessmentObservationPhotoID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservationPhoto(this IQueryable<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotos, ICollection<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotosToDelete)
        {
            if(onlandVisualTrashAssessmentObservationPhotosToDelete.Any())
            {
                var onlandVisualTrashAssessmentObservationPhotoIDList = onlandVisualTrashAssessmentObservationPhotosToDelete.Select(x => x.OnlandVisualTrashAssessmentObservationPhotoID).ToList();
                onlandVisualTrashAssessmentObservationPhotos.Where(x => onlandVisualTrashAssessmentObservationPhotoIDList.Contains(x.OnlandVisualTrashAssessmentObservationPhotoID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentObservationPhoto(this IQueryable<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotos, int onlandVisualTrashAssessmentObservationPhotoID)
        {
            DeleteOnlandVisualTrashAssessmentObservationPhoto(onlandVisualTrashAssessmentObservationPhotos, new List<int> { onlandVisualTrashAssessmentObservationPhotoID });
        }

        public static void DeleteOnlandVisualTrashAssessmentObservationPhoto(this IQueryable<OnlandVisualTrashAssessmentObservationPhoto> onlandVisualTrashAssessmentObservationPhotos, OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhotoToDelete)
        {
            DeleteOnlandVisualTrashAssessmentObservationPhoto(onlandVisualTrashAssessmentObservationPhotos, new List<OnlandVisualTrashAssessmentObservationPhoto> { onlandVisualTrashAssessmentObservationPhotoToDelete });
        }
    }
}