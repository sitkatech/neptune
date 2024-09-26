using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NereidLogWaterQualityManagementPlan")]
[Index("NereidLogID", "WaterQualityManagementPlanID", Name = "AK_NereidLogWaterQualityManagementPlan_NereidLogID_WaterQualityManagementPlanID", IsUnique = true)]
public partial class NereidLogWaterQualityManagementPlan
{
    [Key]
    public int NereidLogWaterQualityManagementPlanID { get; set; }

    public int NereidLogID { get; set; }

    public int WaterQualityManagementPlanID { get; set; }

    [ForeignKey("NereidLogID")]
    [InverseProperty("NereidLogWaterQualityManagementPlans")]
    public virtual NereidLog NereidLog { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("NereidLogWaterQualityManagementPlans")]
    public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; } = null!;
}
