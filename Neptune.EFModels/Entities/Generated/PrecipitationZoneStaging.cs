using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("PrecipitationZoneStaging")]
[Index("PrecipitationZoneKey", Name = "AK_PrecipitationZoneStaging_PrecipitationZoneKey", IsUnique = true)]
[Index("PrecipitationZoneGeometry", Name = "SPATIAL_PrecipitationZoneStaging_PrecipitationZoneGeometry")]
public partial class PrecipitationZoneStaging
{
    [Key]
    public int PrecipitationZoneStagingID { get; set; }

    public int PrecipitationZoneKey { get; set; }

    public double DesignStormwaterDepthInInches { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry PrecipitationZoneGeometry { get; set; } = null!;
}
