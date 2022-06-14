//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistoryStatusType]
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
        public static ProjectNetworkSolveHistoryStatusType GetProjectNetworkSolveHistoryStatusType(this IQueryable<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypes, int projectNetworkSolveHistoryStatusTypeID)
        {
            var projectNetworkSolveHistoryStatusType = projectNetworkSolveHistoryStatusTypes.SingleOrDefault(x => x.ProjectNetworkSolveHistoryStatusTypeID == projectNetworkSolveHistoryStatusTypeID);
            Check.RequireNotNullThrowNotFound(projectNetworkSolveHistoryStatusType, "ProjectNetworkSolveHistoryStatusType", projectNetworkSolveHistoryStatusTypeID);
            return projectNetworkSolveHistoryStatusType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectNetworkSolveHistoryStatusType(this IQueryable<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypes, List<int> projectNetworkSolveHistoryStatusTypeIDList)
        {
            if(projectNetworkSolveHistoryStatusTypeIDList.Any())
            {
                projectNetworkSolveHistoryStatusTypes.Where(x => projectNetworkSolveHistoryStatusTypeIDList.Contains(x.ProjectNetworkSolveHistoryStatusTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectNetworkSolveHistoryStatusType(this IQueryable<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypes, ICollection<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypesToDelete)
        {
            if(projectNetworkSolveHistoryStatusTypesToDelete.Any())
            {
                var projectNetworkSolveHistoryStatusTypeIDList = projectNetworkSolveHistoryStatusTypesToDelete.Select(x => x.ProjectNetworkSolveHistoryStatusTypeID).ToList();
                projectNetworkSolveHistoryStatusTypes.Where(x => projectNetworkSolveHistoryStatusTypeIDList.Contains(x.ProjectNetworkSolveHistoryStatusTypeID)).Delete();
            }
        }

        public static void DeleteProjectNetworkSolveHistoryStatusType(this IQueryable<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypes, int projectNetworkSolveHistoryStatusTypeID)
        {
            DeleteProjectNetworkSolveHistoryStatusType(projectNetworkSolveHistoryStatusTypes, new List<int> { projectNetworkSolveHistoryStatusTypeID });
        }

        public static void DeleteProjectNetworkSolveHistoryStatusType(this IQueryable<ProjectNetworkSolveHistoryStatusType> projectNetworkSolveHistoryStatusTypes, ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusTypeToDelete)
        {
            DeleteProjectNetworkSolveHistoryStatusType(projectNetworkSolveHistoryStatusTypes, new List<ProjectNetworkSolveHistoryStatusType> { projectNetworkSolveHistoryStatusTypeToDelete });
        }
    }
}