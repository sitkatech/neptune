using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("ModelBasin")]
[Index("ModelBasinKey", Name = "AK_ModelBasin_ModelBasinKey", IsUnique = true)]
[Index("ModelBasinGeometry", Name = "SPATIAL_ModelBasin_ModelBasinGeometry")]
public partial class ModelBasin
{
    [Key]
    public int ModelBasinID { get; set; }

    public int ModelBasinKey { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry ModelBasinGeometry { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LastUpdate { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ModelBasinState { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ModelBasinRegion { get; set; }

    [InverseProperty("ModelBasin")]
    public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; } = new List<LoadGeneratingUnit>();

    [InverseProperty("ModelBasin")]
    public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; } = new List<ProjectLoadGeneratingUnit>();

    [InverseProperty("ModelBasin")]
    public virtual ICollection<RegionalSubbasin> RegionalSubbasins { get; set; } = new List<RegionalSubbasin>();

    [InverseProperty("ModelBasin")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();
}
