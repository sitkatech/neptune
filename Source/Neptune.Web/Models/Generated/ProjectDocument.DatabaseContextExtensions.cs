//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]
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
        public static ProjectDocument GetProjectDocument(this IQueryable<ProjectDocument> projectDocuments, int projectDocumentID)
        {
            var projectDocument = projectDocuments.SingleOrDefault(x => x.ProjectDocumentID == projectDocumentID);
            Check.RequireNotNullThrowNotFound(projectDocument, "ProjectDocument", projectDocumentID);
            return projectDocument;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectDocument(this IQueryable<ProjectDocument> projectDocuments, List<int> projectDocumentIDList)
        {
            if(projectDocumentIDList.Any())
            {
                projectDocuments.Where(x => projectDocumentIDList.Contains(x.ProjectDocumentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectDocument(this IQueryable<ProjectDocument> projectDocuments, ICollection<ProjectDocument> projectDocumentsToDelete)
        {
            if(projectDocumentsToDelete.Any())
            {
                var projectDocumentIDList = projectDocumentsToDelete.Select(x => x.ProjectDocumentID).ToList();
                projectDocuments.Where(x => projectDocumentIDList.Contains(x.ProjectDocumentID)).Delete();
            }
        }

        public static void DeleteProjectDocument(this IQueryable<ProjectDocument> projectDocuments, int projectDocumentID)
        {
            DeleteProjectDocument(projectDocuments, new List<int> { projectDocumentID });
        }

        public static void DeleteProjectDocument(this IQueryable<ProjectDocument> projectDocuments, ProjectDocument projectDocumentToDelete)
        {
            DeleteProjectDocument(projectDocuments, new List<ProjectDocument> { projectDocumentToDelete });
        }
    }
}