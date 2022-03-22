//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
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
        public static ProjectNetworkSolveHistory GetProjectNetworkSolveHistory(this IQueryable<ProjectNetworkSolveHistory> projectNetworkSolveHistories, int projectNetworkSolveHistoryID)
        {
            var projectNetworkSolveHistory = projectNetworkSolveHistories.SingleOrDefault(x => x.ProjectNetworkSolveHistoryID == projectNetworkSolveHistoryID);
            Check.RequireNotNullThrowNotFound(projectNetworkSolveHistory, "ProjectNetworkSolveHistory", projectNetworkSolveHistoryID);
            return projectNetworkSolveHistory;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectNetworkSolveHistory(this IQueryable<ProjectNetworkSolveHistory> projectNetworkSolveHistories, List<int> projectNetworkSolveHistoryIDList)
        {
            if(projectNetworkSolveHistoryIDList.Any())
            {
                projectNetworkSolveHistories.Where(x => projectNetworkSolveHistoryIDList.Contains(x.ProjectNetworkSolveHistoryID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectNetworkSolveHistory(this IQueryable<ProjectNetworkSolveHistory> projectNetworkSolveHistories, ICollection<ProjectNetworkSolveHistory> projectNetworkSolveHistoriesToDelete)
        {
            if(projectNetworkSolveHistoriesToDelete.Any())
            {
                var projectNetworkSolveHistoryIDList = projectNetworkSolveHistoriesToDelete.Select(x => x.ProjectNetworkSolveHistoryID).ToList();
                projectNetworkSolveHistories.Where(x => projectNetworkSolveHistoryIDList.Contains(x.ProjectNetworkSolveHistoryID)).Delete();
            }
        }

        public static void DeleteProjectNetworkSolveHistory(this IQueryable<ProjectNetworkSolveHistory> projectNetworkSolveHistories, int projectNetworkSolveHistoryID)
        {
            DeleteProjectNetworkSolveHistory(projectNetworkSolveHistories, new List<int> { projectNetworkSolveHistoryID });
        }

        public static void DeleteProjectNetworkSolveHistory(this IQueryable<ProjectNetworkSolveHistory> projectNetworkSolveHistories, ProjectNetworkSolveHistory projectNetworkSolveHistoryToDelete)
        {
            DeleteProjectNetworkSolveHistory(projectNetworkSolveHistories, new List<ProjectNetworkSolveHistory> { projectNetworkSolveHistoryToDelete });
        }
    }
}