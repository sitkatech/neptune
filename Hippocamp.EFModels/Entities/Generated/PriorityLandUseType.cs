using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("PriorityLandUseType")]
    [Index("PriorityLandUseTypeDisplayName", Name = "AK_PriorityLandUseType_PriorityLandUseTypeDisplayName", IsUnique = true)]
    [Index("PriorityLandUseTypeName", Name = "AK_PriorityLandUseType_PriorityLandUseTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string PriorityLandUseTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string PriorityLandUseTypeDisplayName { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string MapColorHexCode { get; set; }

        [InverseProperty("PriorityLandUseType")]
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
    }
}
