//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]
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
        public static OnlandVisualTrashAssessmentObservationPhotoStaging GetOnlandVisualTrashAssessmentObservationPhotoStaging(this IQueryable<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagings, int onlandVisualTrashAssessmentObservationPhotoStagingID)
        {
            var onlandVisualTrashAssessmentObservationPhotoStaging = onlandVisualTrashAssessmentObservationPhotoStagings.SingleOrDefault(x => x.OnlandVisualTrashAssessmentObservationPhotoStagingID == onlandVisualTrashAssessmentObservationPhotoStagingID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentObservationPhotoStaging, "OnlandVisualTrashAssessmentObservationPhotoStaging", onlandVisualTrashAssessmentObservationPhotoStagingID);
            return onlandVisualTrashAssessmentObservationPhotoStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(this IQueryable<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagings, List<int> onlandVisualTrashAssessmentObservationPhotoStagingIDList)
        {
            if(onlandVisualTrashAssessmentObservationPhotoStagingIDList.Any())
            {
                onlandVisualTrashAssessmentObservationPhotoStagings.Where(x => onlandVisualTrashAssessmentObservationPhotoStagingIDList.Contains(x.OnlandVisualTrashAssessmentObservationPhotoStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(this IQueryable<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagings, ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagingsToDelete)
        {
            if(onlandVisualTrashAssessmentObservationPhotoStagingsToDelete.Any())
            {
                var onlandVisualTrashAssessmentObservationPhotoStagingIDList = onlandVisualTrashAssessmentObservationPhotoStagingsToDelete.Select(x => x.OnlandVisualTrashAssessmentObservationPhotoStagingID).ToList();
                onlandVisualTrashAssessmentObservationPhotoStagings.Where(x => onlandVisualTrashAssessmentObservationPhotoStagingIDList.Contains(x.OnlandVisualTrashAssessmentObservationPhotoStagingID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(this IQueryable<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagings, int onlandVisualTrashAssessmentObservationPhotoStagingID)
        {
            DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(onlandVisualTrashAssessmentObservationPhotoStagings, new List<int> { onlandVisualTrashAssessmentObservationPhotoStagingID });
        }

        public static void DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(this IQueryable<OnlandVisualTrashAssessmentObservationPhotoStaging> onlandVisualTrashAssessmentObservationPhotoStagings, OnlandVisualTrashAssessmentObservationPhotoStaging onlandVisualTrashAssessmentObservationPhotoStagingToDelete)
        {
            DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(onlandVisualTrashAssessmentObservationPhotoStagings, new List<OnlandVisualTrashAssessmentObservationPhotoStaging> { onlandVisualTrashAssessmentObservationPhotoStagingToDelete });
        }
    }
}