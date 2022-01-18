using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("SourceControlBMPAttribute")]
    public partial class SourceControlBMPAttribute
    {
        public SourceControlBMPAttribute()
        {
            SourceControlBMPs = new HashSet<SourceControlBMP>();
        }

        [Key]
        public int SourceControlBMPAttributeID { get; set; }
        public int SourceControlBMPAttributeCategoryID { get; set; }
        [Required]
        [StringLength(100)]
        public string SourceControlBMPAttributeName { get; set; }

        [ForeignKey(nameof(SourceControlBMPAttributeCategoryID))]
        [InverseProperty("SourceControlBMPAttributes")]
        public virtual SourceControlBMPAttributeCategory SourceControlBMPAttributeCategory { get; set; }
        [InverseProperty(nameof(SourceControlBMP.SourceControlBMPAttribute))]
        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
    }
}
