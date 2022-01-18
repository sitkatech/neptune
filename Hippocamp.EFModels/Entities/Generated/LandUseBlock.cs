using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("LandUseBlock")]
    [Index(nameof(LandUseBlockGeometry), Name = "SPATIAL_LandUseBlock_LandUseBlockGeometry")]
    public partial class LandUseBlock
    {
        public LandUseBlock()
        {
            TrashGeneratingUnit4326s = new HashSet<TrashGeneratingUnit4326>();
            TrashGeneratingUnits = new HashSet<TrashGeneratingUnit>();
        }

        [Key]
        public int LandUseBlockID { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        [StringLength(500)]
        public string LandUseDescription { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockGeometry { get; set; }
        [Column(TypeName = "decimal(4, 1)")]
        public decimal? TrashGenerationRate { get; set; }
        [StringLength(80)]
        public string LandUseForTGR { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? MedianHouseholdIncomeResidential { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? MedianHouseholdIncomeRetail { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PermitTypeID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockGeometry4326 { get; set; }

        [ForeignKey(nameof(PermitTypeID))]
        [InverseProperty("LandUseBlocks")]
        public virtual PermitType PermitType { get; set; }
        [ForeignKey(nameof(PriorityLandUseTypeID))]
        [InverseProperty("LandUseBlocks")]
        public virtual PriorityLandUseType PriorityLandUseType { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("LandUseBlocks")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty(nameof(TrashGeneratingUnit4326.LandUseBlock))]
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        [InverseProperty(nameof(TrashGeneratingUnit.LandUseBlock))]
        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
    }
}
