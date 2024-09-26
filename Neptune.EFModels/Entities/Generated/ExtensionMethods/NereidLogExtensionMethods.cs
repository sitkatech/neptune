//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidLog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NereidLogExtensionMethods
    {
        public static NereidLogSimpleDto AsSimpleDto(this NereidLog nereidLog)
        {
            var dto = new NereidLogSimpleDto()
            {
                NereidLogID = nereidLog.NereidLogID,
                RequestDate = nereidLog.RequestDate,
                NereidRequest = nereidLog.NereidRequest,
                NereidResponse = nereidLog.NereidResponse
            };
            return dto;
        }
    }
}