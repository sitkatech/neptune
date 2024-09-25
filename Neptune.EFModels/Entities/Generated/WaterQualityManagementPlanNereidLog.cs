using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("WaterQualityManagementPlanNereidLog")]
[Index("WaterQualityManagementPlanID", Name = "AK_WaterQualityManagementPlanNereidLog_WaterQualityManagementPlanID", IsUnique = true)]
public partial class WaterQualityManagementPlanNereidLog
{
    [Key]
    public int WaterQualityManagementPlanNereidLogID { get; set; }

    public int WaterQualityManagementPlanID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastRequestDate { get; set; }

    [Unicode(false)]
    public string? NereidRequest { get; set; }

    [Unicode(false)]
    public string? NereidResponse { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("WaterQualityManagementPlanNereidLog")]
    public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; } = null!;
}
