using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class vGeoServerWatersheds
    {
        public int PrimaryKey { get; set; }
        public int WatershedID { get; set; }
        [Required]
        [StringLength(100)]
        public string WatershedName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry4326 { get; set; }
    }
}
