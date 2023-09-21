using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("AuditLogEventType")]
    [Index("AuditLogEventTypeDisplayName", Name = "AK_AuditLogEventType_AuditLogEventTypeDisplayName", IsUnique = true)]
    [Index("AuditLogEventTypeName", Name = "AK_AuditLogEventType_AuditLogEventTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string AuditLogEventTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AuditLogEventTypeDisplayName { get; set; }

        [InverseProperty("AuditLogEventType")]
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}
