using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Watershed")]
    public partial class Watershed
    {
        [Key]
        public int WatershedID { get; set; }
        [Required]
        [StringLength(100)]
        public string WatershedName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry4326 { get; set; }
    }
}
