//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
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
        public static OnlandVisualTrashAssessmentPreliminarySourceIdentificationType GetOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(this IQueryable<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, int onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID)
        {
            var onlandVisualTrashAssessmentPreliminarySourceIdentificationType = onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.SingleOrDefault(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID == onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentPreliminarySourceIdentificationType, "OnlandVisualTrashAssessmentPreliminarySourceIdentificationType", onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID);
            return onlandVisualTrashAssessmentPreliminarySourceIdentificationType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(this IQueryable<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, List<int> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeIDList)
        {
            if(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeIDList.Any())
            {
                onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Where(x => onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeIDList.Contains(x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(this IQueryable<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, ICollection<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToDelete)
        {
            if(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToDelete.Any())
            {
                var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeIDList = onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToDelete.Select(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID).ToList();
                onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Where(x => onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeIDList.Contains(x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(this IQueryable<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, int onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID)
        {
            DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, new List<int> { onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID });
        }

        public static void DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(this IQueryable<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeToDelete)
        {
            DeleteOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypes, new List<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> { onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeToDelete });
        }
    }
}