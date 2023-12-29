using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("TrashGeneratingUnit")]
[Index("TrashGeneratingUnitGeometry", Name = "SPATIAL_TrashGeneratingUnit_TrashGeneratingUnitGeometry")]
public partial class TrashGeneratingUnit
{
    [Key]
    public int TrashGeneratingUnitID { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    public int? OnlandVisualTrashAssessmentAreaID { get; set; }

    public int? LandUseBlockID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry TrashGeneratingUnitGeometry { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public int? DelineationID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    [ForeignKey("DelineationID")]
    [InverseProperty("TrashGeneratingUnits")]
    public virtual Delineation? Delineation { get; set; }

    [ForeignKey("LandUseBlockID")]
    [InverseProperty("TrashGeneratingUnits")]
    public virtual LandUseBlock? LandUseBlock { get; set; }

    [ForeignKey("OnlandVisualTrashAssessmentAreaID")]
    [InverseProperty("TrashGeneratingUnits")]
    public virtual OnlandVisualTrashAssessmentArea? OnlandVisualTrashAssessmentArea { get; set; }

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("TrashGeneratingUnits")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("TrashGeneratingUnits")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }
}
