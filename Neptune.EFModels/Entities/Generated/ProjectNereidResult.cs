using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("ProjectNereidResult")]
public partial class ProjectNereidResult
{
    [Key]
    public int ProjectNereidResultID { get; set; }

    public int ProjectID { get; set; }

    public bool IsBaselineCondition { get; set; }

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

    [ForeignKey("DelineationID")]
    [InverseProperty("ProjectNereidResults")]
    public virtual Delineation? Delineation { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("ProjectNereidResults")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("RegionalSubbasinID")]
    [InverseProperty("ProjectNereidResults")]
    public virtual RegionalSubbasin? RegionalSubbasin { get; set; }

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("ProjectNereidResults")]
    public virtual TreatmentBMP? TreatmentBMP { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("ProjectNereidResults")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }
}
