using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisitStatus")]
    [Index(nameof(FieldVisitStatusName), Name = "AK_FieldVisitStatus_FieldVisitStatusName", IsUnique = true)]
    public partial class FieldVisitStatus
    {
        public FieldVisitStatus()
        {
            FieldVisits = new HashSet<FieldVisit>();
        }

        [Key]
        public int FieldVisitStatusID { get; set; }
        [Required]
        [StringLength(20)]
        public string FieldVisitStatusName { get; set; }
        [Required]
        [StringLength(20)]
        public string FieldVisitStatusDisplayName { get; set; }

        [InverseProperty(nameof(FieldVisit.FieldVisitStatus))]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
    }
}
