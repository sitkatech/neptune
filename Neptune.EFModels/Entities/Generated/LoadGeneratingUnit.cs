using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("LoadGeneratingUnit")]
[Index("LoadGeneratingUnitGeometry", Name = "SPATIAL_LoadGeneratingUnit_LoadGeneratingUnitGeometry")]
public partial class LoadGeneratingUnit
{
    [Key]
    public int LoadGeneratingUnitID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry LoadGeneratingUnitGeometry { get; set; } = null!;

    public int? ModelBasinID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? DelineationID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public bool? IsEmptyResponseFromHRUService { get; set; }

    [ForeignKey("DelineationID")]
    [InverseProperty("LoadGeneratingUnits")]
    public virtual Delineation? Delineation { get; set; }

    [InverseProperty("LoadGeneratingUnit")]
    public virtual ICollection<HRUCharacteristic> HRUCharacteristics { get; set; } = new List<HRUCharacteristic>();

    [ForeignKey("ModelBasinID")]
    [InverseProperty("LoadGeneratingUnits")]
    public virtual ModelBasin? ModelBasin { get; set; }

    [ForeignKey("RegionalSubbasinID")]
    [InverseProperty("LoadGeneratingUnits")]
    public virtual RegionalSubbasin? RegionalSubbasin { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("LoadGeneratingUnits")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }
}
