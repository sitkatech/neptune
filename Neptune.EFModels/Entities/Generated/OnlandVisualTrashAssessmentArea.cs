﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("OnlandVisualTrashAssessmentArea")]
[Index("OnlandVisualTrashAssessmentAreaName", "StormwaterJurisdictionID", Name = "AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaName_StormwaterJurisdictionID", IsUnique = true)]
[Index("OnlandVisualTrashAssessmentAreaGeometry", Name = "SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry")]
[Index("OnlandVisualTrashAssessmentAreaGeometry4326", Name = "SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry4326")]
[Index("TransectLine", Name = "SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine")]
[Index("TransectLine4326", Name = "SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine4326")]
public partial class OnlandVisualTrashAssessmentArea
{
    [Key]
    public int OnlandVisualTrashAssessmentAreaID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; } = null!;

    public int? OnlandVisualTrashAssessmentBaselineScoreID { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AssessmentAreaDescription { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? TransectLine { get; set; }

    public int? OnlandVisualTrashAssessmentProgressScoreID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? OnlandVisualTrashAssessmentAreaGeometry4326 { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? TransectLine4326 { get; set; }

    [InverseProperty("OnlandVisualTrashAssessmentArea")]
    public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; } = new List<OnlandVisualTrashAssessment>();

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("OnlandVisualTrashAssessmentAreas")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [InverseProperty("OnlandVisualTrashAssessmentArea")]
    public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; } = new List<TrashGeneratingUnit4326>();

    [InverseProperty("OnlandVisualTrashAssessmentArea")]
    public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; } = new List<TrashGeneratingUnit>();
}
