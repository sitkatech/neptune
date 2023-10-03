using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectDocumentExtensionMethods
    {
        static partial void DoCustomSimpleDtoMappings(ProjectDocument projectDocument, ProjectDocumentSimpleDto projectDocumentSimpleDto)
        {
            projectDocumentSimpleDto.FileResource = projectDocument.FileResource.AsSimpleDto();
        }

    }

}