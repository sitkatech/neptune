using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanBoundary")]
    [Index("WaterQualityManagementPlanID", Name = "AK_WaterQualityManagementPlanGeometry_WaterQualityManagementPlanID", IsUnique = true)]
    [Index("Geometry4326", Name = "SPATIAL_WaterQualityManagementPlanBoundary_Geometry4326")]
    [Index("GeometryNative", Name = "SPATIAL_WaterQualityManagementPlanBoundary_GeometryNative")]
    public partial class WaterQualityManagementPlanBoundary
    {
        [Key]
        public int WaterQualityManagementPlanGeometryID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry GeometryNative { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry Geometry4326 { get; set; }

        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("WaterQualityManagementPlanBoundary")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
    }
}
