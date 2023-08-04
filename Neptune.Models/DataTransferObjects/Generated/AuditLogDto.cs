//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AuditLog]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class AuditLogDto
    {
        public int AuditLogID { get; set; }
        public PersonDto Person { get; set; }
        public DateTime AuditLogDate { get; set; }
        public AuditLogEventTypeDto AuditLogEventType { get; set; }
        public string TableName { get; set; }
        public int RecordID { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string AuditDescription { get; set; }
    }

    public partial class AuditLogSimpleDto
    {
        public int AuditLogID { get; set; }
        public System.Int32 PersonID { get; set; }
        public DateTime AuditLogDate { get; set; }
        public System.Int32 AuditLogEventTypeID { get; set; }
        public string TableName { get; set; }
        public int RecordID { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string AuditDescription { get; set; }
    }

}