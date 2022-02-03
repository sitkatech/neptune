using System;
using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public class ProjectDocuments
    {
        private static IQueryable<ProjectDocument> GetProjectDocumentsImpl(HippocampDbContext dbContext)
        {
            return dbContext.ProjectDocuments
                .Include(x => x.Project)
                .Include(x => x.FileResource)
                    .ThenInclude(x => x.FileResourceMimeType)
                .AsNoTracking();
        }

        public static ProjectDocument GetByID(HippocampDbContext dbContext, int projectDocumentID)
        {
            return GetProjectDocumentsImpl(dbContext)
                .SingleOrDefault(x => x.ProjectDocumentID == projectDocumentID);
        }

        public static List<ProjectDocumentSimpleDto> ListByProjectIDAsSimpleDto(HippocampDbContext dbContext, int projectID)
        {
            return GetProjectDocumentsImpl(dbContext)
                .Where(x => x.ProjectID == projectID)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }

        public static ProjectDocument Create(HippocampDbContext dbContext, ProjectDocumentUpsertDto projectDocumentUpsertDto, FileResource fileResource)
        {
            var projectDocument = new ProjectDocument()
            {
                ProjectID = projectDocumentUpsertDto.ProjectID,
                FileResource = fileResource,
                DisplayName = projectDocumentUpsertDto.DisplayName,
                DocumentDescription = projectDocumentUpsertDto.DocumentDescription,
                UploadDate = DateTime.Now
            };

            dbContext.ProjectDocuments.Add(projectDocument);
            dbContext.SaveChanges();
            dbContext.Entry(projectDocument).Reload();

            return projectDocument;
        }

        public static ProjectDocument Update(HippocampDbContext dbContext, ProjectDocument projectDocument, ProjectDocumentUpsertDto projectDocumentUpsertDto, FileResource fileResource)
        {
            if (fileResource != null)
            {
                projectDocument.FileResource = fileResource;
                projectDocument.UploadDate = DateTime.Now;
            }
            
            projectDocument.DisplayName = projectDocumentUpsertDto.DisplayName;
            projectDocument.DocumentDescription = projectDocumentUpsertDto.DocumentDescription;

            dbContext.SaveChanges(); 
            dbContext.Entry(projectDocument).Reload();

            return projectDocument;
        }

        public static void Delete(HippocampDbContext dbContext, ProjectDocument projectDocument)
        {
            dbContext.ProjectDocuments.Remove(projectDocument);
            dbContext.SaveChanges();
        }
    }
}
