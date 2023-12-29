using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("ModelBasinStaging")]
[Index("ModelBasinKey", Name = "AK_ModelBasinStaging_ModelBasinKey", IsUnique = true)]
[Index("ModelBasinGeometry", Name = "SPATIAL_ModelBasinStaging_ModelBasinGeometry")]
public partial class ModelBasinStaging
{
    [Key]
    public int ModelBasinStagingID { get; set; }

    public int ModelBasinKey { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry ModelBasinGeometry { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string? ModelBasinState { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ModelBasinRegion { get; set; }
}
