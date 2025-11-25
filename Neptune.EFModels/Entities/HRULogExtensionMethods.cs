using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class HRULogExtensionMethods
    {
        public static HRULogDto AsDto(this HRULog entity)
        {
            return new HRULogDto
            {
                HRULogID = entity.HRULogID,
                RequestDate = entity.RequestDate,
                Success = entity.Success,
                HRURequest = entity.HRURequest,
                HRUResponse = entity.HRUResponse
            };
        }
    }
}
