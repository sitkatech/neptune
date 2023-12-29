using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("LandUseBlock")]
[Index("LandUseBlockGeometry", Name = "SPATIAL_LandUseBlock_LandUseBlockGeometry")]
[Index("LandUseBlockGeometry4326", Name = "SPATIAL_LandUseBlock_LandUseBlockGeometry4326")]
public partial class LandUseBlock
{
    [Key]
    public int LandUseBlockID { get; set; }

    public int? PriorityLandUseTypeID { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? LandUseDescription { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry LandUseBlockGeometry { get; set; } = null!;

    [Column(TypeName = "decimal(4, 1)")]
    public decimal? TrashGenerationRate { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? LandUseForTGR { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeResidential { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeRetail { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    public int PermitTypeID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? LandUseBlockGeometry4326 { get; set; }

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("LandUseBlocks")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [InverseProperty("LandUseBlock")]
    public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; } = new List<TrashGeneratingUnit4326>();

    [InverseProperty("LandUseBlock")]
    public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; } = new List<TrashGeneratingUnit>();
}
