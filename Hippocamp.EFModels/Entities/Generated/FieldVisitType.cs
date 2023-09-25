using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("FieldVisitType")]
    [Index("FieldVisitTypeName", Name = "AK_FieldVisitType_FieldVisitTypeName", IsUnique = true)]
    public partial class FieldVisitType
    {
        public FieldVisitType()
        {
            FieldVisits = new HashSet<FieldVisit>();
        }

        [Key]
        public int FieldVisitTypeID { get; set; }
        [Required]
        [StringLength(40)]
        [Unicode(false)]
        public string FieldVisitTypeName { get; set; }
        [Required]
        [StringLength(40)]
        [Unicode(false)]
        public string FieldVisitTypeDisplayName { get; set; }

        [InverseProperty("FieldVisitType")]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
    }
}
