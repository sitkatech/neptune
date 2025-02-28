using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("ParcelStaging")]
public partial class ParcelStaging
{
    [Key]
    public int ParcelStagingID { get; set; }

    [StringLength(22)]
    [Unicode(false)]
    public string? ParcelNumber { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry Geometry { get; set; } = null!;

    public double ParcelAreaInSquareFeet { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ParcelAddress { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ParcelCityState { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ParcelZipCode { get; set; }
}
