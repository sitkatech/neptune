//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectDocumentExtensionMethods
    {
        public static ProjectDocumentSimpleDto AsSimpleDto(this ProjectDocument projectDocument)
        {
            var dto = new ProjectDocumentSimpleDto()
            {
                ProjectDocumentID = projectDocument.ProjectDocumentID,
                FileResourceID = projectDocument.FileResourceID,
                ProjectID = projectDocument.ProjectID,
                DisplayName = projectDocument.DisplayName,
                UploadDate = projectDocument.UploadDate,
                DocumentDescription = projectDocument.DocumentDescription
            };
            return dto;
        }
    }
}