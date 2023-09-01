using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerTreatmentBMPDelineation
    {
        public int PrimaryKey { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
    }
}
