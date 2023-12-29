using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("DelineationOverlap")]
[Index("OverlappingGeometry", Name = "SPATIAL_DelineationOverlap_OverlappingGeometry")]
public partial class DelineationOverlap
{
    [Key]
    public int DelineationOverlapID { get; set; }

    public int DelineationID { get; set; }

    public int OverlappingDelineationID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry OverlappingGeometry { get; set; } = null!;

    [ForeignKey("DelineationID")]
    [InverseProperty("DelineationOverlapDelineations")]
    public virtual Delineation Delineation { get; set; } = null!;

    [ForeignKey("OverlappingDelineationID")]
    [InverseProperty("DelineationOverlapOverlappingDelineations")]
    public virtual Delineation OverlappingDelineation { get; set; } = null!;
}
