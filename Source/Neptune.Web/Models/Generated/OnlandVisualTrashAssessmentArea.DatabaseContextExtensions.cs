//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]
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
        public static OnlandVisualTrashAssessmentArea GetOnlandVisualTrashAssessmentArea(this IQueryable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, int onlandVisualTrashAssessmentAreaID)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreas.SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
            Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentArea, "OnlandVisualTrashAssessmentArea", onlandVisualTrashAssessmentAreaID);
            return onlandVisualTrashAssessmentArea;
        }

        public static void DeleteOnlandVisualTrashAssessmentArea(this IQueryable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, List<int> onlandVisualTrashAssessmentAreaIDList)
        {
            if(onlandVisualTrashAssessmentAreaIDList.Any())
            {
                onlandVisualTrashAssessmentAreas.Where(x => onlandVisualTrashAssessmentAreaIDList.Contains(x.OnlandVisualTrashAssessmentAreaID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentArea(this IQueryable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, ICollection<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreasToDelete)
        {
            if(onlandVisualTrashAssessmentAreasToDelete.Any())
            {
                var onlandVisualTrashAssessmentAreaIDList = onlandVisualTrashAssessmentAreasToDelete.Select(x => x.OnlandVisualTrashAssessmentAreaID).ToList();
                onlandVisualTrashAssessmentAreas.Where(x => onlandVisualTrashAssessmentAreaIDList.Contains(x.OnlandVisualTrashAssessmentAreaID)).Delete();
            }
        }

        public static void DeleteOnlandVisualTrashAssessmentArea(this IQueryable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, int onlandVisualTrashAssessmentAreaID)
        {
            DeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentAreas, new List<int> { onlandVisualTrashAssessmentAreaID });
        }

        public static void DeleteOnlandVisualTrashAssessmentArea(this IQueryable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentAreaToDelete)
        {
            DeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentAreas, new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentAreaToDelete });
        }
    }
}