using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("LoadGeneratingUnit4326")]
[Index("LoadGeneratingUnit4326Geometry", Name = "SPATIAL_LoadGeneratingUnit4326_LoadGeneratingUnit4326Geometry")]
public partial class LoadGeneratingUnit4326
{
    [Key]
    public int LoadGeneratingUnit4326ID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry LoadGeneratingUnit4326Geometry { get; set; } = null!;

    public int? ModelBasinID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? DelineationID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public bool? IsEmptyResponseFromHRUService { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateHRURequested { get; set; }

    [ForeignKey("DelineationID")]
    [InverseProperty("LoadGeneratingUnit4326s")]
    public virtual Delineation? Delineation { get; set; }

    [ForeignKey("ModelBasinID")]
    [InverseProperty("LoadGeneratingUnit4326s")]
    public virtual ModelBasin? ModelBasin { get; set; }

    [ForeignKey("RegionalSubbasinID")]
    [InverseProperty("LoadGeneratingUnit4326s")]
    public virtual RegionalSubbasin? RegionalSubbasin { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("LoadGeneratingUnit4326s")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }
}
