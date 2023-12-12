//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestLog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SupportRequestLogExtensionMethods
    {
        public static SupportRequestLogSimpleDto AsSimpleDto(this SupportRequestLog supportRequestLog)
        {
            var dto = new SupportRequestLogSimpleDto()
            {
                SupportRequestLogID = supportRequestLog.SupportRequestLogID,
                RequestDate = supportRequestLog.RequestDate,
                RequestPersonName = supportRequestLog.RequestPersonName,
                RequestPersonEmail = supportRequestLog.RequestPersonEmail,
                RequestPersonID = supportRequestLog.RequestPersonID,
                SupportRequestTypeID = supportRequestLog.SupportRequestTypeID,
                RequestDescription = supportRequestLog.RequestDescription,
                RequestPersonOrganization = supportRequestLog.RequestPersonOrganization,
                RequestPersonPhone = supportRequestLog.RequestPersonPhone
            };
            return dto;
        }
    }
}