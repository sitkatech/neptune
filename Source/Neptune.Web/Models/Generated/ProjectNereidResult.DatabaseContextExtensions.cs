//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]
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
        public static ProjectNereidResult GetProjectNereidResult(this IQueryable<ProjectNereidResult> projectNereidResults, int projectNereidResultID)
        {
            var projectNereidResult = projectNereidResults.SingleOrDefault(x => x.ProjectNereidResultID == projectNereidResultID);
            Check.RequireNotNullThrowNotFound(projectNereidResult, "ProjectNereidResult", projectNereidResultID);
            return projectNereidResult;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectNereidResult(this IQueryable<ProjectNereidResult> projectNereidResults, List<int> projectNereidResultIDList)
        {
            if(projectNereidResultIDList.Any())
            {
                projectNereidResults.Where(x => projectNereidResultIDList.Contains(x.ProjectNereidResultID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectNereidResult(this IQueryable<ProjectNereidResult> projectNereidResults, ICollection<ProjectNereidResult> projectNereidResultsToDelete)
        {
            if(projectNereidResultsToDelete.Any())
            {
                var projectNereidResultIDList = projectNereidResultsToDelete.Select(x => x.ProjectNereidResultID).ToList();
                projectNereidResults.Where(x => projectNereidResultIDList.Contains(x.ProjectNereidResultID)).Delete();
            }
        }

        public static void DeleteProjectNereidResult(this IQueryable<ProjectNereidResult> projectNereidResults, int projectNereidResultID)
        {
            DeleteProjectNereidResult(projectNereidResults, new List<int> { projectNereidResultID });
        }

        public static void DeleteProjectNereidResult(this IQueryable<ProjectNereidResult> projectNereidResults, ProjectNereidResult projectNereidResultToDelete)
        {
            DeleteProjectNereidResult(projectNereidResults, new List<ProjectNereidResult> { projectNereidResultToDelete });
        }
    }
}