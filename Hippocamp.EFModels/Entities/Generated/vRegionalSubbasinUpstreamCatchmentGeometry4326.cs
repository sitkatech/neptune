using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vRegionalSubbasinUpstreamCatchmentGeometry4326
    {
        public int? PrimaryKey { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry UpstreamCatchmentGeometry4326 { get; set; }
    }
}
