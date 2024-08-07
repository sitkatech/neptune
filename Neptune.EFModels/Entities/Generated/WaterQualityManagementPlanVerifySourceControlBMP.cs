﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("WaterQualityManagementPlanVerifySourceControlBMP")]
public partial class WaterQualityManagementPlanVerifySourceControlBMP
{
    [Key]
    public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }

    public int WaterQualityManagementPlanVerifyID { get; set; }

    public int SourceControlBMPID { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanSourceControlCondition { get; set; }

    [ForeignKey("SourceControlBMPID")]
    [InverseProperty("WaterQualityManagementPlanVerifySourceControlBMPs")]
    public virtual SourceControlBMP SourceControlBMP { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanVerifyID")]
    [InverseProperty("WaterQualityManagementPlanVerifySourceControlBMPs")]
    public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; } = null!;
}
