using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPyQgisLandUseBlockTGUInput
    {
        public int LUBID { get; set; }
        public int SJID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockGeometry { get; set; }
    }
}
