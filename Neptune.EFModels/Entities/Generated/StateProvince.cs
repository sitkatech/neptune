using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("StateProvince")]
[Index("StateProvinceFeature", Name = "SPATIAL_StateProvince_StateProvinceFeature")]
[Index("StateProvinceFeatureForAnalysis", Name = "SPATIAL_StateProvince_StateProvinceFeatureForAnalysis")]
public partial class StateProvince
{
    [Key]
    public int StateProvinceID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? StateProvinceName { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? StateProvinceAbbreviation { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? StateProvinceFeature { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry StateProvinceFeatureForAnalysis { get; set; } = null!;

    [InverseProperty("StateProvince")]
    public virtual ICollection<County> Counties { get; set; } = new List<County>();

    [InverseProperty("StateProvince")]
    public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; } = new List<StormwaterJurisdiction>();
}
