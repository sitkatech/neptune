using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public class ProjectDocuments
    {
        public static IQueryable<ProjectDocument> GetProjectDocumentsImplNoTracking(NeptuneDbContext dbContext)
        {
            return dbContext.ProjectDocuments
                .Include(x => x.Project)
                    .ThenInclude(x => x.StormwaterJurisdiction)
                .Include(x => x.FileResource)
                .AsNoTracking();
        }

        public static ProjectDocument GetByID(NeptuneDbContext dbContext, int projectDocumentID)
        {
            return GetProjectDocumentsImplNoTracking(dbContext)
                .SingleOrDefault(x => x.ProjectDocumentID == projectDocumentID);
        }

        public static ProjectDocument GetByIDWithTracking(NeptuneDbContext dbContext, int projectDocumentID)
        {
            return dbContext.ProjectDocuments
                .Include(x => x.Project)
                    .ThenInclude(x => x.StormwaterJurisdiction)
                .Include(x => x.FileResource)
                .SingleOrDefault(x => x.ProjectDocumentID == projectDocumentID);
        }

        public static List<ProjectDocumentSimpleDto> ListByProjectIDAsSimpleDto(NeptuneDbContext dbContext, int projectID)
        {
            return GetProjectDocumentsImplNoTracking(dbContext)
                .Where(x => x.ProjectID == projectID)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }

        public static ProjectDocument Update(NeptuneDbContext dbContext, ProjectDocument projectDocument, ProjectDocumentUpdateDto projectDocumentUpdateDto)
        {
            projectDocument.DisplayName = projectDocumentUpdateDto.DisplayName;
            projectDocument.DocumentDescription = projectDocumentUpdateDto.DocumentDescription;

            dbContext.SaveChanges(); 
            dbContext.Entry(projectDocument).Reload();

            return projectDocument;
        }

        public static void Delete(NeptuneDbContext dbContext, ProjectDocument projectDocument)
        {
            dbContext.ProjectDocuments.Remove(projectDocument);
            dbContext.SaveChanges();
        }
    }
}
