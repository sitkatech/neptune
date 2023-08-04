using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("Watershed")]
public partial class Watershed
{
    [Key]
    public int WatershedID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? WatershedGeometry { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? WatershedName { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? WatershedGeometry4326 { get; set; }

    [InverseProperty("Watershed")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();
}
