using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("County")]
[Index("CountyName", "StateProvinceID", Name = "AK_County_CountyName_StateProvinceID", IsUnique = true)]
public partial class County
{
    [Key]
    public int CountyID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CountyName { get; set; }

    public int StateProvinceID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? CountyFeature { get; set; }

    [ForeignKey("StateProvinceID")]
    [InverseProperty("Counties")]
    public virtual StateProvince StateProvince { get; set; } = null!;
}
