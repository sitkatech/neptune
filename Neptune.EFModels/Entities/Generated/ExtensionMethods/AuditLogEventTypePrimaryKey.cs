//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.AuditLogEventType


namespace Neptune.EFModels.Entities
{
    public class AuditLogEventTypePrimaryKey : EntityPrimaryKey<AuditLogEventType>
    {
        public AuditLogEventTypePrimaryKey() : base(){}
        public AuditLogEventTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public AuditLogEventTypePrimaryKey(AuditLogEventType auditLogEventType) : base(auditLogEventType){}

        public static implicit operator AuditLogEventTypePrimaryKey(int primaryKeyValue)
        {
            return new AuditLogEventTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator AuditLogEventTypePrimaryKey(AuditLogEventType auditLogEventType)
        {
            return new AuditLogEventTypePrimaryKey(auditLogEventType);
        }
    }
}