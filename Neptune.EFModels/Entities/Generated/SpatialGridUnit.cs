using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("SpatialGridUnit")]
[Index("SpatialGridUnitGeometry", Name = "SPATIAL_SpatialGridUnit_SpatialGridUnitGeometry")]
public partial class SpatialGridUnit
{
    [Key]
    public int SpatialGridUnitID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry SpatialGridUnitGeometry { get; set; } = null!;
}
