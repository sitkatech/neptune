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
    public partial class vGeoServerWatershed
    {
        public int WatershedID { get; set; }
        [StringLength(50)]
        public string WatershedName { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry { get; set; }
    }
}
