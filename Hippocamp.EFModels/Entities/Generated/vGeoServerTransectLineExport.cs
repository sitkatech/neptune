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
    public partial class vGeoServerTransectLineExport
    {
        [Required]
        [StringLength(100)]
        public string OVTAAreaName { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry TransectLine { get; set; }
        public int JurisID { get; set; }
    }
}
