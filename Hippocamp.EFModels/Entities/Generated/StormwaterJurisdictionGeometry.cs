using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdictionGeometry")]
    [Index(nameof(StormwaterJurisdictionID), Name = "AK_StormwaterJurisdictionGeometry_StormwaterJurisdictionID", IsUnique = true)]
    [Index(nameof(GeometryNative), Name = "SPATIAL_StormwaterJurisdictionGeometry_GeometryNative")]
    public partial class StormwaterJurisdictionGeometry
    {
        [Key]
        public int StormwaterJurisdictionGeometryID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry GeometryNative { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry Geometry4326 { get; set; }

        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("StormwaterJurisdictionGeometry")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
    }
}
