using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vPyQgisRegionalSubbasinLGUInput
{
    public int RSBID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry CatchmentGeometry { get; set; } = null!;

    public int? ModelID { get; set; }
}
