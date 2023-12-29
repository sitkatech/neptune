using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("WaterQualityManagementPlanVerifyTreatmentBMP")]
[Index("TreatmentBMPID", "WaterQualityManagementPlanVerifyTreatmentBMPID", Name = "AK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMPID_WaterQualityManagementPlanVerifyTreatmentBMPID", IsUnique = true)]
public partial class WaterQualityManagementPlanVerifyTreatmentBMP
{
    [Key]
    public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }

    public int WaterQualityManagementPlanVerifyID { get; set; }

    public int TreatmentBMPID { get; set; }

    public bool? IsAdequate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("WaterQualityManagementPlanVerifyTreatmentBMPs")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanVerifyID")]
    [InverseProperty("WaterQualityManagementPlanVerifyTreatmentBMPs")]
    public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; } = null!;
}
