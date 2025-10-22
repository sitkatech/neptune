using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class NereidResultExtensionMethods
    {
        public static NereidResultDto AsDto(this NereidResult entity)
        {
            return new NereidResultDto
            {
                NereidResultID = entity.NereidResultID,
                NodeID = entity.NodeID
            };
        }
    }
}
