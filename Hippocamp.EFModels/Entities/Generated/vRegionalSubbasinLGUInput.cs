using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vRegionalSubbasinLGUInput
    {
        public int RSBID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry CatchmentGeometry { get; set; }
        public int? ModelID { get; set; }
    }
}
