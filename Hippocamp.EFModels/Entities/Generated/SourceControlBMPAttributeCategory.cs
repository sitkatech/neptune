using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("SourceControlBMPAttributeCategory")]
    public partial class SourceControlBMPAttributeCategory
    {
        public SourceControlBMPAttributeCategory()
        {
            SourceControlBMPAttributes = new HashSet<SourceControlBMPAttribute>();
        }

        [Key]
        public int SourceControlBMPAttributeCategoryID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SourceControlBMPAttributeCategoryShortName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string SourceControlBMPAttributeCategoryName { get; set; }

        [InverseProperty("SourceControlBMPAttributeCategory")]
        public virtual ICollection<SourceControlBMPAttribute> SourceControlBMPAttributes { get; set; }
    }
}
