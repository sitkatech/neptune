using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vPowerBIWaterQualityManagementPlan
{
    public int PrimaryKey { get; set; }

    public int WaterQualityManagementPlanID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string OrganizationName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanStatusDisplayName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanLandUseDisplayName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanPermitTermDisplayName { get; set; }

    public int? ApprovalDate { get; set; }

    public int? DateOfConstruction { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? HydromodificationAppliesDisplayName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? HydrologicSubareaName { get; set; }

    [Column(TypeName = "decimal(5, 1)")]
    public decimal? RecordedWQMPAreaInAcres { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TrashCaptureStatusTypeDisplayName { get; set; }

    public int? TrashCaptureEffectiveness { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ModelingApproach { get; set; }
}
