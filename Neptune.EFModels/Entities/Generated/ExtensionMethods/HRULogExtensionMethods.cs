//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRULog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HRULogExtensionMethods
    {
        public static HRULogSimpleDto AsSimpleDto(this HRULog hRULog)
        {
            var dto = new HRULogSimpleDto()
            {
                HRULogID = hRULog.HRULogID,
                RequestDate = hRULog.RequestDate,
                Success = hRULog.Success,
                HRURequest = hRULog.HRURequest,
                HRUResponse = hRULog.HRUResponse
            };
            return dto;
        }
    }
}