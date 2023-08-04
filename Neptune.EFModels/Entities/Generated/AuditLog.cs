using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("AuditLog")]
public partial class AuditLog
{
    [Key]
    public int AuditLogID { get; set; }

    public int PersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AuditLogDate { get; set; }

    public int AuditLogEventTypeID { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? TableName { get; set; }

    public int RecordID { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ColumnName { get; set; }

    [Unicode(false)]
    public string? OriginalValue { get; set; }

    [Unicode(false)]
    public string? NewValue { get; set; }

    [Unicode(false)]
    public string? AuditDescription { get; set; }

    [ForeignKey("PersonID")]
    [InverseProperty("AuditLogs")]
    public virtual Person Person { get; set; } = null!;
}
