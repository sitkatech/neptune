using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("OCTAPrioritization")]
[Index("OCTAPrioritizationKey", Name = "AK_OCTAPrioritization_OCTAPrioritizationKey", IsUnique = true)]
public partial class OCTAPrioritization
{
    [Key]
    public int OCTAPrioritizationID { get; set; }

    public int OCTAPrioritizationKey { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry OCTAPrioritizationGeometry { get; set; } = null!;

    [Column(TypeName = "geometry")]
    public Geometry? OCTAPrioritizationGeometry4326 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdate { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? Watershed { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? CatchIDN { get; set; }

    public double TPI { get; set; }

    public double WQNLU { get; set; }

    public double WQNMON { get; set; }

    public double IMPAIR { get; set; }

    public double MON { get; set; }

    public double SEA { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? SEA_PCTL { get; set; }

    public double PC_VOL_PCT { get; set; }

    public double PC_NUT_PCT { get; set; }

    public double PC_BAC_PCT { get; set; }

    public double PC_MET_PCT { get; set; }

    public double PC_TSS_PCT { get; set; }
}
