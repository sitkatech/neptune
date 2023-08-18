//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.AuditLog


namespace Neptune.EFModels.Entities
{
    public class AuditLogPrimaryKey : EntityPrimaryKey<AuditLog>
    {
        public AuditLogPrimaryKey() : base(){}
        public AuditLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public AuditLogPrimaryKey(AuditLog auditLog) : base(auditLog){}

        public static implicit operator AuditLogPrimaryKey(int primaryKeyValue)
        {
            return new AuditLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator AuditLogPrimaryKey(AuditLog auditLog)
        {
            return new AuditLogPrimaryKey(auditLog);
        }
    }
}