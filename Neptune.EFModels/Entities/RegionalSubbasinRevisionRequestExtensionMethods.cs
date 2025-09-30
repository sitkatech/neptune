using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class RegionalSubbasinRevisionRequestExtensionMethods
    {
        public static RegionalSubbasinRevisionRequestDto AsDto(this RegionalSubbasinRevisionRequest entity)
        {
            return new RegionalSubbasinRevisionRequestDto
            {
                RegionalSubbasinRevisionRequestID = entity.RegionalSubbasinRevisionRequestID,
                Name = null
            };
        }
    }
}
