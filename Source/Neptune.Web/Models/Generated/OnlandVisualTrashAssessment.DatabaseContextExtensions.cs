//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
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
        public static OnlandVisualTrashAssessment GetOnlandVisualTrashAssessment(this IQueryable<OnlandVisualTrashAssessment> onlandVisualTrashAssessments, int onlandVisualTrashAssessmentID)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessments.SingleOrDefault(x => x.OnlandVisualTrashAssessmentID == onlandVisualTrashAssessmentID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessment, "OnlandVisualTrashAssessment", onlandVisualTrashAssessmentID);
            return onlandVisualTrashAssessment;
        }

        public static void DeleteOnlandVisualTrashAssessment(this IQueryable<OnlandVisualTrashAssessment> onlandVisualTrashAssessments, List<int> onlandVisualTrashAssessmentIDList)
        {
            if(onlandVisualTrashAssessmentIDList.Any())
            {
                onlandVisualTrashAssessments.Where(x => onlandVisualTrashAssessmentIDList.Contains(x.OnlandVisualTrashAssessmentID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessment(this IQueryable<OnlandVisualTrashAssessment> onlandVisualTrashAssessments, ICollection<OnlandVisualTrashAssessment> onlandVisualTrashAssessmentsToDelete)
        {
            if(onlandVisualTrashAssessmentsToDelete.Any())
            {
                var onlandVisualTrashAssessmentIDList = onlandVisualTrashAssessmentsToDelete.Select(x => x.OnlandVisualTrashAssessmentID).ToList();
                onlandVisualTrashAssessments.Where(x => onlandVisualTrashAssessmentIDList.Contains(x.OnlandVisualTrashAssessmentID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessment(this IQueryable<OnlandVisualTrashAssessment> onlandVisualTrashAssessments, int onlandVisualTrashAssessmentID)
        {
            DeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessments, new List<int> { onlandVisualTrashAssessmentID });
        }

        public static void DeleteOnlandVisualTrashAssessment(this IQueryable<OnlandVisualTrashAssessment> onlandVisualTrashAssessments, OnlandVisualTrashAssessment onlandVisualTrashAssessmentToDelete)
        {
            DeleteOnlandVisualTrashAssessment(onlandVisualTrashAssessments, new List<OnlandVisualTrashAssessment> { onlandVisualTrashAssessmentToDelete });
        }
    }
}