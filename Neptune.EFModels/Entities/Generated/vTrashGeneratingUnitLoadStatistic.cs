﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vTrashGeneratingUnitLoadStatistic
{
    public int PrimaryKey { get; set; }

    public int TrashGeneratingUnitID { get; set; }

    public int? TreatmentBMPID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentBMPName { get; set; }

    public int? TrashCaptureEffectivenessBMP { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TrashCaptureStatusBMP { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry TrashGeneratingUnitGeometry { get; set; } = null!;

    public double? TrashGeneratingUnitArea { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    public int OrganizationID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string OrganizationName { get; set; } = null!;

    [Column(TypeName = "decimal(4, 1)")]
    public decimal BaselineLoadingRate { get; set; }

    public bool IsFullTrashCapture { get; set; }

    public bool IsPartialTrashCapture { get; set; }

    public int PartialTrashCaptureEffectivenessPercentage { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? LandUseType { get; set; }

    public int? PriorityLandUseTypeID { get; set; }

    public bool? HasBaselineScore { get; set; }

    public bool? HasProgressScore { get; set; }

    [Column(TypeName = "numeric(23, 7)")]
    public decimal? CurrentLoadingRate { get; set; }

    [Column(TypeName = "decimal(4, 1)")]
    public decimal ProgressLoadingRate { get; set; }

    public bool DelineationIsVerified { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastCalculatedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PriorityLandUseTypeDisplayName { get; set; }

    public int? OnlandVisualTrashAssessmentAreaID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? OnlandVisualTrashAssessmentAreaName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? OnlandVisualTrashAssessmentAreaBaselineScore { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TrashCaptureStatusWQMP { get; set; }

    public int? TrashCaptureEffectivenessWQMP { get; set; }

    public int? LandUseBlockID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeResidential { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeRetail { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PermitClass { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? LandUseForTGR { get; set; }

    [Column(TypeName = "decimal(4, 1)")]
    public decimal? TrashGenerationRate { get; set; }

    public double Area { get; set; }

    [Column(TypeName = "numeric(24, 7)")]
    public decimal? LoadingRateDelta { get; set; }
}
