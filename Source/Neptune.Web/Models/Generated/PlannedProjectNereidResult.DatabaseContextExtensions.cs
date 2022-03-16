//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectNereidResult]
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
        public static PlannedProjectNereidResult GetPlannedProjectNereidResult(this IQueryable<PlannedProjectNereidResult> plannedProjectNereidResults, int plannedProjectNereidResultID)
        {
            var plannedProjectNereidResult = plannedProjectNereidResults.SingleOrDefault(x => x.PlannedProjectNereidResultID == plannedProjectNereidResultID);
            Check.RequireNotNullThrowNotFound(plannedProjectNereidResult, "PlannedProjectNereidResult", plannedProjectNereidResultID);
            return plannedProjectNereidResult;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePlannedProjectNereidResult(this IQueryable<PlannedProjectNereidResult> plannedProjectNereidResults, List<int> plannedProjectNereidResultIDList)
        {
            if(plannedProjectNereidResultIDList.Any())
            {
                plannedProjectNereidResults.Where(x => plannedProjectNereidResultIDList.Contains(x.PlannedProjectNereidResultID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePlannedProjectNereidResult(this IQueryable<PlannedProjectNereidResult> plannedProjectNereidResults, ICollection<PlannedProjectNereidResult> plannedProjectNereidResultsToDelete)
        {
            if(plannedProjectNereidResultsToDelete.Any())
            {
                var plannedProjectNereidResultIDList = plannedProjectNereidResultsToDelete.Select(x => x.PlannedProjectNereidResultID).ToList();
                plannedProjectNereidResults.Where(x => plannedProjectNereidResultIDList.Contains(x.PlannedProjectNereidResultID)).Delete();
            }
        }

        public static void DeletePlannedProjectNereidResult(this IQueryable<PlannedProjectNereidResult> plannedProjectNereidResults, int plannedProjectNereidResultID)
        {
            DeletePlannedProjectNereidResult(plannedProjectNereidResults, new List<int> { plannedProjectNereidResultID });
        }

        public static void DeletePlannedProjectNereidResult(this IQueryable<PlannedProjectNereidResult> plannedProjectNereidResults, PlannedProjectNereidResult plannedProjectNereidResultToDelete)
        {
            DeletePlannedProjectNereidResult(plannedProjectNereidResults, new List<PlannedProjectNereidResult> { plannedProjectNereidResultToDelete });
        }
    }
}