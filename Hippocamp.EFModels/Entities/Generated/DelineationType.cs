using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("DelineationType")]
    [Index(nameof(DelineationTypeDisplayName), Name = "AK_DelineationType_DelineationTypeDisplayName", IsUnique = true)]
    [Index(nameof(DelineationTypeName), Name = "AK_DelineationType_DelineationTypeName", IsUnique = true)]
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
        public string DelineationTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string DelineationTypeDisplayName { get; set; }

        [InverseProperty(nameof(Delineation.DelineationType))]
        public virtual ICollection<Delineation> Delineations { get; set; }
    }
}
