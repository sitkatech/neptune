using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("Parcel")]
    [Index("ParcelGeometry", Name = "SPATIAL_Parcel_ParcelGeometry")]
    public partial class Parcel
    {
        public Parcel()
        {
            WaterQualityManagementPlanParcels = new HashSet<WaterQualityManagementPlanParcel>();
        }

        [Key]
        public int ParcelID { get; set; }
        [Required]
        [StringLength(22)]
        [Unicode(false)]
        public string ParcelNumber { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ParcelGeometry { get; set; }
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
        public double ParcelAreaInAcres { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry ParcelGeometry4326 { get; set; }

        [InverseProperty("Parcel")]
        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
    }
}
