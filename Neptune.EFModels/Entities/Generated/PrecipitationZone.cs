using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("PrecipitationZone")]
[Index("PrecipitationZoneKey", Name = "AK_PrecipitationZone_PrecipitationZoneKey", IsUnique = true)]
public partial class PrecipitationZone
{
    [Key]
    public int PrecipitationZoneID { get; set; }

    public int PrecipitationZoneKey { get; set; }

    public double DesignStormwaterDepthInInches { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry PrecipitationZoneGeometry { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LastUpdate { get; set; }

    [InverseProperty("PrecipitationZone")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();
}
