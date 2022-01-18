using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Parcel")]
    [Index(nameof(ParcelGeometry), Name = "SPATIAL_Parcel_ParcelGeometry")]
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
        public string ParcelNumber { get; set; }
        [Required]
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
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public double ParcelAreaInAcres { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry ParcelGeometry4326 { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlanParcel.Parcel))]
        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
    }
}
