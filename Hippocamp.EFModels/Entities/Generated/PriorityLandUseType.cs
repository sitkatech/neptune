using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("PriorityLandUseType")]
    [Index(nameof(PriorityLandUseTypeDisplayName), Name = "AK_PriorityLandUseType_PriorityLandUseTypeDisplayName", IsUnique = true)]
    [Index(nameof(PriorityLandUseTypeName), Name = "AK_PriorityLandUseType_PriorityLandUseTypeName", IsUnique = true)]
    public partial class PriorityLandUseType
    {
        public PriorityLandUseType()
        {
            LandUseBlocks = new HashSet<LandUseBlock>();
        }

        [Key]
        public int PriorityLandUseTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string PriorityLandUseTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string PriorityLandUseTypeDisplayName { get; set; }
        [Required]
        [StringLength(7)]
        public string MapColorHexCode { get; set; }

        [InverseProperty(nameof(LandUseBlock.PriorityLandUseType))]
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
    }
}
