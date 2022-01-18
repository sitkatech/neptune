using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("AuditLogEventType")]
    [Index(nameof(AuditLogEventTypeDisplayName), Name = "AK_AuditLogEventType_AuditLogEventTypeDisplayName", IsUnique = true)]
    [Index(nameof(AuditLogEventTypeName), Name = "AK_AuditLogEventType_AuditLogEventTypeName", IsUnique = true)]
    public partial class AuditLogEventType
    {
        public AuditLogEventType()
        {
            AuditLogs = new HashSet<AuditLog>();
        }

        [Key]
        public int AuditLogEventTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string AuditLogEventTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string AuditLogEventTypeDisplayName { get; set; }

        [InverseProperty(nameof(AuditLog.AuditLogEventType))]
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}
