//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AuditLogEventType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class AuditLogEventTypeExtensionMethods
    {
        public static AuditLogEventTypeDto AsDto(this AuditLogEventType auditLogEventType)
        {
            var auditLogEventTypeDto = new AuditLogEventTypeDto()
            {
                AuditLogEventTypeID = auditLogEventType.AuditLogEventTypeID,
                AuditLogEventTypeName = auditLogEventType.AuditLogEventTypeName,
                AuditLogEventTypeDisplayName = auditLogEventType.AuditLogEventTypeDisplayName
            };
            DoCustomMappings(auditLogEventType, auditLogEventTypeDto);
            return auditLogEventTypeDto;
        }

        static partial void DoCustomMappings(AuditLogEventType auditLogEventType, AuditLogEventTypeDto auditLogEventTypeDto);

        public static AuditLogEventTypeSimpleDto AsSimpleDto(this AuditLogEventType auditLogEventType)
        {
            var auditLogEventTypeSimpleDto = new AuditLogEventTypeSimpleDto()
            {
                AuditLogEventTypeID = auditLogEventType.AuditLogEventTypeID,
                AuditLogEventTypeName = auditLogEventType.AuditLogEventTypeName,
                AuditLogEventTypeDisplayName = auditLogEventType.AuditLogEventTypeDisplayName
            };
            DoCustomSimpleDtoMappings(auditLogEventType, auditLogEventTypeSimpleDto);
            return auditLogEventTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(AuditLogEventType auditLogEventType, AuditLogEventTypeSimpleDto auditLogEventTypeSimpleDto);
    }
}