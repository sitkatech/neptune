using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("DelineationType")]
    [Index("DelineationTypeDisplayName", Name = "AK_DelineationType_DelineationTypeDisplayName", IsUnique = true)]
    [Index("DelineationTypeName", Name = "AK_DelineationType_DelineationTypeName", IsUnique = true)]
    public partial class DelineationType
    {
        public DelineationType()
        {
            Delineations = new HashSet<Delineation>();
        }

        [Key]
        public int DelineationTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DelineationTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DelineationTypeDisplayName { get; set; }

        [InverseProperty("DelineationType")]
        public virtual ICollection<Delineation> Delineations { get; set; }
    }
}
