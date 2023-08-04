using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("ProjectHRUCharacteristic")]
public partial class ProjectHRUCharacteristic
{
    [Key]
    public int ProjectHRUCharacteristicID { get; set; }

    public int ProjectID { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? HydrologicSoilGroup { get; set; }

    public int SlopePercentage { get; set; }

    public double ImperviousAcres { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    public double Area { get; set; }

    public int HRUCharacteristicLandUseCodeID { get; set; }

    public int ProjectLoadGeneratingUnitID { get; set; }

    public double BaselineImperviousAcres { get; set; }

    public int BaselineHRUCharacteristicLandUseCodeID { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("ProjectHRUCharacteristics")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("ProjectLoadGeneratingUnitID")]
    [InverseProperty("ProjectHRUCharacteristics")]
    public virtual ProjectLoadGeneratingUnit ProjectLoadGeneratingUnit { get; set; } = null!;
}
