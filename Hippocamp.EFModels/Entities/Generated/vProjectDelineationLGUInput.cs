using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vProjectDelineationLGUInput
    {
        public int DelinID { get; set; }
        public int? ProjectID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry { get; set; }
    }
}
