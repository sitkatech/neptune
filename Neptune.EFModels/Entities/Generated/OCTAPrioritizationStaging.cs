using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("OCTAPrioritizationStaging")]
[Index("OCTAPrioritizationKey", Name = "AK_OCTAPrioritizationStaging_OCTAPrioritizationKey", IsUnique = true)]
[Index("OCTAPrioritizationGeometry", Name = "SPATIAL_OCTAPrioritizationStaging_OCTAPrioritizationGeometry")]
public partial class OCTAPrioritizationStaging
{
    [Key]
    public int OCTAPrioritizationStagingID { get; set; }

    public int OCTAPrioritizationKey { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry OCTAPrioritizationGeometry { get; set; } = null!;

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
