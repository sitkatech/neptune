using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisitType")]
    [Index(nameof(FieldVisitTypeName), Name = "AK_FieldVisitType_FieldVisitTypeName", IsUnique = true)]
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
        public string FieldVisitTypeName { get; set; }
        [Required]
        [StringLength(40)]
        public string FieldVisitTypeDisplayName { get; set; }

        [InverseProperty(nameof(FieldVisit.FieldVisitType))]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
    }
}
