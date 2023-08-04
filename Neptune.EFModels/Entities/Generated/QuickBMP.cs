using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("QuickBMP")]
[Index("WaterQualityManagementPlanID", "QuickBMPName", Name = "AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName", IsUnique = true)]
public partial class QuickBMP
{
    [Key]
    public int QuickBMPID { get; set; }

    public int WaterQualityManagementPlanID { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? QuickBMPName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? QuickBMPNote { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PercentOfSiteTreated { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PercentCaptured { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PercentRetained { get; set; }

    public int? DryWeatherFlowOverrideID { get; set; }

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("QuickBMPs")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("QuickBMPs")]
    public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; } = null!;

    [InverseProperty("QuickBMP")]
    public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; } = new List<WaterQualityManagementPlanVerifyQuickBMP>();
}
