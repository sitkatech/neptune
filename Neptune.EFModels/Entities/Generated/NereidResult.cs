using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NereidResult")]
public partial class NereidResult
{
    [Key]
    public int NereidResultID { get; set; }

    public int? TreatmentBMPID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? DelineationID { get; set; }

    [Unicode(false)]
    public string? NodeID { get; set; }

    [Unicode(false)]
    public string? FullResponse { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    public bool IsBaselineCondition { get; set; }

    [ForeignKey("DelineationID")]
    [InverseProperty("NereidResults")]
    public virtual Delineation? Delineation { get; set; }

    [ForeignKey("RegionalSubbasinID")]
    [InverseProperty("NereidResults")]
    public virtual RegionalSubbasin? RegionalSubbasin { get; set; }

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("NereidResults")]
    public virtual TreatmentBMP? TreatmentBMP { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("NereidResults")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }
}
