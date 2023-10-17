using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vHRUCharacteristic
{
    public int HRUCharacteristicID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    public int LoadGeneratingUnitID { get; set; }

    [StringLength(29)]
    [Unicode(false)]
    public string HRUEntity { get; set; } = null!;

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

    [StringLength(5)]
    [Unicode(false)]
    public string? HydrologicSoilGroup { get; set; }

    public int SlopePercentage { get; set; }

    public double Area { get; set; }

    public double ImperviousAcres { get; set; }

    public int HRUCharacteristicLandUseCodeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? HRUCharacteristicLandUseCodeDisplayName { get; set; }

    public double BaselineImperviousAcres { get; set; }

    public int BaselineHRUCharacteristicLandUseCodeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BaslineHRUCharacteristicLandUseCodeDisplayName { get; set; }
}
