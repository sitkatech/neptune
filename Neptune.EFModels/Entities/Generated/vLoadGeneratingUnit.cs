using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vLoadGeneratingUnit
{
    public int LoadGeneratingUnitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateHRURequested { get; set; }

    public bool? IsEmptyResponseFromHRUService { get; set; }

    public int? DelineationID { get; set; }

    public int? TreatmentBMPID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentBMPName { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanName { get; set; }

    public int? RegionalSubbasinID { get; set; }

    [StringLength(127)]
    [Unicode(false)]
    public string RegionalSubbasinName { get; set; } = null!;

    public int? ModelBasinID { get; set; }

    public int? ModelBasinKey { get; set; }
}
