using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        [Unicode(false)]
        public string SourceControlBMPAttributeName { get; set; }

        [ForeignKey("SourceControlBMPAttributeCategoryID")]
        [InverseProperty("SourceControlBMPAttributes")]
        public virtual SourceControlBMPAttributeCategory SourceControlBMPAttributeCategory { get; set; }
        [InverseProperty("SourceControlBMPAttribute")]
        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
    }
}
