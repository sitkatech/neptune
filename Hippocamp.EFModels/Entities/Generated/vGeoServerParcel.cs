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
    public partial class vGeoServerParcel
    {
        public int ParcelID { get; set; }
        [Required]
        [StringLength(22)]
        public string ParcelNumber { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry ParcelGeometry { get; set; }
        [StringLength(100)]
        public string OwnerName { get; set; }
        [StringLength(10)]
        public string ParcelStreetNumber { get; set; }
        [StringLength(150)]
        public string ParcelAddress { get; set; }
        [StringLength(5)]
        public string ParcelZipCode { get; set; }
        [StringLength(4)]
        public string LandUse { get; set; }
        public double ParcelArea { get; set; }
        public int? WQMPCount { get; set; }
    }
}
