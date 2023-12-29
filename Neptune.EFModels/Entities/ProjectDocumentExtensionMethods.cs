using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectDocumentExtensionMethods
    {
        public static ProjectDocumentDto AsDto(this ProjectDocument projectDocument)
        {
            var dto = new ProjectDocumentDto()
            {
                ProjectDocumentID = projectDocument.ProjectDocumentID,
                FileResourceID = projectDocument.FileResourceID,
                ProjectID = projectDocument.ProjectID,
                DisplayName = projectDocument.DisplayName,
                UploadDate = projectDocument.UploadDate,
                DocumentDescription = projectDocument.DocumentDescription,
                FileResource = projectDocument.FileResource.AsSimpleDto()
            };
            return dto;
        }
    }
}