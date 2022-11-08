using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("ParcelStaging")]
    [Index("ParcelStagingGeometry", Name = "SPATIAL_ParcelStaging_ParcelStagingGeometry")]
    [Index("ParcelStagingGeometry", Name = "ogr_dbo_parcelstaging_ParcelStagingGeometry_sidx")]
    public partial class ParcelStaging
    {
        [Key]
        public int ParcelStagingID { get; set; }
        [Required]
        [StringLength(22)]
        [Unicode(false)]
        public string ParcelNumber { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ParcelStagingGeometry { get; set; }
        public double ParcelStagingAreaSquareFeet { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string OwnerName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ParcelStreetNumber { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        public string ParcelAddress { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string ParcelZipCode { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string LandUse { get; set; }
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public int UploadedByPersonID { get; set; }

        [ForeignKey("UploadedByPersonID")]
        [InverseProperty("ParcelStagings")]
        public virtual Person UploadedByPerson { get; set; }
    }
}
