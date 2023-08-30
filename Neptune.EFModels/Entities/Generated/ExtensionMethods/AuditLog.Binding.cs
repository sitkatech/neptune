//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AuditLog]
namespace Neptune.EFModels.Entities
{
    public partial class AuditLog : IHavePrimaryKey
    {
        public int PrimaryKey => AuditLogID;
        public AuditLogEventType AuditLogEventType => AuditLogEventType.AllLookupDictionary[AuditLogEventTypeID];

        public static class FieldLengths
        {
            public const int TableName = 500;
            public const int ColumnName = 500;
        }
    }
}