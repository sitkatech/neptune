//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestLog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SupportRequestLogExtensionMethods
    {
        public static SupportRequestLogDto AsDto(this SupportRequestLog supportRequestLog)
        {
            var supportRequestLogDto = new SupportRequestLogDto()
            {
                SupportRequestLogID = supportRequestLog.SupportRequestLogID,
                RequestDate = supportRequestLog.RequestDate,
                RequestPersonName = supportRequestLog.RequestPersonName,
                RequestPersonEmail = supportRequestLog.RequestPersonEmail,
                RequestPerson = supportRequestLog.RequestPerson?.AsDto(),
                SupportRequestType = supportRequestLog.SupportRequestType.AsDto(),
                RequestDescription = supportRequestLog.RequestDescription,
                RequestPersonOrganization = supportRequestLog.RequestPersonOrganization,
                RequestPersonPhone = supportRequestLog.RequestPersonPhone
            };
            DoCustomMappings(supportRequestLog, supportRequestLogDto);
            return supportRequestLogDto;
        }

        static partial void DoCustomMappings(SupportRequestLog supportRequestLog, SupportRequestLogDto supportRequestLogDto);

        public static SupportRequestLogSimpleDto AsSimpleDto(this SupportRequestLog supportRequestLog)
        {
            var supportRequestLogSimpleDto = new SupportRequestLogSimpleDto()
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
            DoCustomSimpleDtoMappings(supportRequestLog, supportRequestLogSimpleDto);
            return supportRequestLogSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SupportRequestLog supportRequestLog, SupportRequestLogSimpleDto supportRequestLogSimpleDto);
    }
}