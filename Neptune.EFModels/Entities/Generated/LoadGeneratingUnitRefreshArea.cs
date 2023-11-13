using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("LoadGeneratingUnitRefreshArea")]
[Index("LoadGeneratingUnitRefreshAreaGeometry", Name = "SPATIAL_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaGeometry")]
public partial class LoadGeneratingUnitRefreshArea
{
    [Key]
    public int LoadGeneratingUnitRefreshAreaID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry LoadGeneratingUnitRefreshAreaGeometry { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? ProcessDate { get; set; }
}
