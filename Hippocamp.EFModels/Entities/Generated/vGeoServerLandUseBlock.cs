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
    public partial class vGeoServerLandUseBlock
    {
        public int LandUseBlockID { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        [StringLength(500)]
        public string LandUseDescription { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockGeometry { get; set; }
    }
}
