using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPyQgisModelBasinLGUInput
    {
        public int ModelID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ModelBasinGeometry { get; set; }
    }
}
