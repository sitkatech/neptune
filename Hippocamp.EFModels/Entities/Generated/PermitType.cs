using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("PermitType")]
    [Index(nameof(PermitTypeDisplayName), Name = "AK_PermitType_PermitTypeDisplayName", IsUnique = true)]
    [Index(nameof(PermitTypeName), Name = "AK_PermitType_PermitTypeName", IsUnique = true)]
    public partial class PermitType
    {
        public PermitType()
        {
            LandUseBlocks = new HashSet<LandUseBlock>();
        }

        [Key]
        public int PermitTypeID { get; set; }
        [Required]
        [StringLength(80)]
        public string PermitTypeName { get; set; }
        [Required]
        [StringLength(80)]
        public string PermitTypeDisplayName { get; set; }

        [InverseProperty(nameof(LandUseBlock.PermitType))]
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
    }
}
