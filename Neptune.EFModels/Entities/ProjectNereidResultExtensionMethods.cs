using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class ProjectNereidResultExtensionMethods
    {
        public static ProjectNereidResultDto AsDto(this ProjectNereidResult entity)
        {
            return new ProjectNereidResultDto
            {
                ProjectNereidResultID = entity.ProjectNereidResultID,
                NodeID = entity.NodeID
            };
        }
    }
}
