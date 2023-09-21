using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("MaintenanceRecordType")]
    [Index("MaintenanceRecordTypeDisplayName", Name = "AK_MaintenanceRecordType_MaintenanceRecordTypeDisplayName", IsUnique = true)]
    [Index("MaintenanceRecordTypeName", Name = "AK_MaintenanceRecordType_MaintenanceRecordTypeName", IsUnique = true)]
    public partial class MaintenanceRecordType
    {
        public MaintenanceRecordType()
        {
            MaintenanceRecords = new HashSet<MaintenanceRecord>();
        }

        [Key]
        public int MaintenanceRecordTypeID { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string MaintenanceRecordTypeName { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string MaintenanceRecordTypeDisplayName { get; set; }

        [InverseProperty("MaintenanceRecordType")]
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    }
}
