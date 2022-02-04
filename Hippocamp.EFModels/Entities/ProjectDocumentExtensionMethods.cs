using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectDocumentExtensionMethods
    {
        static partial void DoCustomSimpleDtoMappings(ProjectDocument projectDocument, ProjectDocumentSimpleDto projectDocumentSimpleDto)
        {
            projectDocumentSimpleDto.FileResource = projectDocument.FileResource.AsSimpleDto();
        }

    }

}