using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisitStatus")]
    [Index("FieldVisitStatusName", Name = "AK_FieldVisitStatus_FieldVisitStatusName", IsUnique = true)]
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
        [Unicode(false)]
        public string FieldVisitStatusName { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string FieldVisitStatusDisplayName { get; set; }

        [InverseProperty("FieldVisitStatus")]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
    }
}
