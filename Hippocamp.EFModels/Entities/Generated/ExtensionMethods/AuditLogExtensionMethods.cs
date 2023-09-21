//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AuditLog]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class AuditLogExtensionMethods
    {
        public static AuditLogDto AsDto(this AuditLog auditLog)
        {
            var auditLogDto = new AuditLogDto()
            {
                AuditLogID = auditLog.AuditLogID,
                Person = auditLog.Person.AsDto(),
                AuditLogDate = auditLog.AuditLogDate,
                AuditLogEventType = auditLog.AuditLogEventType.AsDto(),
                TableName = auditLog.TableName,
                RecordID = auditLog.RecordID,
                ColumnName = auditLog.ColumnName,
                OriginalValue = auditLog.OriginalValue,
                NewValue = auditLog.NewValue,
                AuditDescription = auditLog.AuditDescription
            };
            DoCustomMappings(auditLog, auditLogDto);
            return auditLogDto;
        }

        static partial void DoCustomMappings(AuditLog auditLog, AuditLogDto auditLogDto);

        public static AuditLogSimpleDto AsSimpleDto(this AuditLog auditLog)
        {
            var auditLogSimpleDto = new AuditLogSimpleDto()
            {
                AuditLogID = auditLog.AuditLogID,
                PersonID = auditLog.PersonID,
                AuditLogDate = auditLog.AuditLogDate,
                AuditLogEventTypeID = auditLog.AuditLogEventTypeID,
                TableName = auditLog.TableName,
                RecordID = auditLog.RecordID,
                ColumnName = auditLog.ColumnName,
                OriginalValue = auditLog.OriginalValue,
                NewValue = auditLog.NewValue,
                AuditDescription = auditLog.AuditDescription
            };
            DoCustomSimpleDtoMappings(auditLog, auditLogSimpleDto);
            return auditLogSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(AuditLog auditLog, AuditLogSimpleDto auditLogSimpleDto);
    }
}