using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("DelineationOverlap")]
    public partial class DelineationOverlap
    {
        [Key]
        public int DelineationOverlapID { get; set; }
        public int DelineationID { get; set; }
        public int OverlappingDelineationID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OverlappingGeometry { get; set; }

        [ForeignKey(nameof(DelineationID))]
        [InverseProperty("DelineationOverlapDelineations")]
        public virtual Delineation Delineation { get; set; }
        [ForeignKey(nameof(OverlappingDelineationID))]
        [InverseProperty("DelineationOverlapOverlappingDelineations")]
        public virtual Delineation OverlappingDelineation { get; set; }
    }
}
