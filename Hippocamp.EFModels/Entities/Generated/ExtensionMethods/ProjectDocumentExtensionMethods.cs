//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectDocumentExtensionMethods
    {
        public static ProjectDocumentDto AsDto(this ProjectDocument projectDocument)
        {
            var projectDocumentDto = new ProjectDocumentDto()
            {
                ProjectDocumentID = projectDocument.ProjectDocumentID,
                FileResource = projectDocument.FileResource.AsDto(),
                Project = projectDocument.Project.AsDto(),
                DisplayName = projectDocument.DisplayName,
                UploadDate = projectDocument.UploadDate,
                DocumentDescription = projectDocument.DocumentDescription
            };
            DoCustomMappings(projectDocument, projectDocumentDto);
            return projectDocumentDto;
        }

        static partial void DoCustomMappings(ProjectDocument projectDocument, ProjectDocumentDto projectDocumentDto);

        public static ProjectDocumentSimpleDto AsSimpleDto(this ProjectDocument projectDocument)
        {
            var projectDocumentSimpleDto = new ProjectDocumentSimpleDto()
            {
                ProjectDocumentID = projectDocument.ProjectDocumentID,
                FileResourceID = projectDocument.FileResourceID,
                ProjectID = projectDocument.ProjectID,
                DisplayName = projectDocument.DisplayName,
                UploadDate = projectDocument.UploadDate,
                DocumentDescription = projectDocument.DocumentDescription
            };
            DoCustomSimpleDtoMappings(projectDocument, projectDocumentSimpleDto);
            return projectDocumentSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectDocument projectDocument, ProjectDocumentSimpleDto projectDocumentSimpleDto);
    }
}