using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("ParcelGeometry")]
    [Index("ParcelID", Name = "AK_ParcelGeometry_ParcelID", IsUnique = true)]
    [Index("Geometry4326", Name = "SPATIAL_ParcelGeometry_Geometry4326")]
    [Index("GeometryNative", Name = "SPATIAL_ParcelGeometry_GeometryNative")]
    public partial class ParcelGeometry
    {
        [Key]
        public int ParcelGeometryID { get; set; }
        public int ParcelID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry GeometryNative { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry Geometry4326 { get; set; }

        [ForeignKey("ParcelID")]
        [InverseProperty("ParcelGeometry")]
        public virtual Parcel Parcel { get; set; }
    }
}
